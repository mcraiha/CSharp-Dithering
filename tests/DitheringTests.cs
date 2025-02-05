using NUnit.Framework;
using BigGustave;
using System;
using System.IO;
using System.Collections.Generic;

namespace tests
{
	public class DitheringTests
	{
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
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

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
			BurkesDitheringRGB<byte> burkes = new BurkesDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long burkesImageChecksum, int burkesImageColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(burkes, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, burkesImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * burkesImageColorCount);
		}

		[Test, Description("Test that FloydSteinbergDithering produces modified output")]
		public void FloydSteinbergDitheringTests()
		{
			// Arrange
			FloydSteinbergDitheringRGB<byte> floydSteinberg = new FloydSteinbergDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long floydSteinbergImageChecksum, int floydSteinbergColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(floydSteinberg, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, floydSteinbergImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * floydSteinbergColorCount);
		}

		[Test, Description("Test that JarvisJudiceNinkeDithering produces modified output")]
		public void JarvisJudiceNinkeDitheringTests()
		{
			// Arrange
			JarvisJudiceNinkeDitheringRGB<byte> jarvisJudiceNinke = new JarvisJudiceNinkeDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long jarvisJudiceNinkeImageChecksum, int jarvisJudiceNinkeColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(jarvisJudiceNinke, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, jarvisJudiceNinkeImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * jarvisJudiceNinkeColorCount);
		}

		[Test, Description("Test that SierraDithering produces modified output")]
		public void SierraDitheringTests()
		{
			// Arrange
			SierraDitheringRGB<byte> sierra = new SierraDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraImageChecksum, int sierraColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(sierra, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraColorCount);
		}

		[Test, Description("Test that SierraLiteDithering produces modified output")]
		public void SierraLiteDitheringTests()
		{
			// Arrange
			SierraLiteDitheringRGB<byte> sierraLite = new SierraLiteDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraLiteImageChecksum, int sierraLiteColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(sierraLite, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraLiteImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraLiteColorCount);
		}

		[Test, Description("Test that SierraTwoRowDithering produces modified output")]
		public void SierraTwoRowDitheringTests()
		{
			// Arrange
			SierraTwoRowDitheringRGB<byte> sierraTwoRow = new SierraTwoRowDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraTwoRowImageChecksum, int sierraTwoRowColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(sierraTwoRow, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraTwoRowImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraTwoRowColorCount);
		}

		[Test, Description("Test that StuckiDithering produces modified output")]
		public void StuckiDitheringTests()
		{
			// Arrange
			StuckiDitheringRGB<byte> stucki = new StuckiDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long stuckiImageChecksum, int stuckiColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(stucki, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, stuckiImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * stuckiColorCount);
		}

		[Test, Description("Test that FakeDithering produces modified output because of color func")]
		public void FakeDitheringTests()
		{
			// Arrange
			FakeDitheringRGB<byte> fake = new FakeDitheringRGB<byte>(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long fakeImageChecksum, int fakeImageColorCount) = DoDitheringAndGetChecksumAndColorCountBytes(fake, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, fakeImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * fakeImageColorCount);
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