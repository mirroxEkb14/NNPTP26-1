using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments provided. Using default parameters.");
                args = new string[] { "800", "600", "-1.5", "1.5", "-1", "1", "out.png" };
            }

            if (args.Length < 7)
            {
                Console.WriteLine("Usage: NNPTPZ1 <width> <height> <xmin> <xmax> <ymin> <ymax> <output>");
                Console.WriteLine("Not enough arguments provided ({0}). Missing values will be filled with defaults.", args.Length);
                var defaults = new string[] { "800", "600", "-1.5", "1.5", "-1", "1", "out.png" };
                var merged = new string[7];
                for (int i = 0; i < 7; i++)
                    merged[i] = i < args.Length ? args[i] : defaults[i];
                args = merged;
            }

            int[] intargs = new int[2];
            if (!int.TryParse(args[0], out intargs[0]))
            {
                Console.WriteLine("Invalid width '{0}', using default 800.", args[0]);
                intargs[0] = 800;
            }
            if (!int.TryParse(args[1], out intargs[1]))
            {
                Console.WriteLine("Invalid height '{0}', using default 600.", args[1]);
                intargs[1] = 600;
            }

            double[] doubleargs = new double[4];
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            if (!double.TryParse(args[2], System.Globalization.NumberStyles.Float, culture, out doubleargs[0]))
            {
                Console.WriteLine("Invalid xmin '{0}', using default -1.5.", args[2]);
                doubleargs[0] = -1.5;
            }
            if (!double.TryParse(args[3], System.Globalization.NumberStyles.Float, culture, out doubleargs[1]))
            {
                Console.WriteLine("Invalid xmax '{0}', using default 1.5.", args[3]);
                doubleargs[1] = 1.5;
            }
            if (!double.TryParse(args[4], System.Globalization.NumberStyles.Float, culture, out doubleargs[2]))
            {
                Console.WriteLine("Invalid ymin '{0}', using default -1.", args[4]);
                doubleargs[2] = -1.0;
            }
            if (!double.TryParse(args[5], System.Globalization.NumberStyles.Float, culture, out doubleargs[3]))
            {
                Console.WriteLine("Invalid ymax '{0}', using default 1.", args[5]);
                doubleargs[3] = 1.0;
            }

            string output = args[6] ?? "out.png";

            Bitmap bmp = new Bitmap(intargs[0], intargs[1]);
            double xmin = doubleargs[0];
            double xmax = doubleargs[1];
            double ymin = doubleargs[2];
            double ymax = doubleargs[3];

            double xstep = (xmax - xmin) / intargs[0];
            double ystep = (ymax - ymin) / intargs[1];

            // TODO: poly should be parameterised?
            Polynomial p = new Polynomial();
            p.Coefficients.Add(new ComplexNumber() { Re = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            //p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber() { Re = 1 });
            Polynomial pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            Rendering.FractalRenderer.Render(p, pd, intargs[0], intargs[1], xmin, xmax, ymin, ymax, output, maxIter: 100, tol: 1e-6);
        }
    }
}
