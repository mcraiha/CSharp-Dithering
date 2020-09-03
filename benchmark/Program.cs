using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace benchmark
{
	public class TempByteImageFormat1dvs3d
	{
		private const int dimension = 256;
		private TempByteImageFormat oneD = new TempByteImageFormat(new byte[dimension * dimension * 3], dimension, dimension, 3);

		private TempByteImageFormat threeD = new TempByteImageFormat(new byte[dimension, dimension, 3]);

		FakeDitheringRGBByte fake = new FakeDitheringRGBByte(TrueColorBytesToWebSafeColorBytes);

		public TempByteImageFormat1dvs3d()
		{
			
		}

		[Benchmark]
		public void onedee() => fake.DoDithering(oneD);

		[Benchmark]
		public void threedee() => fake.DoDithering(threeD);

		private static void TrueColorBytesToWebSafeColorBytes(in byte[] input, ref byte[] output)
		{
			for (int i = 0; i < input.Length; i++)
			{
				output[i] = (byte)(Math.Round(input[i] / 51.0) * 51);
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<TempByteImageFormat1dvs3d>();
		}
	}
}
