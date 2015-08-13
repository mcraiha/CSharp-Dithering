/*
   This file implements base dithering class that can be used to implement error pushing dithering implementations.

   This is free and unencumbered software released into the public domain.
*/

using System;
using System.Drawing;

// This Delegate is used to find best suitable color from limited set of colors
public delegate Color FindColor(Color original);

public abstract class DitheringBase {

    protected Bitmap currentBitmap = null; // Slow mode uses this
    protected byte[] currentBitmapAsByteArray = null; // Fast mode uses this

    protected int width;
	protected int height;

    protected bool isFastMode = false;

    protected string methodLongName = "";
    protected string fileNameAddition = "";

    protected FindColor colorFunction = null;

	public DitheringBase(FindColor colorfunc, bool useFastMode = false)
	{
        this.colorFunction = colorfunc;
        this.isFastMode = useFastMode;
    }

    // Work horse, call this when you want to dither something
    public Bitmap DoDithering(Bitmap input)
    {
        if (this.isFastMode)
        {
            return this.DoDitheringFast(input);
        }

        return this.DoDitheringSlow(input);
    }

    // Fast dither uses LockBits and InteropServices.Marshal.Copy
    private Bitmap DoDitheringFast(Bitmap input)
    {
        this.width = input.Width;
        this.height = input.Height;

        // Lock input bitmap for reading
        Rectangle rect = new Rectangle(0, 0, input.Width, input.Height);
        System.Drawing.Imaging.BitmapData bmpData =
            input.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
            input.PixelFormat);

        // Get the address of the first line.
        IntPtr ptr = bmpData.Scan0;

        // Declare an array to hold the bytes of the bitmap. 
        int byteCount = Math.Abs(bmpData.Stride) * input.Height;
        this.currentBitmapAsByteArray = new byte[byteCount];

        // Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, this.currentBitmapAsByteArray, 0, byteCount);

        // Release the lock
        input.UnlockBits(bmpData);

        Color originalPixel = Color.White; // Default value isn't used
        Color newPixel = Color.White; // Default value isn't used
        short[] quantError = null; // Default values aren't used

        for (int y = 0; y < this.height; y++)
        {
            for (int x = 0; x < this.width; x++)
            {
                originalPixel = GetColorFromByteArray(this.currentBitmapAsByteArray, x, y, this.width);
                newPixel = this.colorFunction(originalPixel);

                SetColorToByteArray(this.currentBitmapAsByteArray, x, y, this.width, newPixel);

                quantError = GetQuantError(originalPixel, newPixel);
                this.PushError(x, y, quantError);
            }
        }

        // Create new bitmap
        Bitmap returnBitmap = new Bitmap(this.width, this.height);
        // Lock it
        bmpData =
            returnBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
            input.PixelFormat);
        // Get the address of the first line.
        ptr = bmpData.Scan0;
        // Copy array to it
        System.Runtime.InteropServices.Marshal.Copy(this.currentBitmapAsByteArray, 0, ptr, byteCount);
        // Unlock the bitmap
        returnBitmap.UnlockBits(bmpData);

        return returnBitmap;
    }

    // Slow dither uses GetPixel and SetPixel
    private Bitmap DoDitheringSlow(Bitmap input)
	{
		this.width = input.Width;
		this.height = input.Height;

        // Copy input to different bitmap so it can be modified
        this.currentBitmap = new Bitmap(input);

		Color originalPixel = Color.White; // Default value isn't used
		Color newPixel = Color.White; // Default value isn't used
        short[] quantError = null; // Default values aren't used

        for (int y = 0; y < this.height; y++)
		{
			for (int x = 0; x < this.width; x++)
			{
                originalPixel = this.currentBitmap.GetPixel(x, y);
                newPixel = this.colorFunction(originalPixel);

                this.currentBitmap.SetPixel(x, y, newPixel);

                quantError = GetQuantError(originalPixel, newPixel);

                this.PushError(x, y, quantError);
            }
		}

        return this.currentBitmap;
    }

    public string GetMethodName()
    {
        return this.methodLongName;
    }

    public string GetFilenameAddition()
    {
        return this.fileNameAddition;
    }

    protected short[] GetQuantError(Color originalPixel, Color newPixel)
    {
        short[] returnValue = new short[4];

        returnValue[0] = (short)(originalPixel.R - newPixel.R);
        returnValue[1] = (short)(originalPixel.G - newPixel.G);
        returnValue[2] = (short)(originalPixel.B - newPixel.B);
        returnValue[3] = (short)(originalPixel.A - newPixel.A);

        return returnValue;
    }

    protected bool IsValidCoordinate(int x, int y)
    {
        return (0 <= x && x < this.width && 0 <= y && y < this.height);
    }

    // Implement this for every dithering method
    protected abstract void PushError(int x, int y, short[] quantError);

    public void ModifyImageWithErrorAndMultiplier(int x, int y, short[] quantError, float multiplier)
    {
        Color oldColor = Color.White; // Default color isn't used
        if (this.isFastMode)
        {
            oldColor = GetColorFromByteArray(this.currentBitmapAsByteArray, x, y, this.width);
        }
        else
        {
            oldColor = this.currentBitmap.GetPixel(x, y);
        }

        // We limit the color here because we don't want the value go over 255 or under 0
        Color newColor = Color.FromArgb(
                            //GetLimitedValue(oldColor.A, (int)Math.Round(quantError[3] * multiplier)),
                            GetLimitedValue(oldColor.R, (int)Math.Round(quantError[0] * multiplier)),
                            GetLimitedValue(oldColor.G, (int)Math.Round(quantError[1] * multiplier)),
                            GetLimitedValue(oldColor.B, (int)Math.Round(quantError[2] * multiplier)));

        if (this.isFastMode)
        {
            SetColorToByteArray(this.currentBitmapAsByteArray, x, y, this.width, newColor);
        }
        else
        {
            this.currentBitmap.SetPixel(x, y, newColor);
        }
    }

    private static byte GetLimitedValue(byte original, int error)
    {
        int newValue = original + error;
        return (byte)Clamp(newValue, byte.MinValue, byte.MaxValue);
    }

    // C# doesn't have a Clamp method so we need this
    private static int Clamp(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    private static Color GetColorFromByteArray(byte[] byteArray, int x, int y, int width)
    {
        int baseAddress = 3 * (y * width + x);
        return Color.FromArgb(byteArray[baseAddress + 2], byteArray[baseAddress + 1], byteArray[baseAddress]);
    }

    private static void SetColorToByteArray(byte[] byteArray, int x, int y, int width, Color color)
    {
        int baseAddress = 3 * (y * width + x);
        byteArray[baseAddress + 2] = color.R;
        byteArray[baseAddress + 1] = color.G;
        byteArray[baseAddress] = color.B;
    }
}
