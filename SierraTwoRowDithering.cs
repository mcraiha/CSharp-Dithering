/*
   This file implements error pushing of dithering via (Frankie) Sierra Two Row kernel.

   This is free and unencumbered software released into the public domain.
*/

class SierraTwoRowDithering : DitheringBase
{
    public SierraTwoRowDithering(FindColor colorfunc, bool useFastMode = false) : base(colorfunc, useFastMode: useFastMode)
    {
        this.methodLongName = "SierraTwoRow";
        this.fileNameAddition = "_SIE2R";
    }

    override protected void PushError(int x, int y, short[] quantError)
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
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 4.0f / 16.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 3.0f / 16.0f);
        }

        // Next row
        currentRow = yPlusOne;
        if (this.IsValidCoordinate(xMinusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 1.0f / 16.0f);
        }

        if (this.IsValidCoordinate(xMinusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 2.0f / 16.0f);
        }

        if (this.IsValidCoordinate(x, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 3.0f / 16.0f);
        }

        if (this.IsValidCoordinate(xPlusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 2.0f / 16.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 1.0f / 16.0f);
        }
    }
}

