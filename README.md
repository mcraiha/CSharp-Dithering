# CSharp-Dithering
CSharp (C#) versions of certain dithering algorithms. This project is .NET Standard 2.0 compatible, managed and available as Nuget!

## Build status
![](https://github.com/mcraiha/CSharp-Dithering/workflows/.NET%20Core/badge.svg)

## Introduction to this project
This project contains implementations of different dithering algorithms (C#). They can be used with any graphics/image API.

## Introduction to dithering
As Wikipedia says *"Dither is an intentionally applied form of noise used to randomize quantization error, preventing large-scale patterns such as color banding in images."*

In this case dithering is used help in color reduction (less banding). This reduction of colors + dithering combo can be used e.g. to reduce file sizes, make artsy images and avoid issues when displaying images on displays that have limited color range.

## Implementation
Inspiration for this project came from [blog post](http://www.tannerhelland.com/4660/dithering-eleven-algorithms-source-code/) made by **Tanner Helland**.

[DitheringBase.cs](https://github.com/mcraiha/CSharp-Dithering/blob/master/src/DitheringBase.cs) contains the abstract base class that every error pushing dithering implmentation should use.

[FakeDithering.cs](https://github.com/mcraiha/CSharp-Dithering/blob/master/src/FakeDithering.cs) is "fake" dithering since it doesn't do any dithering. It is used to get image with reduced colors.

Other .cs files are used for different dithering algorithms, and the files are named as **SomeAlgorithm**Dithering.cs

[Samples folder](https://github.com/mcraiha/CSharp-Dithering/blob/master/samples) contains images that are shown in the end of this Readme file

## Examples
Use Atkinson dithering with web safe color reduction for 24 bit PNG input with System.Drawing
```cs
public void DoAtkinsonDithering()
{
    AtkinsonDitheringRGBByte atkinson = new AtkinsonDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

    using(FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read))
    using(var image = new Bitmap(pngStream))
    {
        byte[,,] bytes = ReadBitmapToColorBytes(image);

        TempByteImageFormat temp = new TempByteImageFormat(bytes);
        temp = (TempByteImageFormat)atkinson.DoDithering(temp);

        WriteToBitmap(image, temp.GetPixelChannels);

        image.Save("test.png");
    }
}

private static object[] TrueColorBytesToWebSafeColorBytes(object[] input)
{
    object[] returnArray = new object[input.Length];
    for (int i = 0; i < returnArray.Length; i++)
    {
        returnArray[i] = (byte)(Math.Round((byte)input[i] / 51.0) * 51);
    }
    
    return returnArray;
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

private static void WriteToBitmap(Bitmap bitmap, Func<int, int, object[]> reader)
{
    for (int x = 0; x < bitmap.Width; x++)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            object[] read = reader(x, y);
            Color color = Color.FromArgb((byte)read[0], (byte)read[1], (byte)read[2]);
            bitmap.SetPixel(x, y, color);
        }
    }
}
```
## Usage
You have to always give color reduction method as parameter for dither constructor. You can dither multiple images with one instance by calling DoDithering again with different input.

## Wasn't this .NET Framework project?
Yes, but time moves on...

## License
Text in this document and source code files are released into the public domain. See [PUBLICDOMAIN](https://github.com/mcraiha/CSharp-Dithering/blob/master/PUBLICDOMAIN) file.

Parrot image (half.png) is made from image that comes from [Kodak Lossless True Color Image Suite](http://r0k.us/graphics/kodak/) and it doesn't have any specific license.

## Samples
I took the famous [parrot image](http://r0k.us/graphics/kodak/kodim23.html) and reduced its size. Then I ran the image (which has 64655 different colors) with all dithering methods and using [Web safe colors](https://en.wikipedia.org/wiki/Web_colors#Web-safe_colors) as palette (216 colors). 

![Original](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/half.png)

Original
<br><br><br>
![Floyd-Steinberg](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_FS.png)

Floyd-Steinberg
<br><br><br>
![Jarvis-Judice-Ninke](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_JJN.png)

Jarvis-Judice-Ninke
<br><br><br>
![Stucki](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_STU.png)

Stucki
<br><br><br>
![Atkinson](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_ATK.png)

Atkinson
<br><br><br>
![Burkes](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_BUR.png)

Burkes
<br><br><br>
![Sierra](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_SIE.png)

Sierra
<br><br><br>
![Sierra Two Row](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_SIE2R.png)

Sierra Two Row
<br><br><br>
![Sierra Lite](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_SIEL.png)

Sierra Lite
<br><br><br>
![None](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_NONE.png)

No dithering, just color reduction
