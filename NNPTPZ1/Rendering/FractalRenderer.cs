using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Rendering
{
    public static class FractalRenderer
    {
        public static void Render(Polynomial p, Polynomial pd, Config.RenderOptions opts,
            int maxIter = 100, double tol = 1e-6)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (pd == null) throw new ArgumentNullException(nameof(pd));

            var width = opts.Width;
            var height = opts.Height;
            var xmin = opts.XMin;
            var xmax = opts.XMax;
            var ymin = opts.YMin;
            var ymax = opts.YMax;

            Bitmap bmp = new Bitmap(width, height);

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            var mapper = new ColorMapper();
            var roots = new List<ComplexNumber>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var start = PixelToComplex(j, i, xmin, ymin, xstep, ystep);
                    EnsureNonZero(ref start);

                    var (id, iterations) = ComputeRootIndex(p, pd, start, roots, tol, maxIter);
                    PaintPixel(bmp, j, i, id, iterations, mapper);
                }
            }

            bmp.Save(opts.Output ?? "../../../out.png");
        }

        private static (int id, int iterations) ComputeRootIndex(Polynomial p, Polynomial pd, ComplexNumber start, List<ComplexNumber> roots, double tol, int maxIter)
        {
            var result = NewtonSolver.Solve(p, pd, start, maxIter: maxIter, tol: tol);
            var root = result.Root;
            var iterations = result.Iterations;
            int id = FindOrAddRoot(roots, root, 0.01);
            return (id, iterations);
        }

        private static void PaintPixel(Bitmap bmp, int x, int y, int rootIndex, int iterations, ColorMapper mapper)
        {
            var color = mapper.MapColor(rootIndex, iterations);
            bmp.SetPixel(x, y, color);
        }

        private static ComplexNumber PixelToComplex(int x, int y, double xmin, double ymin, double xstep, double ystep)
        {
            double cx = xmin + x * xstep;
            double cy = ymin + y * ystep;
            return new ComplexNumber() { Real = cx, Imaginary = cy };
        }

        private static void EnsureNonZero(ref ComplexNumber z)
        {
            if (z.Real == 0) z.Real = 1e-6;
            if (z.Imaginary == 0) z.Imaginary = 1e-6;
        }

        private static int FindOrAddRoot(List<ComplexNumber> roots, ComplexNumber root, double tol)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                var dx = root.Real - roots[i].Real;
                var dy = root.Imaginary - roots[i].Imaginary;
                if (dx * dx + dy * dy <= tol * tol)
                    return i;
            }
            roots.Add(root);
            return roots.Count - 1;
        }
    }
}
