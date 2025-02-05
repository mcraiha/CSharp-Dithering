using NUnit.Framework;
using BigGustave;
using System;
using System.IO;
using System.Collections.Generic;

namespace tests
{
	public class TempImageFormatTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test, Description("Test that Data is correctly loaded as 3d")]
		public void LoadTest3d()
		{
			// Arrange

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);

			Pixel pixel = image.GetPixel(0, 0);
			
			TempImageFormat<byte> test = new TempImageFormat<byte>(ReadTo3DBytes(image));
			byte[] firstPixel = test.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(pixel.R, firstPixel[0]);
			Assert.AreEqual(pixel.G, firstPixel[1]);
			Assert.AreEqual(pixel.B, firstPixel[2]);
		}

		[Test, Description("Test that Data is correctly loaded as 1d")]
		public void LoadTest1d()
		{
			// Arrange

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);

			Pixel pixel = image.GetPixel(0, 0);
			
			TempImageFormat<byte> test = new TempImageFormat<byte>(ReadTo1DBytes(image), image.Width, image.Height, 3);
			byte[] firstPixel = test.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(pixel.R, firstPixel[0]);
			Assert.AreEqual(pixel.G, firstPixel[1]);
			Assert.AreEqual(pixel.B, firstPixel[2]);
		}

		[Test, Description("Test that indexing works equally")]
		public void IndexingTest()
		{
			// Arrange
			int[] indexes = new int[] { 0, 1, 12, 37, 56, 132, 200 };

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);

			TempImageFormat<byte> test3d = new TempImageFormat<byte>(ReadTo3DBytes(image));
			TempImageFormat<byte> test1d = new TempImageFormat<byte>(ReadTo1DBytes(image), image.Width, image.Height, 3);

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

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			byte[] bytes1d = ReadTo1DBytes(image);
			TempImageFormat<byte> test1d_1 = new TempImageFormat<byte>(bytes1d, image.Width, image.Height, 3, createCopy: false);
			TempImageFormat<byte> test1d_2 = new TempImageFormat<byte>(bytes1d, image.Width, image.Height, 3, createCopy: true);
			bytes1d[0] = 0;

			byte[] firstPixel1 = test1d_1.GetPixelChannels(0, 0);
			byte[] firstPixel2 = test1d_2.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(0, firstPixel1[0]);
			Assert.AreNotEqual(0, firstPixel2[0]);
		}

		[Test, Description("Test that raw content works")]
		public void CheckThatRawContentWorks()
		{
			// Arrange

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			byte[] bytes1d = ReadTo1DBytes(image);
			TempImageFormat<byte> test1d_1 = new TempImageFormat<byte>(bytes1d, image.Width, image.Height, 3, createCopy: true);

			// Assert
			Assert.Greater(bytes1d.Length, 1000, "There should be some bytes in image data");
			CollectionAssert.AreEqual(bytes1d, test1d_1.GetRawContent());
		}

		[Test, Description("Test that GetQuantErrorsPerChannel for one channel works")]
		public void CheckThatGetQuantErrorsPerChannelForOneChannelWorks()
		{
			// Arrange
			byte[] imageBytes1d = new byte[1] { 0 };
			byte[] modifiedBytes = new byte[1] { 1 };
			double[] expected = new double[] { -1 };
			double[] actual = new double[1];

			TempImageFormat<byte> test1d = new TempImageFormat<byte>(imageBytes1d, 1, 1, 1);

			// Act
			test1d.GetQuantErrorsPerChannel(imageBytes1d, modifiedBytes, ref actual);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test, Description("Test that GetQuantErrorsPerChannel for three channels works")]
		public void CheckThatGetQuantErrorsPerChannelForThreeChannelsWorks()
		{
			// Arrange
			byte[] imageBytes1d = new byte[3] { 0, 127, 255 };
			byte[] modifiedBytes = new byte[3] { 0, 128, 254 };
			double[] expected = new double[] { 0, -1, 1 };
			double[] actual = new double[3];

			TempImageFormat<byte> test1d = new TempImageFormat<byte>(imageBytes1d, 1, 1, 3);

			// Act
			test1d.GetQuantErrorsPerChannel(imageBytes1d, modifiedBytes, ref actual);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test, Description("Test that ModifyPixelChannelsWithQuantError for one channel works")]
		public void CheckThatModifyPixelChannelsWithQuantErrorForOneChannelWorks()
		{
			// Arrange
			byte[] imageBytes1d = new byte[1] { 0 };
			double[] quantErrors = new double[1] { 1 };
			byte[] expected = new byte[1] { 1 };
			double multiplier = 1.0;
			TempImageFormat<byte> test1d = new TempImageFormat<byte>(imageBytes1d, imageWidth: 1, imageHeight: 1, imageChannelsPerPixel: 1);

			// Act
			test1d.ModifyPixelChannelsWithQuantError(ref imageBytes1d, quantErrors, multiplier);

			// Assert
			CollectionAssert.AreEqual(expected, imageBytes1d);
		}

		[Test, Description("Test that ModifyPixelChannelsWithQuantError for three channels works")]
		public void CheckThatModifyPixelChannelsWithQuantErrorForThreeChannelsWorks()
		{
			// Arrange
			byte[] imageBytes3d = new byte[3] { 30, 127, 200 };
			double[] quantErrors = new double[3] { -10, 0, 20 };
			byte[] expected = new byte[3] { 10, 127, 240 };
			double multiplier = 2.0;
			TempImageFormat<byte> test1d = new TempImageFormat<byte>(imageBytes3d, imageWidth: 1, imageHeight: 1, imageChannelsPerPixel: 3);

			// Act
			test1d.ModifyPixelChannelsWithQuantError(ref imageBytes3d, quantErrors, multiplier);

			// Assert
			CollectionAssert.AreEqual(expected, imageBytes3d);
		}

		private static byte[,,] ReadTo3DBytes(Png bitmap)
		{
			byte[,,] returnValue = new byte[bitmap.Width, bitmap.Height, 3];
			for (int x = 0; x < bitmap.Width; x++)
			{
				for (int y = 0; y < bitmap.Height; y++)
				{
					Pixel pixel = bitmap.GetPixel(x, y);
					returnValue[x, y, 0] = pixel.R;
					returnValue[x, y, 1] = pixel.G;
					returnValue[x, y, 2] = pixel.B;
				}
			}
			return returnValue;
		}

		private static byte[] ReadTo1DBytes(Png bitmap)
		{
			int width = bitmap.Width;
			int height = bitmap.Height;
			int channelsPerPixel = 3;
			byte[] returnValue = new byte[width * height * 3];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Pixel pixel = bitmap.GetPixel(x, y);
					int arrayIndex = y * width * channelsPerPixel + x * channelsPerPixel;
					returnValue[arrayIndex + 0] = pixel.R;
					returnValue[arrayIndex + 1] = pixel.G;
					returnValue[arrayIndex + 2] = pixel.B;
				}
			}
			return returnValue;
		}
	}
}