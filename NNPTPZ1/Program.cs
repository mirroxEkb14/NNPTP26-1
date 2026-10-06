using System;
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
            var opts = Config.CommandLineParser.Parse(args);

            double xmin = opts.XMin;
            double xmax = opts.XMax;
            double ymin = opts.YMin;
            double ymax = opts.YMax;

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

            Rendering.FractalRenderer.Render(p, pd, opts.Width, opts.Height, xmin, xmax, ymin, ymax, opts.Output, maxIter: opts.MaxIter, tol: opts.Tolerance);
        }
    }
}
