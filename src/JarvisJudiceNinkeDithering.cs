/*
   This file implements error pushing of dithering via Jarvis, Judice and Ninke kernel.

   This is free and unencumbered software released into the public domain.
*/
using System;

/// <summary>
/// Jarvis-Judice-Ninke dithering for RGB bytes
/// </summary>
public sealed class JarvisJudiceNinkeDitheringRGBByte : DitheringBase<byte>
{
	/// <summary>
	/// Constructor for Jarvis-Judice-Ninke dithering
	/// </summary>
	/// <param name="colorfunc">Color function</param>
	public JarvisJudiceNinkeDitheringRGBByte(ColorFunction colorfunc) : base(colorfunc, "Jarvis-Judice-Ninke", "_JJN")
	{

	}

	/// <summary>
	/// Push error method for Jarvis-Judice-Ninke dithering
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	/// <param name="quantError">Quantization error</param>
	override protected void PushError(int x, int y, double[] quantError)
	{
		// Push error
		// 	              X     7/48   5/48
		// 3/48   5/48   7/48   5/48   3/48
		// 1/48   3/48   5/48   3/48   1/48

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
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 7.0 / 48.0);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 5.0 / 48.0);
		}

		// Next row
		currentRow = yPlusOne;
		if (this.IsValidCoordinate(xMinusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 3.0 / 48.0);
		}

		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 5.0 / 48.0);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 7.0 / 48.0);
		}

		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 5.0 / 48.0);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 3.0 / 48.0);
		}

		// Next row
		currentRow = yPlusTwo;
		if (this.IsValidCoordinate(xMinusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 1.0 / 48.0);
		}

		if (this.IsValidCoordinate(xMinusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 3.0 / 48.0);
		}

		if (this.IsValidCoordinate(x, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 5.0 / 48.0);
		}

		if (this.IsValidCoordinate(xPlusOne, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 3.0 / 48.0);
		}

		if (this.IsValidCoordinate(xPlusTwo, currentRow))
		{
			this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 1.0 / 48.0);
		}
	}
}