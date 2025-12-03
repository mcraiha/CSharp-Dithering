/*
   This file implements base dithering class that can be used to implement error pushing dithering implementations.

   This is free and unencumbered software released into the public domain.
*/

using System;
using System.Numerics;

/// <summary>
/// Abstract base class for dithering implementations that use error pushing
/// </summary>
public abstract class DitheringNoiseBase<T> : IDitherCommon<T> where T : INumber<T>, IMinMaxValue<T>
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
	private readonly string methodLongName = "";

	/// <summary>
	/// Filename addition
	/// </summary>
	private readonly string fileNameAddition = "";

	/// <summary>
	/// Color reduction function/method
	/// </summary>
	protected ColorFunction colorFunction = null;

	/// <summary>
	/// Dither type
	/// </summary>
	protected readonly DitherType ditherType;

	/// <summary>
	/// Current bitmap
	/// </summary>
	private IImageFormat<T> currentBitmap;

	/// <summary>
	/// Mask bitmap
	/// </summary>
	private readonly MaskBitmap<T> maskBitmap;

	/// <summary>
	/// Color function for color reduction
	/// </summary>
	/// <param name="inputColors">Input colors</param>
	/// <param name="outputColors">Output colors</param>
	public delegate void ColorFunction(in T[] inputColors, ref T[] outputColors);

	/// <summary>
	/// Base constructor for mask dither
	/// </summary>
	/// <param name="colorfunc">Color reduction function/method</param>
	/// <param name="mask">Mask image</param>
	/// <param name="longName">Long name of dither method</param>
	/// <param name="fileNameAdd">Filename addition</param>
	public DitheringNoiseBase(ColorFunction colorfunc, MaskBitmap<T> mask, string longName, string fileNameAdd)
	{
		this.colorFunction = colorfunc;
		this.maskBitmap = mask;
		this.ditherType = DitherType.Mask;
		this.methodLongName = longName;
		this.fileNameAddition = fileNameAdd;
	}

	/// <summary>
	/// Do dithering for chosen image with chosen color reduction method. Work horse, call this when you want to dither something
	/// </summary>
	/// <param name="input">Input image</param>
	/// <returns>Dithered image</returns>
	public IImageFormat<T> DoDithering(IImageFormat<T> input)
	{
		this.width = input.GetWidth();
		this.height = input.GetHeight();
		int channelsPerPixel = input.GetChannelsPerPixel();
		this.currentBitmap = input;

		T[] originalPixel = new T[channelsPerPixel];
		T[] newPixel = new T[channelsPerPixel];

		T[] maskPixel = new T[channelsPerPixel];
		int maskWidth = maskBitmap.width;
		int maskHeight = maskBitmap.height;

		bool isFloatType = (typeof(T) == typeof(double) || typeof(T) == typeof(float));
		double half = double.CreateChecked(T.MaxValue - T.MinValue);

		for (int y = 0; y < this.height; y++)
		{
			for (int x = 0; x < this.width; x++)
			{
				input.GetPixelChannels(x, y, ref originalPixel);

				maskBitmap.GetPixelChannels(x % maskWidth, y % maskHeight, ref maskPixel);

				for (int i = 0; i < originalPixel.Length; i++)
				{
					// Be lazy and only operate with doubles
					if (isFloatType)
					{
						double maskPixelWithAdjustment = double.CreateChecked(originalPixel[i]) + (2.0 * double.CreateChecked(maskPixel[i]) - 1.0);
						originalPixel[i] = input.GetClamped(maskPixelWithAdjustment);
					}
					else
					{
						// Really hacksy way of doing this
						double maskPixelWithAdjustment = double.CreateChecked(originalPixel[i]) + double.CreateChecked(maskPixel[i]) - half/2;
						originalPixel[i] = input.GetClamped(maskPixelWithAdjustment);
					}
				}

				this.colorFunction(in originalPixel, ref newPixel);

				input.SetPixelChannels(x, y, newPixel);
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

	private T[] tempBuffer = null;

	/// <summary>
	/// Modify image with error and multiplier
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	/// <param name="multiplier">Multiplier</param>
	public void ModifyImageWithErrorAndMultiplier(int x, int y, double[] quantError, double multiplier)
	{
		this.currentBitmap.GetPixelChannels(x, y, ref this.tempBuffer);

		// We limit the color here because we don't want the value go over min or max
		this.currentBitmap.ModifyPixelChannelsWithQuantError(ref this.tempBuffer, quantError, multiplier);

		this.currentBitmap.SetPixelChannels(x, y, this.tempBuffer);
	}
}
