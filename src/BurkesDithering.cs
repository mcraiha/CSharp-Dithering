/*
   This file implements error pushing of dithering via (Daniel) Burkes kernel.

   This is free and unencumbered software released into the public domain.
*/
using System;

/// <summary>
/// Burkes dithering for RGB bytes
/// </summary>
public sealed class BurkesDitheringRGBByte : DitheringBase<byte>
{
	/// <summary>
	/// Constructor for Burkes dithering
	/// </summary>
	/// <param name="colorfunc">Color function</param>
	public BurkesDitheringRGBByte(ColorFunction colorfunc) : base(colorfunc, "Burkes", "_BUR")
	{

	}

	/// <summary>
	/// Push error method for Burkes dithering
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	override protected void PushError(int x, int y, double[] quantError)
	{
		// Push error
		//                X     8/32   4/32 
		// 2/32   4/32   8/32   4/32   2/32

		int xMinusOne = x - 1;
		int xMinusTwo = x - 2;
		int xPlusOne = x + 1;
		int xPlusTwo = x + 2;
		int yPlusOne = y + 1;

		// Current row
		int currentRow = y;
		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 8.0f / 32.0f);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 4.0f / 32.0f);
		}

		// Next row
		currentRow = yPlusOne;
		if (this.IsValidCoordinate(xMinusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 2.0f / 32.0f);
		}

		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 4.0f / 32.0f);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 8.0f / 32.0f);
		}

		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 4.0f / 32.0f);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 2.0f / 32.0f);
		}
	}
}

