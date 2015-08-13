/*
   This file implements error pushing of dithering via Jarvis, Judice and Ninke kernel.

   This is free and unencumbered software released into the public domain.
*/

public class JarvisJudiceNinkeDithering : DitheringBase
{
    public JarvisJudiceNinkeDithering(FindColor colorfunc, bool useFastMode = false) : base(colorfunc, useFastMode: useFastMode)
    {
        this.methodLongName = "Jarvis-Judice-Ninke";
        this.fileNameAddition = "_JJN";
    }

    override protected void PushError(int x, int y, short[] quantError)
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
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 7.0f / 48.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 5.0f / 48.0f);
        }

        // Next row
        currentRow = yPlusOne;
        if (this.IsValidCoordinate(xMinusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 3.0f / 48.0f);
        }

        if (this.IsValidCoordinate(xMinusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 5.0f / 48.0f);
        }

        if (this.IsValidCoordinate(x, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 7.0f / 48.0f);
        }

        if (this.IsValidCoordinate(xPlusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 5.0f / 48.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 3.0f / 48.0f);
        }

        // Next row
        currentRow = yPlusTwo;
        if (this.IsValidCoordinate(xMinusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusTwo, currentRow, quantError, 1.0f / 48.0f);
        }

        if (this.IsValidCoordinate(xMinusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xMinusOne, currentRow, quantError, 3.0f / 48.0f);
        }

        if (this.IsValidCoordinate(x, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(x, currentRow, quantError, 5.0f / 48.0f);
        }

        if (this.IsValidCoordinate(xPlusOne, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusOne, currentRow, quantError, 3.0f / 48.0f);
        }

        if (this.IsValidCoordinate(xPlusTwo, currentRow))
        {
            this.ModifyImageWithErrorAndMultiplier(xPlusTwo, currentRow, quantError, 1.0f / 48.0f);
        }
    }
}