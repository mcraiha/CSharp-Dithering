using NUnit.Framework;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;
using System.IO;
using System.Collections.Generic;

namespace tests
{
	public class TempByteImageFormatTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test, Description("Test that Data is correctly loaded as 3d")]
		public void LoadTest3d()
		{
			// Arrange
			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			Color firstColor = image.GetPixel(0, 0);
			
			TempByteImageFormat test = new TempByteImageFormat(ReadTo3DBytes(image));
			byte[] firstPixel = test.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(firstColor.R, firstPixel[0]);
			Assert.AreEqual(firstColor.G, firstPixel[1]);
			Assert.AreEqual(firstColor.B, firstPixel[2]);
		}

		[Test, Description("Test that Data is correctly loaded as 1d")]
		public void LoadTest1d()
		{
			// Arrange
			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			Color firstColor = image.GetPixel(0, 0);
			
			TempByteImageFormat test = new TempByteImageFormat(ReadTo1DBytes(image), image.Width, image.Height, 3);
			byte[] firstPixel = test.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(firstColor.R, firstPixel[0]);
			Assert.AreEqual(firstColor.G, firstPixel[1]);
			Assert.AreEqual(firstColor.B, firstPixel[2]);
		}

		[Test, Description("Test that indexing works equally")]
		public void IndexingTest()
		{
			// Arrange
			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			int[] indexes = new int[] { 0, 1, 12, 37, 56, 132, 200 };

			// Act
			var image = new Bitmap(pngStream);

			TempByteImageFormat test3d = new TempByteImageFormat(ReadTo3DBytes(image));
			TempByteImageFormat test1d = new TempByteImageFormat(ReadTo1DBytes(image), image.Width, image.Height, 3);

			// Assert
			for (int x = 0; x < indexes.Length; x++)
			{
				for (int y = 0; y < indexes.Length; y++)
				{
					byte[] pixel3d = test3d.GetPixelChannels(x, y);
					byte[] pixel1d = test1d.GetPixelChannels(x, y);

					CollectionAssert.AreEqual(pixel3d, pixel1d, $"Pixels at {x} x {y} should be equal");
				}
			}
		}

		[Test, Description("Test that 1d copy works")]
		public void CheckThat1dCopyWorks()
		{
			// Arrange
			FileStream pngStream = new FileStream("half.png", FileMode.Open, FileAccess.Read);

			// Act
			var image = new Bitmap(pngStream);
			byte[] bytes1d = ReadTo1DBytes(image);
			TempByteImageFormat test1d_1 = new TempByteImageFormat(bytes1d, image.Width, image.Height, 3, createCopy: false);
			TempByteImageFormat test1d_2 = new TempByteImageFormat(bytes1d, image.Width, image.Height, 3, createCopy: true);
			bytes1d[0] = 0;

			byte[] firstPixel1 = test1d_1.GetPixelChannels(0, 0);
			byte[] firstPixel2 = test1d_2.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(0, firstPixel1[0]);
			Assert.AreNotEqual(0, firstPixel2[0]);
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

		private static byte[] ReadTo1DBytes(Bitmap bitmap)
		{
			int width = bitmap.Width;
			int height = bitmap.Height;
			int channelsPerPixel = 3;
			byte[] returnValue = new byte[width * height * 3];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Color color = bitmap.GetPixel(x, y);
					int arrayIndex = y * width * channelsPerPixel + x * channelsPerPixel;
					returnValue[arrayIndex + 0] = color.R;
					returnValue[arrayIndex + 1] = color.G;
					returnValue[arrayIndex + 2] = color.B;
				}
			}
			return returnValue;
		}
	}
}