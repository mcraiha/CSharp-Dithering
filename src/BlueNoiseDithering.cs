/*
   This file implements blue noise dithering

   This is free and unencumbered software released into the public domain.
*/
using System.Numerics;

/// <summary>
/// Blue noise dithering.
/// </summary>
public sealed partial class BlueNoiseDitheringRGB<T> : DitheringNoiseBase<T> where T : INumber<T>, IMinMaxValue<T>
{
	/// <summary>
	/// Constructor for blue noise dithering (no dither, just color reduction)
	/// </summary>
	/// <param name="colorfunc"></param>
	/// <returns></returns>
	public BlueNoiseDitheringRGB(ColorFunction colorfunc) : base(colorfunc, new MaskBitmap<T>(byteMask, 64, 64, 4), "Blue noise", "_BN")
	{
		
	}

	/// <summary>
	/// Push error method for blue noise dithering
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	override protected void PushError(int x, int y, double[] quantError)
	{
		// Don't do anything, because mask does all the work
	}
}
