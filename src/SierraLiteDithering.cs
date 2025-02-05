/*
   This file implements error pushing of dithering via (Frankie) Sierra Lite kernel.

   This is free and unencumbered software released into the public domain.
*/
using System;
using System.Numerics;

/// <summary>
/// Sierra lite dithering for RGB
/// </summary>
public sealed class SierraLiteDitheringRGB<T> : DitheringBase<T> where T : INumber<T>
{
	/// <summary>
	/// Constructor for Sierra lite dithering
	/// </summary>
	/// <param name="colorfunc">Color function</param>
	public SierraLiteDitheringRGB(ColorFunction colorfunc) : base(colorfunc, "SierraLite", "_SIEL")
	{

	}

	/// <summary>
	/// Push error method for Sierra lite dithering
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	override protected void PushError(int x, int y, double[] quantError)
	{
		// Push error
		//        X    2/4
		// 1/4   1/4

		int xMinusOne = x - 1;
		int xPlusOne = x + 1;
		int yPlusOne = y + 1;

		// Current row
		int currentRow = y;
		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 2.0 / 4.0);
		}

		// Next row
		currentRow = yPlusOne;
		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 1.0 / 4.0);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 1.0 / 4.0);
		}
	}
}
