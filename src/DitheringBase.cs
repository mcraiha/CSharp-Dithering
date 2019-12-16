/*
   This file implements base dithering class that can be used to implement error pushing dithering implementations.

   This is free and unencumbered software released into the public domain.
*/

using System;

public abstract class DitheringBase
{
	protected int width;
	protected int height;

	protected string methodLongName = "";
	protected string fileNameAddition = "";

	protected Func<object[],object[]> colorFunction = null;

	private IImageFormat currentBitmap;

	public DitheringBase(Func<object[],object[]> colorfunc)
	{
		this.colorFunction = colorfunc;
	}

	// Work horse, call this when you want to dither something
	public IImageFormat DoDithering(IImageFormat input)
	{
		this.width = input.GetWidth();
		this.height = input.GetHeight();
		this.currentBitmap = input;

		object[] originalPixel = null; // Default value isn't used
		object[] newPixel = null; // Default value isn't used
		double[] quantError = null; // Default values aren't used

		for (int y = 0; y < this.height; y++)
		{
			for (int x = 0; x < this.width; x++)
			{
				originalPixel = input.GetPixelChannels(x, y);
				newPixel = this.colorFunction(originalPixel);

				input.SetPixelChannels(x, y, newPixel);

				quantError = input.GetQuantErrorsPerChannel(originalPixel, newPixel);

				this.PushError(x, y, quantError);
			}
		}

		return input;
	}

	public string GetMethodName()
	{
		return this.methodLongName;
	}

	public string GetFilenameAddition()
	{
		return this.fileNameAddition;
	}

	protected bool IsValidCoordinate(int x, int y)
	{
		return (0 <= x && x < this.width && 0 <= y && y < this.height);
	}

	// Implement this for every dithering method
	protected abstract void PushError(int x, int y, double[] quantError);

	public void ModifyImageWithErrorAndMultiplier(int x, int y, double[] quantError, double multiplier)
	{
		object[] oldColor = this.currentBitmap.GetPixelChannels(x, y);

		// We limit the color here because we don't want the value go over min or max
		object[] newColor = this.currentBitmap.CreatePixelFromChannelsAndQuantError(oldColor, quantError, multiplier);

		this.currentBitmap.SetPixelChannels(x, y, newColor);
	}
}
