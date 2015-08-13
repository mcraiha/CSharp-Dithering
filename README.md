# CSharp-Dithering
CSharp (C#) versions of certain dithering algorithms

## Introduction to this project
This project contains implementations of different dithering algorithms (C#). They use .NET Bitmap class so **System.Drawing** is required (albeit it could be easily replaced). As usual most of the error handling code has been stripped away, so YMMV

## Introduction to dithering
As Wikipedia says *"Dither is an intentionally applied form of noise used to randomize quantization error, preventing large-scale patterns such as color banding in images."*

In this case dithering is used help in color reduction (less banding). This reduction of colors + dithering combo can be used e.g. to reduce file sizes, make artsy images and avoid issues when displaying images on displays that have limited color range.

## Implementation
Inspiration for this project came from [blog post](http://www.tannerhelland.com/4660/dithering-eleven-algorithms-source-code/) made by **Tanner Helland**.

[Program.cs](https://github.com/mcraiha/CSharp-Dithering/blob/master/Program.cs) is a sample program that can be used for testing different dithering methods.

[DitheringBase.cs](https://github.com/mcraiha/CSharp-Dithering/blob/master/DitheringBase.cs) contains the abstract base class that every error pushing dithering implmentation should use.

[FakeDithering.cs](https://github.com/mcraiha/CSharp-Dithering/blob/master/FakeDithering.cs) is "fake" dithering since it doesn't do any dithering. It is used to get image with reduced colors.

Other .cs files are used for different dithering algorithms, and the files are named as **SomeAlgorithm**Dithering.cs

Samples folder contains images that are shown in the end of this Readme file

## Examples
Use Floyd-Steinberg dithering with black or white color reduction
```cs
private static Color BlackOrWhite(Color inputColor)
{
    int luminanceHSL = (Math.Max(inputColor.R, Math.Max(inputColor.G, inputColor.B)) + Math.Min(inputColor.R, Math.Min(inputColor.G, inputColor.B))) / 2;
    if (luminanceHSL < 128)
    {
        return Color.Black;
    }

    return Color.White;
}

DitheringBase method = new FloydSteinbergDithering(BlackOrWhite, useFastMode: true);
Bitmap dithered = method.DoDithering(input);
```
## Usage
You have to always give color reduction method as parameter for dither constructor. You can dither multiple images with one instance by calling DoDithering again with different input.

## What is useFastMode
Since [Bitmap Class](https://msdn.microsoft.com/en-us/library/system.drawing.bitmap(v=vs.110).aspx) in .NET has very slow SetPixel method, it is much faster to use LockBits and InteropServices.Marshal.Copy. Unfortunately in certain situations those might be unusable so that is why GetPixel method is the backup method.

So it is better (perfomance wise) to call constructors with **useFastMode: true** if possible.

## License
Text in this document and source code files are released into the public domain. See [PUBLICDOMAIN](https://github.com/mcraiha/CSharp-Dithering/blob/master/PUBLICDOMAIN) file.

Parrot image (half.png) is made from image that comes from [Kodak Lossless True Color Image Suite](http://r0k.us/graphics/kodak/) and it doesn't have any specific license.

## Samples
I took the famous [parrot image](http://r0k.us/graphics/kodak/kodim23.html) and reduced its size. Then I ran the image with all dithering methods and using [Web safe colors](https://en.wikipedia.org/wiki/Web_colors#Web-safe_colors) as palette.

![Original](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/half.png)

Original
<br><br>
![Floyd-Steinberg](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_FS.png)

Floyd-Steinberg
<br><br>
![Jarvis-Judice-Ninke](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_JJN.png)

Jarvis-Judice-Ninke
<br><br>
![Stucki](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_STU.png)

Stucki
<br><br>
![Atkinson](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_ATK.png)

Atkinson
<br><br>
![Burkes](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_BUR.png)

Burkes
<br><br>
![Sierra](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_SIE.png)

Sierra
<br><br>
![Sierra Two Row](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_SIE2R.png)

Sierra Two Row
<br><br>
![Sierra Lite](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_SIEL.png)

Sierra Lite
<br><br>
![None](https://github.com/mcraiha/CSharp-Dithering/blob/master/Samples/dither_NONE.png)

No dithering, just color reduction
