/*
   This file implements error pushing of dithering via (Robert) Floyd and (Louis) Steinberg kernel.

   This is free and unencumbered software released into the public domain.
*/
using System;

public class FloydSteinbergDithering : DitheringBase
{
	public FloydSteinbergDithering(Func<object[],object[]> colorfunc) : base(colorfunc)
    {
        this.methodLongName = "Floyd-Steinberg";
        this.fileNameAddition = "_FS";
    }

	override protected void PushError(int x, int y, double[] quantError)
	{
		// Push error
		// 			X		7/16
		// 3/16		5/16	1/16

		int xMinusOne = x - 1;
		int xPlusOne = x + 1;
		int yPlusOne = y + 1;

        // Current row
        if (this.IsValidCoordinate(xPlusOne, y))
		{
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, y,           quantError, 7.0 / 16.0);
		}

        // Next row
        if (this.IsValidCoordinate(xMinusOne, yPlusOne))
		{
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, yPlusOne,   quantError, 3.0 / 16.0);
		}

		if (this.IsValidCoordinate(x, yPlusOne))
		{
            this.ModifyImageWithErrorAndMultiplier(x, yPlusOne,           quantError, 5.0 / 16.0);
		}

		if (this.IsValidCoordinate(xPlusOne, yPlusOne))
		{
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, yPlusOne,    quantError, 1.0 / 16.0);
		}
	}
}
