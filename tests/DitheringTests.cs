using NUnit.Framework;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long atkinsonImageChecksum, int atkinsonImageColorCount) = DoDitheringAndGetChecksumAndColorCount(atkinson, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, atkinsonImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * atkinsonImageColorCount);
		}

		[Test, Description("Test that BurkesDithering produces modified output")]
		public void BurkesDitheringTest()
		{
			// Arrange
			BurkesDitheringRGBByte burkes = new BurkesDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long burkesImageChecksum, int burkesImageColorCount) = DoDitheringAndGetChecksumAndColorCount(burkes, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, burkesImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * burkesImageColorCount);
		}

		[Test, Description("Test that FloydSteinbergDithering produces modified output")]
		public void FloydSteinbergDitheringTest()
		{
			// Arrange
			FloydSteinbergDitheringRGBByte floydSteinberg = new FloydSteinbergDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long floydSteinbergImageChecksum, int floydSteinbergColorCount) = DoDitheringAndGetChecksumAndColorCount(floydSteinberg, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, floydSteinbergImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * floydSteinbergColorCount);
		}

		[Test, Description("Test that JarvisJudiceNinkeDithering produces modified output")]
		public void JarvisJudiceNinkeDitheringTest()
		{
			// Arrange
			JarvisJudiceNinkeDitheringRGBByte jarvisJudiceNinke = new JarvisJudiceNinkeDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long jarvisJudiceNinkeImageChecksum, int jarvisJudiceNinkeColorCount) = DoDitheringAndGetChecksumAndColorCount(jarvisJudiceNinke, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, jarvisJudiceNinkeImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * jarvisJudiceNinkeColorCount);
		}

		[Test, Description("Test that SierraDithering produces modified output")]
		public void SierraDitheringTest()
		{
			// Arrange
			SierraDitheringRGBByte sierra = new SierraDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraImageChecksum, int sierraColorCount) = DoDitheringAndGetChecksumAndColorCount(sierra, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraColorCount);
		}

		[Test, Description("Test that SierraLiteDithering produces modified output")]
		public void SierraLiteDitheringTest()
		{
			// Arrange
			SierraLiteDitheringRGBByte sierraLite = new SierraLiteDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraLiteImageChecksum, int sierraLiteColorCount) = DoDitheringAndGetChecksumAndColorCount(sierraLite, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraLiteImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraLiteColorCount);
		}

		[Test, Description("Test that SierraTwoRowDithering produces modified output")]
		public void SierraTwoRowDitheringTest()
		{
			// Arrange
			SierraTwoRowDitheringRGBByte sierraTwoRow = new SierraTwoRowDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long sierraTwoRowImageChecksum, int sierraTwoRowColorCount) = DoDitheringAndGetChecksumAndColorCount(sierraTwoRow, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, sierraTwoRowImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * sierraTwoRowColorCount);
		}

		[Test, Description("Test that StuckiDithering produces modified output")]
		public void StuckiDitheringTest()
		{
			// Arrange
			StuckiDitheringRGBByte stucki = new StuckiDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long stuckiImageChecksum, int stuckiColorCount) = DoDitheringAndGetChecksumAndColorCount(stucki, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, stuckiImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * stuckiColorCount);
		}

		[Test, Description("Test that FakeDithering produces modified output because of color func")]
		public void FakeDitheringTest()
		{
			// Arrange
			FakeDitheringRGBByte fake = new FakeDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			long originalImageChecksum = GetImageTotalPixelSum(image);
			int originalImageColorCount = CountTotalColors(image);

			(long fakeImageChecksum, int fakeImageColorCount) = DoDitheringAndGetChecksumAndColorCount(fake, new Bitmap(image));

			// Assert
			Assert.AreNotEqual(originalImageChecksum, fakeImageChecksum);
			Assert.Greater(originalImageColorCount, 10 * fakeImageColorCount);
		}

		private static (long checksum, int colorCount) DoDitheringAndGetChecksumAndColorCount(DitheringBase<byte> dithering, Bitmap bitmap)
		{
			using(var image = bitmap)
			{
				byte[,,] bytes = ReadTo3DBytes(image);

				TempByteImageFormat temp = new TempByteImageFormat(bytes);
				dithering.DoDithering(temp);

				WriteToBitmap(image, temp.GetPixelChannels);
				//image.Save("temp123.png");

				return (GetImageTotalPixelSum(image),  CountTotalColors(image));
			}
		}

		private static void TrueColorBytesToWebSafeColorBytes(in byte[] input, ref byte[] output)
		{
			for (int i = 0; i < input.Length; i++)
			{
				output[i] = (byte)(Math.Round(input[i] / 51.0) * 51);
			}
		}

		private static byte[,,] ReadTo3DBytes(Bitmap bitmap)
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

		private static void WriteToBitmap(Bitmap bitmap, byte[,,] bytes)
		{
			for (int x = 0; x < bitmap.Width; x++)
			{
				for (int y = 0; y < bitmap.Height; y++)
				{
					Color color = Color.FromArgb(bytes[x, y, 0], bytes[x, y, 1], bytes[x, y, 2]);
					bitmap.SetPixel(x, y, color);
				}
			}
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

		private static long GetImageTotalPixelSum(Bitmap bitmap)
		{
			long totalSum = 0;

			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					Color pixel = bitmap.GetPixel(i,j);
					totalSum += (pixel.R + pixel.G + pixel.B);
				}
			}
			
			return totalSum;
		}

		private static int CountTotalColors(Bitmap bitmap)
		{
			HashSet<int> unique = new HashSet<int>();

			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					Color pixel = bitmap.GetPixel(i,j);
					unique.Add(pixel.GetHashCode());
				}
			}

			return unique.Count;
		}
	}
}