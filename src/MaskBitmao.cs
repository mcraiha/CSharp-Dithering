using System;
using System.Numerics;

/// <summary>
/// Class for handling mask bitmap
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class MaskBitmap<T> where T : INumber<T>, IMinMaxValue<T>
{
	/// <summary>
	/// Width of bitmap
	/// </summary>
	public readonly int width;

	/// <summary>
	/// Height of bitmap
	/// </summary>
	public readonly int height;

	private readonly byte[] content1d;

	/// <summary>
	/// How many color channels per pixel
	/// </summary>
	public readonly int channelsPerPixel;

	/// <summary>
	/// Only constructor
	/// </summary>
	/// <param name="maskImageBytes">Mask image as bytes</param>
	/// <param name="imageWidth">Image width</param>
	/// <param name="imageHeight">Image height</param>
	/// <param name="imageChannelsPerPixel">Image channels per pixel</param>
	/// <exception cref="ArgumentException">If there are not enough mask bytes</exception>
	public MaskBitmap(byte[] maskImageBytes, int imageWidth, int imageHeight, int imageChannelsPerPixel)
	{
		if (maskImageBytes.Length < imageWidth * imageHeight * imageChannelsPerPixel)
		{
			throw new ArgumentException($"Not enough bytes, {maskImageBytes.Length} vs. {imageWidth * imageHeight * imageChannelsPerPixel}");
		}

		this.content1d = maskImageBytes;
		this.width = imageWidth;
		this.height = imageHeight;
		this.channelsPerPixel = imageChannelsPerPixel;
	}

	/// <summary>
	/// Get pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="howManyChannels">How many pixel channels are wanted</param>
	/// <returns>Values as array</returns>
	public T[] GetPixelChannels(int x, int y, int howManyChannels)
	{
		T[] returnArray = new T[howManyChannels];

		int indexBase = y * this.width * this.channelsPerPixel + x * this.channelsPerPixel;
		for (int i = 0; i < howManyChannels; i++)
		{
			returnArray[i] = T.CreateChecked(this.content1d[indexBase + i]);
		}

		return returnArray;
	}

	/// <summary>
	/// Get pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="pixelStorage">Array where pixel channels values will be written</param>
	public void GetPixelChannels(int x, int y, ref T[] pixelStorage)
	{
		int indexBase = y * this.width * this.channelsPerPixel + x * this.channelsPerPixel;
		for (int i = 0; i < pixelStorage.Length; i++)
		{
			pixelStorage[i] = T.CreateChecked(this.content1d[indexBase + i]);
		}
	}
}