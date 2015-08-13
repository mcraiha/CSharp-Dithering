/*
   This file implements error pushing of dithering via (Peter) Stucki kernel.

   This is free and unencumbered software released into the public domain.
*/

class StuckiDithering : DitheringBase
{
    public StuckiDithering(FindColor colorfunc, bool useFastMode = false) : base(colorfunc, useFastMode: useFastMode)
    {
        this.methodLongName = "Stucki";
        this.fileNameAddition = "_STU";
    }

    override protected void PushError(int x, int y, short[] quantError)
    {
        // Push error
        //                X     8/42   4/42 
        // 2/42   4/42   8/42   4/42   2/42
        // 1/42   2/42   4/42   2/42   1/42

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
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 8.0f / 42.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 4.0f / 42.0f);
        }

        // Next row
        currentRow = yPlusOne;
        if (this.IsValidCoordinate(xMinusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 2.0f / 42.0f);
        }

        if (this.IsValidCoordinate(xMinusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 4.0f / 42.0f);
        }

        if (this.IsValidCoordinate(x, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 8.0f / 42.0f);
        }

        if (this.IsValidCoordinate(xPlusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 4.0f / 42.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 2.0f / 42.0f);
        }

        // Next row
        currentRow = yPlusTwo;
        if (this.IsValidCoordinate(xMinusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 1.0f / 42.0f);
        }

        if (this.IsValidCoordinate(xMinusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 2.0f / 42.0f);
        }

        if (this.IsValidCoordinate(x, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 4.0f / 42.0f);
        }

        if (this.IsValidCoordinate(xPlusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 2.0f / 42.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 1.0f / 42.0f);
        }
    }
}
