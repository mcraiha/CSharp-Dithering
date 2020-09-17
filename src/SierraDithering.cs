/*
   This file implements error pushing of dithering via (Frankie) Sierra kernel.

   This is free and unencumbered software released into the public domain.
*/

using System;

/// <summary>
/// Sierra dithering for RGB bytes
/// </summary>
public sealed class SierraDitheringRGBByte : DitheringBase<byte>
{
	/// <summary>
	/// Constructor for Sierra dithering
	/// </summary>
	/// <param name="colorfunc">Color function</param>
	public SierraDitheringRGBByte(ColorFunction colorfunc) : base(colorfunc, "Sierra", "_SIE")
	{

	}

	/// <summary>
	/// Push error method for Sierra dithering
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	override protected void PushError(int x, int y, double[] quantError)
	{
		// Push error
		//                X     5/32   3/32
		// 2/32   4/32   5/32   4/32   2/32
		//        2/32   3/32   2/32

		int xMinusOne = x - 1;
		int xMinusTwo = x - 2;
		int xPlusOne = x + 1;
		int xPlusTwo = x + 2;
		int yPlusOne = y + 1;
		int yPlusTwo = y + 2;

		// Current row
		int currentRow = y;
		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 5.0 / 32.0);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 3.0 / 32.0);
		}

		// Next row
		currentRow = yPlusOne;
		if (this.IsValidCoordinate(xMinusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 2.0 / 32.0);
		}

		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 4.0 / 32.0);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 5.0 / 32.0);
		}

		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 4.0 / 32.0);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 2.0 / 32.0);
		}

		// Next row
		currentRow = yPlusTwo;
		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 2.0 / 32.0);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 3.0 / 32.0);
		}

		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 2.0 / 32.0);
		}
	}
}

