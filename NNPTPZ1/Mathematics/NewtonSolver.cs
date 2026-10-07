namespace NNPTPZ1.Mathematics
{
    public class NewtonResult
    {
        public ComplexNumber Root { get; }
        public int Iterations { get; }
        public bool Converged { get; }

        public NewtonResult(ComplexNumber root, int iterations, bool converged)
        {
            Root = root;
            Iterations = iterations;
            Converged = converged;
        }
    }

    public static class NewtonSolver
    {
        public static NewtonResult Solve(Polynomial p, Polynomial pd, ComplexNumber start, int maxIter = 100, double tol = 1e-6)
        {
            var x = start;
            double epsZero = 1e-12;
            for (int iter = 0; iter < maxIter; iter++)
            {
                var fx = p.Eval(x);
                var dfx = pd.Eval(x);
                var dfxAbs = dfx.GetAbs();
                if (dfxAbs < epsZero)
                {
                    x = x.Add(new ComplexNumber() { Real = tol, Imaginary = tol });
                    continue;
                }

                var diff = fx.Divide(dfx);
                x = x.Subtract(diff);

                if (diff.GetAbs() <= tol)
                    return new NewtonResult(x, iter + 1, true);
            }

            return new NewtonResult(x, maxIter, false);
        }
    }
}
