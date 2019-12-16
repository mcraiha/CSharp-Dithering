/*
   This file implements fake dithering, meaning NO dithering is done.

   This is free and unencumbered software released into the public domain.
*/
using System;

class FakeDithering : DitheringBase
{
	public FakeDithering(Func<object[],object[]> colorfunc) : base(colorfunc)
	{
		this.methodLongName = "No dithering";
		this.fileNameAddition = "_NONE";
	}

	override protected void PushError(int x, int y, double[] quantError)
	{
		// Don't do anything
	}
}

