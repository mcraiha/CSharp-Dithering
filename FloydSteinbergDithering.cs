/*
   This file implements error pushing of dithering via (Robert) Floyd and (Louis) Steinberg kernel.

   This is free and unencumbered software released into the public domain.
*/

public class FloydSteinbergDithering : DitheringBase
{
	public FloydSteinbergDithering(FindColor colorfunc, bool useFastMode = false) : base(colorfunc, useFastMode: useFastMode)
    {
        this.methodLongName = "Floyd-Steinberg";
        this.fileNameAddition = "_FS";
    }

	override protected void PushError(int x, int y, short[] quantError)
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
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, y,           quantError, 7.0f / 16.0f);
		}

        // Next row
        if (this.IsValidCoordinate(xMinusOne, yPlusOne))
		{
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, yPlusOne,   quantError, 3.0f / 16.0f);
		}

		if (this.IsValidCoordinate(x, yPlusOne))
		{
            this.ModifyImageWithErrorAndMultiplier(x, yPlusOne,           quantError, 5.0f / 16.0f);
		}

		if (this.IsValidCoordinate(xPlusOne, yPlusOne))
		{
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, yPlusOne,    quantError, 1.0f / 16.0f);
		}
	}
}
