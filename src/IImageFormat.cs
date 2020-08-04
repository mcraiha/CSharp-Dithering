
/// <summary>
/// Interface for our custom image formats
/// </summary>
public interface IImageFormat
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
	/// <param name="newValues">New values as object array</param>
	void SetPixelChannels(int x, int y, object[] newValues);

	/// <summary>
	/// Get pixel channels of certain coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <returns>Values as object array</returns>
	object[] GetPixelChannels(int x, int y);

	/// <summary>
	/// Get quantization errors per channel
	/// </summary>
	/// <param name="originalPixel">Original pixels</param>
	/// <param name="newPixel">New pixels</param>
	/// <returns>Error values as object array</returns>
	double[] GetQuantErrorsPerChannel(object[] originalPixel, object[] newPixel);

	/// <summary>
	/// Create new values from old values and quantization errors
	/// </summary>
	/// <param name="oldValues">Old values</param>
	/// <param name="quantErrors">Quantization errors</param>
	/// <param name="multiplier">Multiplier</param>
	/// <returns>New values</returns>
	object[] CreatePixelFromChannelsAndQuantError(object[] oldValues, double[] quantErrors, double multiplier);
}