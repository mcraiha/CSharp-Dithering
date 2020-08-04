/*
   This file implements base dithering class that can be used to implement error pushing dithering implementations.

   This is free and unencumbered software released into the public domain.
*/

using System;

/// <summary>
/// Abstract base class for dithering implementations
/// </summary>
public abstract class DitheringBase
{
	/// <summary>
	/// Width of bitmap
	/// </summary>
	protected int width;

	/// <summary>
	/// Height of bitmap
	/// </summary>
	protected int height;

	/// <summary>
	/// Long name of the dither method
	/// </summary>
	protected string methodLongName = "";

	/// <summary>
	/// Filename addition
	/// </summary>
	protected string fileNameAddition = "";

	/// <summary>
	/// Color reduction function/method
	/// </summary>
	protected Func<object[],object[]> colorFunction = null;

	/// <summary>
	/// Current bitmap
	/// </summary>
	private IImageFormat currentBitmap;

	/// <summary>
	/// Base constructor
	/// </summary>
	/// <param name="colorfunc">Color reduction function/method</param>
	public DitheringBase(Func<object[],object[]> colorfunc)
	{
		this.colorFunction = colorfunc;
	}

	/// <summary>
	/// Do dithering for chosen image with chosen color reduction method. Work horse, call this when you want to dither something
	/// </summary>
	/// <param name="input">Input image</param>
	/// <returns>Dithered image</returns>
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

	/// <summary>
	/// Get dither method name
	/// </summary>
	/// <returns>String method name</returns>
	public string GetMethodName()
	{
		return this.methodLongName;
	}

	/// <summary>
	/// Get filename addition
	/// </summary>
	/// <returns></returns>
	public string GetFilenameAddition()
	{
		return this.fileNameAddition;
	}

	/// <summary>
	/// Check if image coordinate is valid
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <returns>True if valid; False otherwise</returns>
	protected bool IsValidCoordinate(int x, int y)
	{
		return (0 <= x && x < this.width && 0 <= y && y < this.height);
	}

	/// <summary>
	/// How error cumulation should be handled. Implement this for every dithering method
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	protected abstract void PushError(int x, int y, double[] quantError);

	/// <summary>
	/// Modify image with error and multiplier
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	/// <param name="multiplier">Multiplier</param>
	public void ModifyImageWithErrorAndMultiplier(int x, int y, double[] quantError, double multiplier)
	{
		object[] oldColor = this.currentBitmap.GetPixelChannels(x, y);

		// We limit the color here because we don't want the value go over min or max
		object[] newColor = this.currentBitmap.CreatePixelFromChannelsAndQuantError(oldColor, quantError, multiplier);

		this.currentBitmap.SetPixelChannels(x, y, newColor);
	}
}
