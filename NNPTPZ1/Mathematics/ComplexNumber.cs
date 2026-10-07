using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber : IEquatable<ComplexNumber>
    {
        public double Real { get; set; }

        public double Imaginary { get; set; }

        public bool Equals(ComplexNumber other)
        {
            if (other is null) return false;
            return other.Real == Real && other.Imaginary == Imaginary;
        }

        public override bool Equals(object obj) => Equals(obj as ComplexNumber);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Real.GetHashCode();
                hash = hash * 23 + Imaginary.GetHashCode();
                return hash;
            }
        }

        public static readonly ComplexNumber Zero = new ComplexNumber() { Real = 0, Imaginary = 0 };

        public ComplexNumber Multiply(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber()
            {
                // Formula for multiplication used:
                // (aRe + aIm*i)*(bRe + bIm*i) = (aRe*bRe - aIm*bIm) + (aRe*bIm + aIm*bRe)i
                Real = a.Real * b.Real - a.Imaginary * b.Imaginary,
                Imaginary = a.Real * b.Imaginary + a.Imaginary * b.Real
            };
        }

        public double GetAbs()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber()
            {
                Real = a.Real + b.Real,
                Imaginary = a.Imaginary + b.Imaginary
            };
        }

        public double GetAngleInDegrees()
        {
            return Math.Atan2(Imaginary, Real) * (180.0 / Math.PI);
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber()
            {
                Real = a.Real - b.Real,
                Imaginary = a.Imaginary - b.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            // Formula for division used:
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            // bRe*bRe - bIm*bIm*i*i
            var tmp = this.Multiply(new ComplexNumber() { Real = b.Real, Imaginary = -b.Imaginary });
            var tmp2 = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new ComplexNumber()
            {
                Real = tmp.Real / tmp2,
                Imaginary = tmp.Imaginary / tmp2
            };
        }

        public bool ApproximatelyEquals(ComplexNumber other, double eps = 1e-6)
        {
            if (other is null) return false;
            return Math.Abs(Real - other.Real) <= eps && Math.Abs(Imaginary - other.Imaginary) <= eps;
        }
    }
}
