using System.Numerics;

public interface IDitherCommon<T>  where T : INumber<T>, IMinMaxValue<T>
{
	string GetMethodName();

	string GetFilenameAddition();

	IImageFormat<T> DoDithering(IImageFormat<T> input);
}