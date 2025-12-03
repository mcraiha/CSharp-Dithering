using System.Numerics;

/// <summary>
/// Common interface for all dithering classes 
/// </summary>
/// <typeparam name="T">T must implement INumber and IMinMaxValue</typeparam>
public interface IDitherCommon<T>  where T : INumber<T>, IMinMaxValue<T>
{
	/// <summary>
	/// Get dithering method name
	/// </summary>
	/// <returns>Name as string</returns>
	string GetMethodName();

	/// <summary>
	/// Get file name addition for dithering method
	/// </summary>
	/// <returns></returns>
	string GetFilenameAddition();

	/// <summary>
	/// Do dithering
	/// </summary>
	/// <param name="input">Input image</param>
	/// <returns>Dithered image</returns>
	IImageFormat<T> DoDithering(IImageFormat<T> input);
}