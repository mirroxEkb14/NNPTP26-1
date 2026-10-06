namespace NNPTPZ1.Mathematics
{
    public static class PolynomialFactory
    {
        public static Polynomial CreateDefault()
        {
            // default coefficients: [1, 0, 0, 1] (i.e. 1 + x^3)
            var p = new Polynomial();
            p.Coefficients.Add(new ComplexNumber() { Re = 1 });
            p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(ComplexNumber.Zero);
            //p.Coefficients.Add(ComplexNumber.Zero);
            p.Coefficients.Add(new ComplexNumber() { Re = 1 });
            return p;
        }
    }
}
