using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1.Rendering
{
    public static class FractalRenderer
    {
        public static void Render(Polynomial p, Polynomial pd, int width, int height,
            double xmin, double xmax, double ymin, double ymax, string output,
            int maxIter = 100, double tol = 1e-6)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (pd == null) throw new ArgumentNullException(nameof(pd));

            Bitmap bmp = new Bitmap(width, height);

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<ComplexNumber> roots = new List<ComplexNumber>();

            var mapper = new ColorMapper();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;
                    ComplexNumber ox = new ComplexNumber() { Real = x, Imaginary = y };

                    if (ox.Real == 0) ox.Real = 1e-6;
                    if (ox.Imaginary == 0) ox.Imaginary = 1e-6;

                    var result = NewtonSolver.Solve(p, pd, ox, maxIter: maxIter, tol: tol);
                    ox = result.Root;
                    int it = result.Iterations;

                    var known = false;
                    var id = 0;
                    for (int w = 0; w < roots.Count; w++)
                    {
                        if (Math.Pow(ox.Real - roots[w].Real, 2) + Math.Pow(ox.Imaginary - roots[w].Imaginary, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                            break;
                        }
                    }
                    if (!known)
                    {
                        roots.Add(ox);
                        id = roots.Count - 1;
                    }

                    var vv = mapper.MapColor(id, it);
                    bmp.SetPixel(j, i, vv);
                }
            }

            bmp.Save(output ?? "../../../out.png");
        }
    }
}
