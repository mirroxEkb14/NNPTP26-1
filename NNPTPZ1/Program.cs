using System;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            var opts = Config.CommandLineParser.Parse(args);

            double xmin = opts.XMin;
            double xmax = opts.XMax;
            double ymin = opts.YMin;
            double ymax = opts.YMax;

            var p = PolynomialFactory.CreateDefault();
            Polynomial pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            Rendering.FractalRenderer.Render(p, pd, opts.Width, opts.Height, xmin, xmax, ymin, ymax, opts.Output, maxIter: opts.MaxIter, tol: opts.Tolerance);
        }
    }
}
