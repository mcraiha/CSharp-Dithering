/*
   This file implements fake dithering, meaning NO dithering is done.

   This is free and unencumbered software released into the public domain.
*/

class FakeDithering : DitheringBase
{
    public FakeDithering(FindColor colorfunc, bool useFastMode = false) : base(colorfunc, useFastMode: useFastMode)
    {
        this.methodLongName = "No dithering";
        this.fileNameAddition = "_NONE";
    }

    override protected void PushError(int x, int y, short[] quantError)
    {
        // Don't do anything
    }
}

