using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass]
    public class NewtonSolverTests
    {
        [TestMethod]
        public void Solve_Cubic_x3_minus_1_From_0_5_ConvergesTo1()
        {
            // p(x) = x^3 - 1
            Polynomial poly = new Polynomial();
            poly.Coefficients.Add(new ComplexNumber() { Real = -1, Imaginary = 0 });
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(new ComplexNumber() { Real = 1, Imaginary = 0 });

            Polynomial pd = poly.Derive();

            ComplexNumber start = new ComplexNumber() { Real = 0.5, Imaginary = 0 };

            var result = NewtonSolver.Solve(poly, pd, start, maxIter: 100, tol: 1e-8);

            Assert.IsTrue(result.Converged, "Newton did not converge");
            var expected = new ComplexNumber() { Real = 1, Imaginary = 0 };
            Assert.IsTrue(result.Root.ApproximatelyEquals(expected, 1e-6), $"Root {result.Root} is not close to 1");
        }
    }
}
