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

		[Test, Description("Test that Data is correctly loaded")]
		public void LoadTest()
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
	}
}