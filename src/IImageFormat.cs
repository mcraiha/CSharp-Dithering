
public interface IImageFormat
{
	int GetWidth();
	int GetHeight();
	void SetPixelChannels(int x, int y, object[] newValues);
	object[] GetPixelChannels(int x, int y);
	double[] GetQuantErrorsPerChannel(object[] originalPixel, object[] newPixel);
	object[] CreatePixelFromChannelsAndQuantError(object[] oldValues, double[] quantErrors, double multiplier);
}