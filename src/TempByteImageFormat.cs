using System;

/// <summary>
/// Temp byte based image format. 0 is zero color, 255 is max color
/// </summary>
public class TempByteImageFormat : IImageFormat
{
	/// <summary>
	/// Width of bitmap
	/// </summary>
	public readonly int width;

	/// <summary>
	/// Height of bitmap
	/// </summary>
	public readonly int height;

	private readonly byte[,,] content;

	/// <summary>
	/// How many color channels per pixel
	/// </summary>
	public readonly int channelsPerPixel;

	/// <summary>
	/// Constructor for temp byte image format
	/// </summary>
	/// <param name="input">Input bitmap as three dimensional (widht, height, channels per pixel) byte array</param>
	public TempByteImageFormat(byte[,,] input)
	{
		this.content = input;
		this.width = input.GetLength(0);
		this.height = input.GetLength(1);
		this.channelsPerPixel = input.GetLength(2);
	}

	/// <summary>
	/// Constructor for temp byte image format
	/// </summary>
	/// <param name="input">Existing TempByteImageFormat</param>
	public TempByteImageFormat(TempByteImageFormat input) : this(input.content)
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
	/// Set pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="newValues">New values as object array</param>
	public void SetPixelChannels(int x, int y, object[] newValues)
	{
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			this.content[x, y, i] = (byte)newValues[i];
		}
	}

	/// <summary>
	/// Get pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <returns>Values as object array</returns>
	public object[] GetPixelChannels(int x, int y)
	{
		object[] returnArray = new object[this.channelsPerPixel];

		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnArray[i] = this.content[x, y, i];
		}

		return returnArray;
	}

	/// <summary>
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <returns>Error values as object array</returns>
	public double[] GetQuantErrorsPerChannel(object[] originalPixel, object[] newPixel)
	{
		double[] returnValue = new double[this.channelsPerPixel];

		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnValue[i] = (byte)originalPixel[i] - (byte)newPixel[i];
		}

		return returnValue;
	}

	/// <summary>
	/// Create new values from old values and quantization errors
	/// </summary>
	/// <param name="oldValues">Old values</param>
	/// <param name="quantErrors">Quantization errors</param>
	/// <param name="multiplier">Multiplier</param>
	/// <returns>New values</returns>
	public object[] CreatePixelFromChannelsAndQuantError(object[] oldValues, double[] quantErrors, double multiplier)
	{
		object[] returnValue = new object[oldValues.Length];
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnValue[i] = GetLimitedValue((byte)oldValues[i], quantErrors[i] * multiplier);
		}
		
		return returnValue;
	}

	private static byte GetLimitedValue(byte original, double error)
	{
		double newValue = original + error;
		return Clamp(newValue, byte.MinValue, byte.MaxValue);
	}

	// C# doesn't have a Clamp method so we need this
	private static byte Clamp(double value, double min, double max)
	{
		return (value < min) ? (byte)min : (value > max) ? (byte)max : (byte)value;
	}
}