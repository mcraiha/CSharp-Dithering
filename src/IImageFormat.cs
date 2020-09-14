
/// <summary>
/// Interface for our custom image formats
/// </summary>
public interface IImageFormat<T>
{
	/// <summary>
	/// Get width
	/// </summary>
	/// <returns>Width of image</returns>
	int GetWidth();

	/// <summary>
	/// Get height
	/// </summary>
	/// <returns>Height of image</returns>
	int GetHeight();

	/// <summary>
	/// Get channels per pixel
	/// </summary>
	/// <returns>Channels per pixel</returns>
	int GetChannelsPerPixel();

	/// <summary>
	/// Get raw content as array
	/// </summary>
	/// <returns>Array</returns>
	T[] GetRawContent();

	/// <summary>
	/// Set pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="newValues">New values</param>
	void SetPixelChannels(int x, int y, T[] newValues);

	/// <summary>
	/// Get pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <returns>Values as array</returns>
	T[] GetPixelChannels(int x, int y);

	/// <summary>
	/// Get pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="pixelStorage">Array where pixel channels values will be written</param>
	void GetPixelChannels(int x, int y, ref T[] pixelStorage);

	/// <summary>
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <returns>Error values as double array</returns>
	double[] GetQuantErrorsPerChannel(T[] originalPixel, T[] newPixel);

	/// <summary>
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <param name="errorValues">Error values as double array</param>
	void GetQuantErrorsPerChannel(in T[] originalPixel, in T[] newPixel, ref double[] errorValues);

	/// <summary>
	/// Modify existing values with quantization errors
	/// </summary>
	/// <param name="modifyValues">Values to modify</param>
	/// <param name="quantErrors">Quantization errors</param>
	/// <param name="multiplier">Multiplier</param>
	void ModifyPixelChannelsWithQuantError(ref T[] modifyValues, double[] quantErrors, double multiplier);
}