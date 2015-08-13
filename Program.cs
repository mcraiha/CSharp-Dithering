/* 
  This is a sample program for testing different dithering methods. It also prints PSRN value for each dithered image.

  This is free and unencumbered software released into the public domain.
*/

using System;
using System.Drawing;

namespace Dithering
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load input file
            Bitmap input = new Bitmap("half.png");

            // Dither with all methods
            bool fastMode = true;
            string filenameBase = "dither";
            DitherCalculateAndSave(new FloydSteinbergDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new JarvisJudiceNinkeDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new StuckiDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new AtkinsonDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new BurkesDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new SierraDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new SierraTwoRowDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new SierraLiteDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
            DitherCalculateAndSave(new FakeDithering(TrueColorToWebSafeColor, fastMode), input, filenameBase);
        }

        private static void DitherCalculateAndSave(DitheringBase method, Bitmap input, string filenameWithoutExtension)
        {
            Bitmap dithered = method.DoDithering(input);
            double mse = CalculateMSE(input, dithered);
            double psrn = CalculatePSRN(mse);
            Console.WriteLine("Method: " + method.GetMethodName() + " PSRN: " + psrn);
            dithered.Save(filenameWithoutExtension + method.GetFilenameAddition() + ".png");
        }

        private static Color TrueColorToWebSafeColor(Color inputColor)
        {
            Color returnColor = Color.FromArgb((byte)Math.Round(inputColor.R / 51.0) * 51,
                                                (byte)Math.Round(inputColor.G / 51.0) * 51,
                                                (byte)Math.Round(inputColor.B / 51.0) * 51);
            return returnColor;
        }

        private static Color BlackOrWhite(Color inputColor)
        {
            int luminanceHSL = (Math.Max(inputColor.R, Math.Max(inputColor.G, inputColor.B)) + Math.Min(inputColor.R, Math.Min(inputColor.G, inputColor.B))) / 2;
            if (luminanceHSL < 128)
            {
                return Color.Black;
            }

            return Color.White;
        }

        // PSRN calculation
        private static double CalculatePSRN(double mse)
        {
            return 10 * Math.Log(255 * 255 / mse) / Math.Log(10);
        }

        private static double CalculateMSE(Bitmap original, Bitmap dithered)
        {
            long mseR = 0;
            long mseG = 0;
            long mseB = 0;

            int difference = 0;

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    difference = original.GetPixel(i, j).R - dithered.GetPixel(i, j).R;
                    mseR += (difference * difference);

                    difference = original.GetPixel(i, j).G - dithered.GetPixel(i, j).G;
                    mseG += (difference * difference);

                    difference = original.GetPixel(i, j).B - dithered.GetPixel(i, j).B;
                    mseB += (difference * difference);
                }
            }

            return (double)(mseR + mseG + mseB) / (double)((original.Width * original.Height) * 3);
        }
    }
}
