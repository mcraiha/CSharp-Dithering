using NUnit.Framework;
using BigGustave;
using System;
using System.IO;
using System.Collections.Generic;

namespace tests
{
	public class TempDoubleImageFormatTests
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
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			Pixel firstColor = image.GetPixel(0, 0);
			
			TempDoubleImageFormat test = new TempDoubleImageFormat(ReadTo3DDoubles(image));
			double[] firstPixel = test.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(firstColor.R / byteMax, firstPixel[0]);
			Assert.AreEqual(firstColor.G / byteMax, firstPixel[1]);
			Assert.AreEqual(firstColor.B / byteMax, firstPixel[2]);
		}

		[Test, Description("Test that Data is correctly loaded as 1d")]
		public void LoadTest1d()
		{
			// Arrange

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			Pixel firstColor = image.GetPixel(0, 0);
			
			TempDoubleImageFormat test = new TempDoubleImageFormat(ReadTo1DDoubles(image), image.Width, image.Height, 3);
			double[] firstPixel = test.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(firstColor.R / byteMax, firstPixel[0]);
			Assert.AreEqual(firstColor.G / byteMax, firstPixel[1]);
			Assert.AreEqual(firstColor.B / byteMax, firstPixel[2]);
		}

		[Test, Description("Test that indexing works equally")]
		public void IndexingTest()
		{
			// Arrange
			int[] indexes = new int[] { 0, 1, 12, 37, 56, 132, 200 };

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			Pixel firstColor = image.GetPixel(0, 0);

			TempDoubleImageFormat test3d = new TempDoubleImageFormat(ReadTo3DDoubles(image));
			TempDoubleImageFormat test1d = new TempDoubleImageFormat(ReadTo1DDoubles(image), image.Width, image.Height, 3);

			// Assert
			for (int x = 0; x < indexes.Length; x++)
			{
				for (int y = 0; y < indexes.Length; y++)
				{
					double[] pixel3d = test3d.GetPixelChannels(x, y);
					double[] pixel1d = test1d.GetPixelChannels(x, y);

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

			double[] doubles1d = ReadTo1DDoubles(image);
			TempDoubleImageFormat test1d_1 = new TempDoubleImageFormat(doubles1d, image.Width, image.Height, 3, createCopy: false);
			TempDoubleImageFormat test1d_2 = new TempDoubleImageFormat(doubles1d, image.Width, image.Height, 3, createCopy: true);
			doubles1d[0] = 0.0;

			double[] firstPixel1 = test1d_1.GetPixelChannels(0, 0);
			double[] firstPixel2 = test1d_2.GetPixelChannels(0, 0);

			// Assert
			Assert.AreEqual(0.0, firstPixel1[0]);
			Assert.AreNotEqual(0.0, firstPixel2[0]);
		}

		[Test, Description("Test that raw content works")]
		public void CheckThatRawContentWorks()
		{
			// Arrange

			// Act
			var stream = File.OpenRead("half.png");
			Png image = Png.Open(stream);
			double[] doubles1d = ReadTo1DDoubles(image);
			TempDoubleImageFormat test1d_1 = new TempDoubleImageFormat(doubles1d, image.Width, image.Height, 3, createCopy: true);

			// Assert
			Assert.Greater(doubles1d.Length, 1000, "There should be some bytes in image data");
			CollectionAssert.AreEqual(doubles1d, test1d_1.GetRawContent());
		}

		[Test, Description("Test that GetQuantErrorsPerChannel for one channel works")]
		public void CheckThatGetQuantErrorsPerChannelForOneChannelWorks()
		{
			// Arrange
			double[] imageDoubles = new double[1] { 0 };
			double[] modifiedBytes = new double[1] { 1 };
			double[] expected = new double[] { -1 };
			double[] actual = new double[1];
			TempDoubleImageFormat test1d = new TempDoubleImageFormat(imageDoubles, imageWidth: 1, imageHeight: 1, imageChannelsPerPixel: 1);

			// Act
			test1d.GetQuantErrorsPerChannel(imageDoubles, modifiedBytes, ref actual);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}


		[Test, Description("Test that GetQuantErrorsPerChannel for three channels works")]
		public void CheckThatGetQuantErrorsPerChannelForThreeChannelsWorks()
		{
			// Arrange
			double[] imageDoubles = new double[3] { 0, 127, 255 };
			double[] modifiedBytes = new double[3] { 0, 128, 254 };
			double[] expected = new double[] { 0, -1, 1 };
			double[] actual = new double[3];
			TempDoubleImageFormat test1d = new TempDoubleImageFormat(imageDoubles, imageWidth: 1, imageHeight: 1, imageChannelsPerPixel: 3);

			// Act
			test1d.GetQuantErrorsPerChannel(imageDoubles, modifiedBytes, ref actual);

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[Test, Description("Test that ModifyPixelChannelsWithQuantError for one channel works")]
		public void CheckThatModifyPixelChannelsWithQuantErrorForOneChannelWorks()
		{
			// Arrange
			double[] imageDoubles = new double[1] { 1 };
			double[] quantErrors = new double[1] { -0.25 };
			double[] expected = new double[1] { 0.75 };
			double multiplier = 1.0;
			TempDoubleImageFormat test1d = new TempDoubleImageFormat(imageDoubles, imageWidth: 1, imageHeight: 1, imageChannelsPerPixel: 1);

			// Act
			test1d.ModifyPixelChannelsWithQuantError(ref imageDoubles, quantErrors, multiplier);

			// Assert
			CollectionAssert.AreEqual(expected, imageDoubles);
		}

		[Test, Description("Test that ModifyPixelChannelsWithQuantError for three channels works")]
		public void CheckThatModifyPixelChannelsWithQuantErrorForThreeChannelsWorks()
		{
			// Arrange
			double[] imageDoubles = new double[3] { 1, 0, 0.25 };
			double[] quantErrors = new double[3] { -0.25, 0, 0.25 };
			double[] expected = new double[3] { 0.25, 0, 1.0 };
			double multiplier = 3.0;
			TempDoubleImageFormat test1d = new TempDoubleImageFormat(imageDoubles, imageWidth: 1, imageHeight: 1, imageChannelsPerPixel: 3);

			// Act
			test1d.ModifyPixelChannelsWithQuantError(ref imageDoubles, quantErrors, multiplier);

			// Assert
			CollectionAssert.AreEqual(expected, imageDoubles);
		}

		private static readonly double byteMax = byte.MaxValue / 1.0;
		private static double[,,] ReadTo3DDoubles(Png image)
		{
			double[,,] returnValue = new double[image.Width, image.Height, 3];
			for (int x = 0; x < image.Width; x++)
			{
				for (int y = 0; y < image.Height; y++)
				{
					Pixel color = image.GetPixel(x, y);
					returnValue[x, y, 0] = color.R / byteMax;
					returnValue[x, y, 1] = color.G / byteMax;
					returnValue[x, y, 2] = color.B / byteMax;
				}
			}
			return returnValue;
		}

		private static double[] ReadTo1DDoubles(Png image)
		{
			int width = image.Width;
			int height = image.Height;
			int channelsPerPixel = 3;
			double[] returnValue = new double[width * height * 3];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Pixel color = image.GetPixel(x, y);
					int arrayIndex = y * width * channelsPerPixel + x * channelsPerPixel;
					returnValue[arrayIndex + 0] = color.R / byteMax;
					returnValue[arrayIndex + 1] = color.G / byteMax;
					returnValue[arrayIndex + 2] = color.B / byteMax;
				}
			}
			return returnValue;
		}
	}
}