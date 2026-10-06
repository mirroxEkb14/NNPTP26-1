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
            Poly poly = new Poly();
            poly.Coe.Add(new ComplexNumber() { Re = -1, Imaginari = 0 });
            poly.Coe.Add(ComplexNumber.Zero);
            poly.Coe.Add(ComplexNumber.Zero);
            poly.Coe.Add(new ComplexNumber() { Re = 1, Imaginari = 0 });

            Poly pd = poly.Derive();

            ComplexNumber start = new ComplexNumber() { Re = 0.5, Imaginari = 0 };

            var result = NewtonSolver.Solve(poly, pd, start, maxIter: 100, tol: 1e-8);

            Assert.IsTrue(result.Converged, "Newton did not converge");
            var expected = new ComplexNumber() { Re = 1, Imaginari = 0 };
            Assert.IsTrue(result.Root.ApproximatelyEquals(expected, 1e-6), $"Root {result.Root} is not close to 1");
        }
    }
}
