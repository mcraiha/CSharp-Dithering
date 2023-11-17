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
		public void AtkinsonDitheringTest()
		{
			// Arrange
			AtkinsonDitheringRGBByte atkinson = new AtkinsonDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long atkinsonImageChecksum, int atkinsonImageColorCount) = DoDitheringAndGetChecksumAndColorCount(atkinson, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, atkinsonImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * atkinsonImageColorCount);
		}

		[Test, Description("Test that BurkesDithering produces modified output")]
		public void BurkesDitheringTest()
		{
			// Arrange
			BurkesDitheringRGBByte burkes = new BurkesDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long burkesImageChecksum, int burkesImageColorCount) = DoDitheringAndGetChecksumAndColorCount(burkes, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, burkesImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * burkesImageColorCount);
		}

		[Test, Description("Test that FloydSteinbergDithering produces modified output")]
		public void FloydSteinbergDitheringTest()
		{
			// Arrange
			FloydSteinbergDitheringRGBByte floydSteinberg = new FloydSteinbergDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long floydSteinbergImageChecksum, int floydSteinbergColorCount) = DoDitheringAndGetChecksumAndColorCount(floydSteinberg, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, floydSteinbergImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * floydSteinbergColorCount);
		}

		[Test, Description("Test that JarvisJudiceNinkeDithering produces modified output")]
		public void JarvisJudiceNinkeDitheringTest()
		{
			// Arrange
			JarvisJudiceNinkeDitheringRGBByte jarvisJudiceNinke = new JarvisJudiceNinkeDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long jarvisJudiceNinkeImageChecksum, int jarvisJudiceNinkeColorCount) = DoDitheringAndGetChecksumAndColorCount(jarvisJudiceNinke, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, jarvisJudiceNinkeImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * jarvisJudiceNinkeColorCount);
		}

		[Test, Description("Test that SierraDithering produces modified output")]
		public void SierraDitheringTest()
		{
			// Arrange
			SierraDitheringRGBByte sierra = new SierraDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraImageChecksum, int sierraColorCount) = DoDitheringAndGetChecksumAndColorCount(sierra, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraColorCount);
		}

		[Test, Description("Test that SierraLiteDithering produces modified output")]
		public void SierraLiteDitheringTest()
		{
			// Arrange
			SierraLiteDitheringRGBByte sierraLite = new SierraLiteDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraLiteImageChecksum, int sierraLiteColorCount) = DoDitheringAndGetChecksumAndColorCount(sierraLite, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraLiteImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraLiteColorCount);
		}

		[Test, Description("Test that SierraTwoRowDithering produces modified output")]
		public void SierraTwoRowDitheringTest()
		{
			// Arrange
			SierraTwoRowDitheringRGBByte sierraTwoRow = new SierraTwoRowDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraTwoRowImageChecksum, int sierraTwoRowColorCount) = DoDitheringAndGetChecksumAndColorCount(sierraTwoRow, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraTwoRowImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraTwoRowColorCount);
		}

		[Test, Description("Test that StuckiDithering produces modified output")]
		public void StuckiDitheringTest()
		{
			// Arrange
			StuckiDitheringRGBByte stucki = new StuckiDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long stuckiImageChecksum, int stuckiColorCount) = DoDitheringAndGetChecksumAndColorCount(stucki, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, stuckiImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * stuckiColorCount);
		}

		[Test, Description("Test that FakeDithering produces modified output because of color func")]
		public void FakeDitheringTest()
		{
			// Arrange
			FakeDitheringRGBByte fake = new FakeDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long fakeImageChecksum, int fakeImageColorCount) = DoDitheringAndGetChecksumAndColorCount(fake, image);

			// Assert
			Assert.AreNotEqual(originalImageChecksum, fakeImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * fakeImageColorCount);
		}

		private static (long checksum, int colorCount) DoDitheringAndGetChecksumAndColorCount(DitheringBase<byte> dithering, Png image)
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
					Pixel pixel = image.GetPixel(i,j);
					unique.Add(pixel.GetHashCode());
				}
			}

			return unique.Count;
		}
	}
}