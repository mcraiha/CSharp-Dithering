/*
   This file implements error pushing of dithering via (Frankie) Sierra Two Row kernel.

   This is free and unencumbered software released into the public domain.
*/
using System;

/// <summary>
/// Sierra two row dithering for RGB bytes
/// </summary>
public sealed class SierraTwoRowDitheringRGBByte : DitheringBase<byte>
{
	/// <summary>
	/// Constructor for Sierra two row dithering
	/// </summary>
	/// <param name="colorfunc">Color function</param>
	public SierraTwoRowDitheringRGBByte(ColorFunction colorfunc) : base(colorfunc, "SierraTwoRow", "_SIE2R")
	{

	}

	/// <summary>
	/// Push error method for Sierra two row dithering
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	override protected void PushError(int x, int y, double[] quantError)
	{
		// Push error
		//                X     4/16   3/16
		// 1/16   2/16   3/16   2/16   1/16

		int xMinusOne = x - 1;
		int xMinusTwo = x - 2;
		int xPlusOne = x + 1;
		int xPlusTwo = x + 2;
		int yPlusOne = y + 1;

		// Current row
		int currentRow = y;
		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 4.0 / 16.0);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 3.0 / 16.0);
		}

		// Next row
		currentRow = yPlusOne;
		if (this.IsValidCoordinate(xMinusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 1.0 / 16.0);
		}

		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 2.0 / 16.0);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 3.0 / 16.0);
		}

		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 2.0 / 16.0);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 1.0 / 16.0);
		}
	}
}

