using System;
using System.Numerics;

/// <summary>
/// Temp generic INumber based image format. T.MinValue is zero color, T.MaxValue is max color. Channels per color can be defined
/// </summary>
public sealed class TempImageFormat<T> : IImageFormat<T> where T : INumber<T>, IMinMaxValue<T>
{
	/// <summary>
	/// Width of bitmap
	/// </summary>
	public readonly int width;

	/// <summary>
	/// Height of bitmap
	/// </summary>
	public readonly int height;

	private readonly T[,,] content3d;

	private readonly T[] content1d;

	/// <summary>
	/// How many color channels per pixel
	/// </summary>
	public readonly int channelsPerPixel;

	/// <summary>
	/// Constructor for temp byte image format
	/// </summary>
	/// <param name="input">Input bitmap as three dimensional (width, height, channels per pixel) byte array</param>
	/// <param name="createCopy">True if you want to create copy of data</param>
	public TempImageFormat(T[,,] input, bool createCopy = false)
	{
		if (createCopy)
		{
			this.content3d = (T[,,])input.Clone();
		}
		else
		{
			this.content3d = input;
		}
		
		this.content1d = null;
		this.width = input.GetLength(0);
		this.height = input.GetLength(1);
		this.channelsPerPixel = input.GetLength(2);
	}

	/// <summary>
	/// Constructor for temp byte image format
	/// </summary>
	/// <param name="input">Input byte array</param>
	/// <param name="imageWidth">Width</param>
	/// <param name="imageHeight">Height</param>
	/// <param name="imageChannelsPerPixel">Image channels per pixel</param>
	/// <param name="createCopy">True if you want to create copy of data</param>
	public TempImageFormat(T[] input, int imageWidth, int imageHeight, int imageChannelsPerPixel, bool createCopy = false)
	{
		this.content3d = null;
		if (createCopy)
		{
			this.content1d = (T[])input.Clone();
		}
		else
		{
			this.content1d = input;
		}
		this.width = imageWidth;
		this.height = imageHeight;
		this.channelsPerPixel = imageChannelsPerPixel;
	}

	/// <summary>
	/// Constructor for temp byte image format
	/// </summary>
	/// <param name="input">Existing TempByteImageFormat</param>
	public TempImageFormat(TempImageFormat<T> input)
	{
		if (input.content1d != null)
		{
			this.content1d = input.content1d;
			this.content3d = null;
		}
		else
		{
			this.content3d = input.content3d;
			this.content1d = null;
		}

		this.width = input.width;
		this.height = input.height;
		this.channelsPerPixel = input.channelsPerPixel;
	}

	/// <summary>
	/// Get width of bitmap
	/// </summary>
	/// <returns>Width in pixels</returns>
	public int GetWidth()
	{
		return this.width;
	}    
	
	/// <summary>
	/// Get height of bitmap
	/// </summary>
	/// <returns>Height in pixels</returns>
	public int GetHeight()
	{
		return this.height;
	}

	/// <summary>
	/// Get channels per pixel
	/// </summary>
	/// <returns>Channels per pixel</returns>
	public int GetChannelsPerPixel()
	{
		return this.channelsPerPixel;
	}

	/// <summary>
	/// Get raw content as byte array
	/// </summary>
	/// <returns>Byte array</returns>
	public T[] GetRawContent()
	{
		if (this.content1d != null)
		{
			return this.content1d;
		}
		else
		{
			T[] returnArray = new T[this.width * this.height * this.channelsPerPixel];
			int currentIndex = 0;
			for (int y = 0; y < this.height; y++)
			{
				for (int x = 0; x < this.width; x++)
				{
					for (int i = 0; i < this.channelsPerPixel; i++)
					{
						returnArray[currentIndex] = this.content3d[x, y, i];
						currentIndex++;
					}
				}
			}

			return returnArray;
		}
	}

	/// <summary>
	/// Set pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="newValues">New values as object array</param>
	public void SetPixelChannels(int x, int y, T[] newValues)
	{
		if (this.content1d != null)
		{
			int indexBase = y * this.width * this.channelsPerPixel + x * this.channelsPerPixel;
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				this.content1d[indexBase + i] = newValues[i];
			}
		}
		else
		{
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				this.content3d[x, y, i] = newValues[i];
			}
		}
	}

	/// <summary>
	/// Get pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <returns>Values as byte array</returns>
	public T[] GetPixelChannels(int x, int y)
	{
		T[] returnArray = new T[this.channelsPerPixel];

		if (this.content1d != null)
		{
			int indexBase = y * this.width * this.channelsPerPixel + x * this.channelsPerPixel;
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				returnArray[i] = this.content1d[indexBase + i];
			}
		}
		else
		{
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				returnArray[i] = this.content3d[x, y, i];
			}
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
		if (this.content1d != null)
		{
			int indexBase = y * this.width * this.channelsPerPixel + x * this.channelsPerPixel;
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				pixelStorage[i] = this.content1d[indexBase + i];
			}
		}
		else
		{
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				pixelStorage[i] = this.content3d[x, y, i];
			}
		}
	}

	/// <summary>
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <param name="errorValues">Error values as double array</param>
	public void GetQuantErrorsPerChannel(in T[] originalPixel, in T[] newPixel, ref double[] errorValues)
	{
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			errorValues[i] = double.CreateChecked(originalPixel[i]) - double.CreateChecked(newPixel[i]);
		}
	}

	/// <summary>
	/// Modify existing values with quantization errors
	/// </summary>
	/// <param name="modifyValues">Values to modify</param>
	/// <param name="quantErrors">Quantization errors</param>
	/// <param name="multiplier">Multiplier</param>
	public void ModifyPixelChannelsWithQuantError(ref T[] modifyValues, double[] quantErrors, double multiplier)
	{
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			double newValue = double.CreateChecked(modifyValues[i]) + quantErrors[i] * multiplier;
			double clamped = Math.Clamp(newValue, clampMin, clampMax);
			modifyValues[i] = T.CreateChecked(clamped);
		}
	}

	private readonly double clampMin = double.CreateChecked(T.MinValue);
	private readonly double clampMax = double.CreateChecked(T.MaxValue);

}