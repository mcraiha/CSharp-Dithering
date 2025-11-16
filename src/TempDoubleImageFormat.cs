using System;
using System.Runtime.Intrinsics;

/// <summary>
/// Temp double based image format. 0.0 is zero color, 1.0 is max color
/// </summary>
public sealed class TempDoubleImageFormat : IImageFormat<double>
{
	/// <summary>
	/// Width of bitmap
	/// </summary>
	public readonly int width;

	/// <summary>
	/// Height of bitmap
	/// </summary>
	public readonly int height;

	private readonly double[,,] content3d;

	private readonly double[] content1d;

	/// <summary>
	/// How many color channels per pixel
	/// </summary>
	public readonly int channelsPerPixel;

	/// <summary>
	/// Constructor for temp double image format
	/// </summary>
	/// <param name="input">Input bitmap as three dimensional (width, height, channels per pixel) double array</param>
	/// <param name="createCopy">True if you want to create copy of data</param>
	public TempDoubleImageFormat(double[,,] input, bool createCopy = false)
	{
		if (createCopy)
		{
			this.content3d = (double[,,])input.Clone();
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
	/// Constructor for temp double image format
	/// </summary>
	/// <param name="input">Input double array</param>
	/// <param name="imageWidth">Width</param>
	/// <param name="imageHeight">Height</param>
	/// <param name="imageChannelsPerPixel">Image channels per pixel</param>
	/// <param name="createCopy">True if you want to create copy of data</param>
	public TempDoubleImageFormat(double[] input, int imageWidth, int imageHeight, int imageChannelsPerPixel, bool createCopy = false)
	{
		this.content3d = null;
		if (createCopy)
		{
			this.content1d = new double[input.Length];
			Buffer.BlockCopy(input, 0, this.content1d, 0, input.Length * sizeof(double));
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
	/// Constructor for temp double image format
	/// </summary>
	/// <param name="input">Existing TempDoubleImageFormat</param>
	public TempDoubleImageFormat(TempDoubleImageFormat input)
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
	/// Get raw content as double array
	/// </summary>
	/// <returns>Double array</returns>
	public double[] GetRawContent()
	{
		if (this.content1d != null)
		{
			return this.content1d;
		}
		else
		{
			double[] returnArray = new double[this.width * this.height * this.channelsPerPixel];
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
	/// <param name="newValues">New values as double array</param>
	public void SetPixelChannels(int x, int y, double[] newValues)
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
	/// <returns>Values as double array</returns>
	public double[] GetPixelChannels(int x, int y)
	{
		double[] returnArray = new double[this.channelsPerPixel];

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
	public void GetPixelChannels(int x, int y, ref double[] pixelStorage)
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
	public void GetQuantErrorsPerChannel(in double[] originalPixel, in double[] newPixel, ref double[] errorValues)
	{
		if (this.channelsPerPixel == 1)
		{
			errorValues[0] = originalPixel[0] - newPixel[0];
		}
		else if (this.channelsPerPixel == 3)
		{
			errorValues[0] = originalPixel[0] - newPixel[0];
			errorValues[1] = originalPixel[1] - newPixel[1];
			errorValues[2] = originalPixel[2] - newPixel[2];
		}
		else
		{
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				errorValues[i] = originalPixel[i] - newPixel[i];
			}
		}
	}

	/// <summary>
	/// Modify existing values with quantization errors
	/// </summary>
	/// <param name="modifyValues">Values to modify</param>
	/// <param name="quantErrors">Quantization errors</param>
	/// <param name="multiplier">Multiplier</param>
	#if NET9_0
	public void ModifyPixelChannelsWithQuantError(ref double[] modifyValues, double[] quantErrors, double multiplier)
	{
		if (this.channelsPerPixel == 1)
		{
			modifyValues[0] = GetLimitedValue(modifyValues[0], quantErrors[0] * multiplier);
		}
		else if (this.channelsPerPixel == 3)
		{
			Span<double> temp = stackalloc double[4];

			Vector256<double> originals = Vector256.Create(modifyValues[0], modifyValues[1], modifyValues[2], 0);
			Vector256<double> errors = Vector256.Create(quantErrors[0], quantErrors[1], quantErrors[2], 0);
			errors *= multiplier;
			originals += errors;

			Vector256.Clamp(originals, Vector256<double>.Zero, Vector256<double>.One);
			Vector256.CopyTo(originals, temp);
			temp.Slice(0, 3).CopyTo(modifyValues);
		}
		else
		{
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				modifyValues[i] = GetLimitedValue(modifyValues[i], quantErrors[i] * multiplier);
			}
		}
	}
	#else
	public void ModifyPixelChannelsWithQuantError(ref double[] modifyValues, double[] quantErrors, double multiplier)
	{
		if (this.channelsPerPixel == 1)
		{
			modifyValues[0] = GetLimitedValue(modifyValues[0], quantErrors[0] * multiplier);
		}
		else if (this.channelsPerPixel == 3)
		{
			modifyValues[0] = GetLimitedValue(modifyValues[0], quantErrors[0] * multiplier);
			modifyValues[1] = GetLimitedValue(modifyValues[1], quantErrors[1] * multiplier);
			modifyValues[2] = GetLimitedValue(modifyValues[2], quantErrors[2] * multiplier);
		}
		else
		{
			for (int i = 0; i < this.channelsPerPixel; i++)
			{
				modifyValues[i] = GetLimitedValue(modifyValues[i], quantErrors[i] * multiplier);
			}
		}
	}
	#endif // NET9_0

	/// <summary>
	/// Get clamped value
	/// </summary>
	/// <param name="input">Double value input</param>
	/// <returns>Clamped value</returns>
	public double GetClamped(double input)
	{
		return Math.Clamp(input, 0.0, 1.0);
	}

	private static double GetLimitedValue(double original, double error)
	{
		double newValue = original + error;
		return Math.Clamp(newValue, 0.0, 1.0);
	}
}