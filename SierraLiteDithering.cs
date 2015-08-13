/*
   This file implements error pushing of dithering via (Frankie) Sierra Lite kernel.

   This is free and unencumbered software released into the public domain.
*/

class SierraLiteDithering : DitheringBase
{
    public SierraLiteDithering(FindColor colorfunc, bool useFastMode = false) : base(colorfunc, useFastMode: useFastMode)
    {
        this.methodLongName = "SierraLite";
        this.fileNameAddition = "_SIEL";
    }

    override protected void PushError(int x, int y, short[] quantError)
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
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 2.0f / 4.0f);
        }

        // Next row
        currentRow = yPlusOne;
        if (this.IsValidCoordinate(xMinusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 1.0f / 4.0f);
        }

        if (this.IsValidCoordinate(x, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 1.0f / 4.0f);
        }
    }
}
