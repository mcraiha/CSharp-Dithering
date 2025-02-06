using NUnit.Framework;
using BigGustave;
using System;
using System.IO;
using System.Collections.Generic;

namespace tests
{
	public class DitheringTests
	{
		private static readonly FileStream stream = File.OpenRead("half.png");
		private static readonly Png image = Png.Open(stream);
		private static readonly long originalImageChecksum = GetImageTotalPixelSum(image);
		private static readonly int originalImageColorCount = CountTotalColors(image);

		[SetUp]
		public void Setup()
		{
		}

		[Test, Description("Test that AtkinsonDithering produces modified output")]
		public void AtkinsonDitheringTests()
		{
			// Arrange
			AtkinsonDitheringRGB<byte> atkinsonBytes = new AtkinsonDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			AtkinsonDitheringRGB<double> atkinsonDoubles = new AtkinsonDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long atkinsonImageChecksumBytes, int atkinsonImageColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(atkinsonBytes, image);
			(long atkinsonImageChecksumDoubles, int atkinsonImageColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(atkinsonDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, atkinsonImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, atkinsonImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * atkinsonImageColorCountBytes);
			Assert.Greater(originalImageColorCount, atkinsonImageColorCountDoubles);
		}

		[Test, Description("Test that BurkesDithering produces modified output")]
		public void BurkesDitheringTests()
		{
			// Arrange
			BurkesDitheringRGB<byte> burkesBytes = new BurkesDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			BurkesDitheringRGB<double> burkesDoubles = new BurkesDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long burkesImageChecksumBytes, int burkesImageColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(burkesBytes, image);
			(long burkesImageChecksumDoubles, int burkesImageColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(burkesDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, burkesImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, burkesImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * burkesImageColorCountBytes);
			Assert.Greater(originalImageColorCount, burkesImageColorCountDoubles);
		}

		[Test, Description("Test that FloydSteinbergDithering produces modified output")]
		public void FloydSteinbergDitheringTests()
		{
			// Arrange
			FloydSteinbergDitheringRGB<byte> floydSteinbergBytes = new FloydSteinbergDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			FloydSteinbergDitheringRGB<double> floydSteinbergDoubles = new FloydSteinbergDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long floydSteinbergImageChecksumBytes, int floydSteinbergColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(floydSteinbergBytes, image);
			(long floydSteinbergImageChecksumDoubles, int floydSteinbergColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(floydSteinbergDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, floydSteinbergImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, floydSteinbergImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * floydSteinbergColorCountBytes);
			Assert.Greater(originalImageColorCount, floydSteinbergColorCountDoubles);
		}

		[Test, Description("Test that JarvisJudiceNinkeDithering produces modified output")]
		public void JarvisJudiceNinkeDitheringTests()
		{
			// Arrange
			JarvisJudiceNinkeDitheringRGB<byte> jarvisJudiceNinkeBytes = new JarvisJudiceNinkeDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			JarvisJudiceNinkeDitheringRGB<double> jarvisJudiceNinkeDoubles = new JarvisJudiceNinkeDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long jarvisJudiceNinkeImageChecksumBytes, int jarvisJudiceNinkeColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(jarvisJudiceNinkeBytes, image);
			(long jarvisJudiceNinkeImageChecksumDoubles, int jarvisJudiceNinkeColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(jarvisJudiceNinkeDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, jarvisJudiceNinkeImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, jarvisJudiceNinkeImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * jarvisJudiceNinkeColorCountBytes);
			Assert.Greater(originalImageColorCount, jarvisJudiceNinkeColorCountDoubles);
		}

		[Test, Description("Test that SierraDithering produces modified output")]
		public void SierraDitheringTests()
		{
			// Arrange
			SierraDitheringRGB<byte> sierraBytes = new SierraDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			SierraDitheringRGB<double> sierraDoubles = new SierraDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long sierraImageChecksumBytes, int sierraColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(sierraBytes, image);
			(long sierraImageChecksumDoubles, int sierraColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(sierraDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, sierraImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * sierraColorCountBytes);
			Assert.Greater(originalImageColorCount, sierraColorCountDoubles);
		}

		[Test, Description("Test that SierraLiteDithering produces modified output")]
		public void SierraLiteDitheringTests()
		{
			// Arrange
			SierraLiteDitheringRGB<byte> sierraLiteBytes = new SierraLiteDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			SierraLiteDitheringRGB<double> sierraLiteDoubles = new SierraLiteDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long sierraLiteImageChecksumBytes, int sierraLiteColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(sierraLiteBytes, image);
			(long sierraLiteImageChecksumDoubles, int sierraLiteColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(sierraLiteDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraLiteImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, sierraLiteImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * sierraLiteColorCountBytes);
			Assert.Greater(originalImageColorCount, sierraLiteColorCountDoubles);
		}

		[Test, Description("Test that SierraTwoRowDithering produces modified output")]
		public void SierraTwoRowDitheringTests()
		{
			// Arrange
			SierraTwoRowDitheringRGB<byte> sierraTwoRowBytes = new SierraTwoRowDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			SierraTwoRowDitheringRGB<double> sierraTwoRowDoubles = new SierraTwoRowDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long sierraTwoRowImageChecksumBytes, int sierraTwoRowColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(sierraTwoRowBytes, image);
			(long sierraTwoRowImageChecksumDoubles, int sierraTwoRowColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(sierraTwoRowDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraTwoRowImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, sierraTwoRowImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * sierraTwoRowColorCountBytes);
			Assert.Greater(originalImageColorCount, sierraTwoRowColorCountDoubles);
		}

		[Test, Description("Test that StuckiDithering produces modified output")]
		public void StuckiDitheringTests()
		{
			// Arrange
			StuckiDitheringRGB<byte> stuckiBytes = new StuckiDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			StuckiDitheringRGB<double> stuckiDoubles = new StuckiDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long stuckiImageChecksumBytes, int stuckiColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(stuckiBytes, image);
			(long stuckiImageChecksumDoubles, int stuckiColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(stuckiDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, stuckiImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, stuckiImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * stuckiColorCountBytes);
			Assert.Greater(originalImageColorCount, stuckiColorCountDoubles);
		}

		[Test, Description("Test that FakeDithering produces modified output because of color func")]
		public void FakeDitheringTests()
		{
			// Arrange
			FakeDitheringRGB<byte> fakeBytes = new FakeDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);
			FakeDitheringRGB<double> fakeDoubles = new FakeDitheringRGB<double>(TrueColorBytesToWebSafeColorDoubles);

			// Act
			(long fakeImageChecksumBytes, int fakeImageColorCountBytes) = DoDitheringAndGetChecksumAndColorCountBytes(fakeBytes, image);
			(long fakeImageChecksumDoubles, int fakeImageColorCountDoubles) = DoDitheringAndGetChecksumAndColorCountDoubles(fakeDoubles, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, fakeImageChecksumBytes);
			Assert.AreNotEqual(originalImageChecksum, fakeImageChecksumDoubles);

			Assert.Greater(originalImageColorCount, 10 * fakeImageColorCountBytes);
			Assert.Greater(originalImageColorCount, fakeImageColorCountDoubles);
		}

		private static (long checksum, int colorCount) DoDitheringAndGetChecksumAndColorCountBytes(DitheringBase<byte> dithering, Png image)
		{
			byte[,,] bytes = ReadTo3DBytes(image);

			TempByteImageFormat temp = new TempByteImageFormat(bytes);
			dithering.DoDithering(temp);

			PngBuilder pngBuilder = PngBuilder.Create(image.Width, image.Height, hasAlphaChannel: false);
			WriteToBitmap(pngBuilder, image.Width, image.Height, temp.GetPixelChannels);
			var memoryStream = new MemoryStream();
			pngBuilder.Save(memoryStream);
			memoryStream.Position = 0;
			Png ditheredImage = Png.Open(memoryStream);

			return (GetImageTotalPixelSum(ditheredImage),  CountTotalColors(ditheredImage));
		}

		private static (long checksum, int colorCount) DoDitheringAndGetChecksumAndColorCountDoubles(DitheringBase<double> dithering, Png image)
		{
			double[,,] doubles = ReadTo3DDoubles(image);

			TempDoubleImageFormat temp = new TempDoubleImageFormat(doubles);
			dithering.DoDithering(temp);

			PngBuilder pngBuilder = PngBuilder.Create(image.Width, image.Height, hasAlphaChannel: false);
			WriteToBitmap(pngBuilder, image.Width, image.Height, temp.GetPixelChannels);
			var memoryStream = new MemoryStream();
			pngBuilder.Save(memoryStream);
			memoryStream.Position = 0;
			Png ditheredImage = Png.Open(memoryStream);

			//image.Save("temp123.png");

			return (GetImageTotalPixelSum(ditheredImage),  CountTotalColors(ditheredImage));
		}

		private static void TrueColorBytesToWebSafeColorBytes(in byte[] input, ref byte[] output)
		{
			for (int i = 0; i < input.Length; i++)
			{
				output[i] = (byte)(Math.Round(input[i] / 51.0) * 51);
			}
		}

		private static void TrueColorBytesToWebSafeColorDoubles(in double[] input, ref double[] output)
		{
			for (int i = 0; i < input.Length; i++)
			{
				output[i] = (Math.Round(input[i] * 51.0) / 51.0);
			}
		}

		private static byte[,,] ReadTo3DBytes(Png image)
		{
			byte[,,] returnValue = new byte[image.Width, image.Height, 3];
			for (int x = 0; x < image.Width; x++)
			{
				for (int y = 0; y < image.Height; y++)
				{
					Pixel pixel = image.GetPixel(x, y);
					returnValue[x, y, 0] = pixel.R;
					returnValue[x, y, 1] = pixel.G;
					returnValue[x, y, 2] = pixel.B;
				}
			}
			return returnValue;
		}

		private static double[,,] ReadTo3DDoubles(Png image)
		{
			double[,,] returnValue = new double[image.Width, image.Height, 3];
			for (int x = 0; x < image.Width; x++)
			{
				for (int y = 0; y < image.Height; y++)
				{
					Pixel pixel = image.GetPixel(x, y);
					returnValue[x, y, 0] = pixel.R / 255.0;
					returnValue[x, y, 1] = pixel.G / 255.0;
					returnValue[x, y, 2] = pixel.B / 255.0;
				}
			}
			return returnValue;
		}

		private static void WriteToBitmap(PngBuilder builder, int width, int height, byte[,,] bytes)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Pixel pixel = new Pixel(bytes[x, y, 0], bytes[x, y, 1], bytes[x, y, 2]);
					builder.SetPixel(pixel, x, y);
				}
			}
		}

		private static void WriteToBitmap(PngBuilder builder, int width, int height, double[,,] doubles)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Pixel pixel = new Pixel((byte)(doubles[x, y, 0] * 255.0), (byte)(doubles[x, y, 1] * 255.0), (byte)(doubles[x, y, 2] * 255.0));
					builder.SetPixel(pixel, x, y);
				}
			}
		}

		private static void WriteToBitmap(PngBuilder builder, int width, int height, Func<int, int, byte[]> reader)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					byte[] read = reader(x, y);
					Pixel pixel = new Pixel(read[0], read[1], read[2]);
					builder.SetPixel(pixel, x, y);
				}
			}
		}

		private static void WriteToBitmap(PngBuilder builder, int width, int height, Func<int, int, double[]> reader)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					double[] read = reader(x, y);
					Pixel pixel = new Pixel((byte)(read[0] * 255.0), (byte)(read[1] * 255.0), (byte)(read[2] * 255.0));
					builder.SetPixel(pixel, x, y);
				}
			}
		}

		private static long GetImageTotalPixelSum(Png image)
		{
			long totalSum = 0;

			for (int i = 0; i < image.Width; i++)
			{
				for (int j = 0; j < image.Height; j++)
				{
					Pixel pixel = image.GetPixel(i,j);
					totalSum += (pixel.R + pixel.G + pixel.B);
				}
			}
			
			return totalSum;
		}

		private static int CountTotalColors(Png image)
		{
			HashSet<int> unique = new HashSet<int>();

			for (int i = 0; i < image.Width; i++)
			{
				for (int j = 0; j < image.Height; j++)
				{
					Pixel pixel = image.GetPixel(i, j);
					unique.Add(pixel.GetHashCode());
				}
			}

			return unique.Count;
		}
	}
}