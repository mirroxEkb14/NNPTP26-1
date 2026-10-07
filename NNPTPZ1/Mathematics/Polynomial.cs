using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coe) => Coefficients.Add(coe);

        public Polynomial Derive()
        {
            Polynomial p = new Polynomial();
            for (int q = 1; q < Coefficients.Count; q++)
            {
                // derivative coefficient: q * Coefficients[q]
                p.Coefficients.Add(Coefficients[q].Multiply(new ComplexNumber() { Real = q }));
            }

            return p;
        }

        public ComplexNumber Eval(double x)
        {
            return Eval(new ComplexNumber() { Real = x, Imaginary = 0 });
        }

        public ComplexNumber Eval(ComplexNumber x)
        {
            if (Coefficients == null || Coefficients.Count == 0)
                return ComplexNumber.Zero;

            // Horner's method: result = a_n; for i=n-1..0: result = result * x + a_i
            ComplexNumber result = Coefficients[Coefficients.Count - 1];
            for (int i = Coefficients.Count - 2; i >= 0; i--)
            {
                result = result.Multiply(x).Add(Coefficients[i]);
            }

            return result;
        }

        public override string ToString()
        {
            if (Coefficients == null || Coefficients.Count == 0) return "0";
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                sb.Append(Coefficients[i].ToString());
                if (i > 0)
                {
                    for (int j = 0; j < i; j++) sb.Append("x");
                }
                if (i + 1 < Coefficients.Count) sb.Append(" + ");
            }
            return sb.ToString();
        }
    }
}
