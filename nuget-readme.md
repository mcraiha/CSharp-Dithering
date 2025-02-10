# CSharp-Dithering

CSharp (C#) versions of certain dithering algorithms. This project is .NET 8 compatible, managed and available as Nuget!

## Examples

Use Atkinson dithering with web safe color reduction for 24 bit PNG input with System.Drawing (this example is Windows only)

```cs
public void DoAtkinsonDithering()
{
    AtkinsonDitheringRGB<byte> atkinson = new AtkinsonDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

    using(FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read))
    using(var image = new Bitmap(pngStream))
    {
        byte[,,] bytes = ReadBitmapToColorBytes(image);

        TempByteImageFormat temp = new TempByteImageFormat(bytes);
        atkinson.DoDithering(temp);

        WriteToBitmap(image, temp.GetPixelChannels);

        image.Save("test.png");
    }
}

private static void TrueColorBytesToWebSafeColorBytes(in byte[] input, ref byte[] output)
{
    for (int i = 0; i < input.Length; i++)
    {
        output[i] = (byte)(Math.Round(input[i] / 51.0) * 51);
    }
}

private static byte[,,] ReadBitmapToColorBytes(Bitmap bitmap)
{
    byte[,,] returnValue = new byte[bitmap.Width, bitmap.Height, 3];
    for (int x = 0; x < bitmap.Width; x++)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            Color color = bitmap.GetPixel(x, y);
            returnValue[x, y, 0] = color.R;
            returnValue[x, y, 1] = color.G;
            returnValue[x, y, 2] = color.B;
        }
    }
    return returnValue;
}

private static void WriteToBitmap(Bitmap bitmap, Func<int, int, byte[]> reader)
{
    for (int x = 0; x < bitmap.Width; x++)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            byte[] read = reader(x, y);
            Color color = Color.FromArgb(read[0], read[1], read[2]);
            bitmap.SetPixel(x, y, color);
        }
    }
}
```