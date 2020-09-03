using System;

/// <summary>
/// Temp double based image format. 0.0 is zero color, 1.0 is max color
/// </summary>
public class TempDoubleImageFormat : IImageFormat<double>
{
	/// <summary>
	/// Width of bitmap
	/// </summary>
	public readonly int width;

	/// <summary>
	/// Height of bitmap
	/// </summary>
	public readonly int height;

	private readonly double[,,] content;

	/// <summary>
	/// How many color channels per pixel
	/// </summary>
	public readonly int channelsPerPixel;

	/// <summary>
	/// Constructor for temp double image format
	/// </summary>
	/// <param name="input">Input bitmap as three dimensional (widht, height, channels per pixel) double array</param>
	public TempDoubleImageFormat(double[,,] input)
	{
		this.content = input;
		this.width = input.GetLength(0);
		this.height = input.GetLength(1);
		this.channelsPerPixel = input.GetLength(2);
	}

	/// <summary>
	/// Constructor for temp double image format
	/// </summary>
	/// <param name="input">Existing TempDoubleImageFormat</param>
	public TempDoubleImageFormat(TempDoubleImageFormat input) : this(input.content)
	{

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
	/// Set pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="newValues">New values as double array</param>
	public void SetPixelChannels(int x, int y, double[] newValues)
	{
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			this.content[x, y, i] = (double)newValues[i];
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

		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnArray[i] = this.content[x, y, i];
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
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			pixelStorage[i] = this.content[x, y, i];
		}
	}

	/// <summary>
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <returns>Error values as double array</returns>
	public double[] GetQuantErrorsPerChannel(double[] originalPixel, double[] newPixel)
	{
		double[] returnValue = new double[this.channelsPerPixel];

		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnValue[i] = (double)originalPixel[i] - (double)newPixel[i];
		}

		return returnValue;
	}

	/// <summary>
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <param name="errorValues">Error values as double array</param>
	public void GetQuantErrorsPerChannel(in double[] originalPixel, in double[] newPixel, ref double[] errorValues)
	{
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			errorValues[i] = originalPixel[i] - newPixel[i];
		}
	}

	/// <summary>
	/// Create new values from old values and quantization errors
	/// </summary>
	/// <param name="oldValues">Old values</param>
	/// <param name="quantErrors">Quantization errors</param>
	/// <param name="multiplier">Multiplier</param>
	/// <returns>New values</returns>
	public double[] CreatePixelFromChannelsAndQuantError(double[] oldValues, double[] quantErrors, double multiplier)
	{
		double[] returnValue = new double[oldValues.Length];
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnValue[i] = GetLimitedValue((byte)oldValues[i], quantErrors[i] * multiplier);
		}
		
		return returnValue;
	}

	private static double GetLimitedValue(byte original, double error)
	{
		double newValue = original + error;
		return Clamp(newValue, 0.0, 1.0);
	}

	// C# doesn't have a Clamp method so we need this
	private static double Clamp(double value, double min, double max)
	{
		return (value < min) ? 0.0 : (value > max) ? 1.0 : value;
	}
}