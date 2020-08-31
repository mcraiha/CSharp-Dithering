
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
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <returns>Error values as double array</returns>
	double[] GetQuantErrorsPerChannel(T[] originalPixel, T[] newPixel);

	/// <summary>
	/// Create new values from old values and quantization errors
	/// </summary>
	/// <param name="oldValues">Old values</param>
	/// <param name="quantErrors">Quantization errors</param>
	/// <param name="multiplier">Multiplier</param>
	/// <returns>New values</returns>
	T[] CreatePixelFromChannelsAndQuantError(T[] oldValues, double[] quantErrors, double multiplier);
}