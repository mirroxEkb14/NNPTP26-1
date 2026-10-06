using System;
using System.Collections.Generic;
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

            List<ComplexNumber> roots = new List<ComplexNumber>();
            // TODO: poly should be parameterised?
            Poly p = new Poly();
            p.Coe.Add(new ComplexNumber() { Re = 1 });
            p.Coe.Add(ComplexNumber.Zero);
            p.Coe.Add(ComplexNumber.Zero);
            //p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new ComplexNumber() { Re = 1 });
            Poly ptmp = p;
            Poly pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            var clrs = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxId = 0;

            // TODO: cleanup!!!
            // for every pixel in image...
            for (int i = 0; i < intargs[1]; i++)
            {
                for (int j = 0; j < intargs[0]; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;
                    ComplexNumber ox = new ComplexNumber() { Re = x, Imaginari = y };

                    if (ox.Re == 0)
                        ox.Re = 1e-6;
                    if (ox.Imaginari == 0)
                        ox.Imaginari = 1e-6;

                    var result = NewtonSolver.Solve(p, pd, ox, maxIter: 100, tol: 1e-6);
                    ox = result.Root;
                    int it = result.Iterations;

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w < roots.Count; w++)
                    {
                        if (Math.Pow(ox.Re - roots[w].Re, 2) + Math.Pow(ox.Imaginari - roots[w].Imaginari, 2) <= 0.01)
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
                        maxId = roots.Count;
                    }

                    // colorize pixel according to root number
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    var vv = clrs[id % clrs.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R-(int)it*2), 255), Math.Min(Math.Max(0, vv.G - (int)it*2), 255), Math.Min(Math.Max(0, vv.B - (int)it*2), 255));
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    bmp.SetPixel(j, i, vv);
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
                }
            }

            // TODO: delete I suppose...
            //for (int i = 0; i < 300; i++)
            //{
            //    for (int j = 0; j < 300; j++)
            //    {
            //        Color c = bmp.GetPixel(j, i);
            //        int nv = (int)Math.Floor(c.R * (255.0 / maxid));
            //        bmp.SetPixel(j, i, Color.FromArgb(nv, nv, nv));
            //    }
            //}

            bmp.Save(output ?? "../../../out.png");
            //Console.ReadKey();
        }
    }

    namespace Mathematics
    {
        public class Poly
        {
            /// <summary>
            /// Coe
            /// </summary>
            public List<ComplexNumber> Coe { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Poly() => Coe = new List<ComplexNumber>();

            public void Add(ComplexNumber coe) =>
                Coe.Add(coe);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Poly Derive()
            {
                Poly p = new Poly();
                for (int q = 1; q < Coe.Count; q++)
                {
                    p.Coe.Add(Coe[q].Multiply(new ComplexNumber() { Re = q }));
                }

                return p;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                var y = Eval(new ComplexNumber() { Re = x, Imaginari = 0 });
                return y;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber s = ComplexNumber.Zero;
                for (int i = 0; i < Coe.Count; i++)
                {
                    ComplexNumber coef = Coe[i];
                    ComplexNumber bx = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            bx = bx.Multiply(x);

                        coef = coef.Multiply(bx);
                    }

                    s = s.Add(coef);
                }

                return s;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string s = "";
                int i = 0;
                for (; i < Coe.Count; i++)
                {
                    s += Coe[i];
                    if (i > 0)
                    {
                        int j = 0;
                        for (; j < i; j++)
                        {
                            s += "x";
                        }
                    }
                    if (i+1<Coe.Count)
                    s += " + ";
                }
                return s;
            }
        }
    }
}
