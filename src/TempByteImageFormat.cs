using System;

public class TempByteImageFormat : IImageFormat
{
	public readonly int width;

	public readonly int height;

	private readonly byte[,,] content;

	public readonly int channelsPerPixel;

	public TempByteImageFormat(byte[,,] input)
	{
		this.content = input;
		this.width = input.GetLength(0);
		this.height = input.GetLength(1);
		this.channelsPerPixel = input.GetLength(2);
	}

	public TempByteImageFormat(TempByteImageFormat input) : this(input.content)
	{

	}

	public int GetWidth()
	{
		return this.width;
	}    
	
	public int GetHeight()
	{
		return this.height;
	}

	public void SetPixelChannels(int x, int y, object[] newValues)
	{
		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			this.content[x, y, i] = (byte)newValues[i];
		}
	}

	public object[] GetPixelChannels(int x, int y)
	{
		object[] returnArray = new object[this.channelsPerPixel];

		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnArray[i] = this.content[x, y, i];
		}

		return returnArray;
	}

	public double[] GetQuantErrorsPerChannel(object[] originalPixel, object[] newPixel)
	{
		double[] returnValue = new double[this.channelsPerPixel];

		for (int i = 0; i < this.channelsPerPixel; i++)
		{
			returnValue[i] = (byte)originalPixel[i] - (byte)newPixel[i];
		}

		return returnValue;
	}

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