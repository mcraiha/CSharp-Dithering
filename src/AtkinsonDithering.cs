/*
   This file implements error pushing of dithering via Atkinson kernel.

   This is free and unencumbered software released into the public domain.
*/
using System;
using System.Numerics;

/// <summary>
/// Atkinson dithering for RGB
/// </summary>
public sealed class AtkinsonDitheringRGB<T> : DitheringBase<T> where T : INumber<T>
{
	/// <summary>
	/// Constructor for Atkinson dithering
	/// </summary>
	/// <param name="colorfunc">Color function</param>
	public AtkinsonDitheringRGB(ColorFunction colorfunc) : base(colorfunc, "Atkinson", "_ATK")
	{

	}

	/// <summary>
	/// Push error method for Atkinson dithering
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	override protected void PushError(int x, int y, double[] quantError)
	{
		// Push error
		//        X    1/8   1/8 
		// 1/8   1/8   1/8
		//       1/8

		int xMinusOne = x - 1;
		int xPlusOne = x + 1;
		int xPlusTwo = x + 2;
		int yPlusOne = y + 1;
		int yPlusTwo = y + 2;

		double multiplier = 1.0 / 8.0; // Atkinson Dithering has same multiplier for every item

		// Current row
		int currentRow = y;
		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, multiplier);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, multiplier);
		}

		// Next row
		currentRow = yPlusOne;
		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, multiplier);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, multiplier);
		}

		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, multiplier);
		}

		// Next row
		currentRow = yPlusTwo;
		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, multiplier);
		}
	}
}

