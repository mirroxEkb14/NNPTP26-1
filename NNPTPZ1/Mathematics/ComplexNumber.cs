using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber : IEquatable<ComplexNumber>
    {
        public double Re { get; set; }

        public double Imaginari { get; set; }

        public bool Equals(ComplexNumber other)
        {
            if (other is null) return false;
            return other.Re == Re && other.Imaginari == Imaginari;
        }

        public override bool Equals(object obj) => Equals(obj as ComplexNumber);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Re.GetHashCode();
                hash = hash * 23 + Imaginari.GetHashCode();
                return hash;
            }
        }

        public static readonly ComplexNumber Zero = new ComplexNumber() { Re = 0, Imaginari = 0 };

        public ComplexNumber Multiply(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber()
            {
                // Formula for multiplication used:
                // (aRe + aIm*i)*(bRe + bIm*i) = (aRe*bRe - aIm*bIm) + (aRe*bIm + aIm*bRe)i
                Re = a.Re * b.Re - a.Imaginari * b.Imaginari,
                Imaginari = a.Re * b.Imaginari + a.Imaginari * b.Re
            };
        }

        public double GetAbS()
        {
            return Math.Sqrt(Re * Re + Imaginari * Imaginari);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber()
            {
                Re = a.Re + b.Re,
                Imaginari = a.Imaginari + b.Imaginari
            };
        }

        public double GetAngleInDegrees()
        {
            return Math.Atan2(Imaginari, Re);
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            var a = this;
            return new ComplexNumber()
            {
                Re = a.Re - b.Re,
                Imaginari = a.Imaginari - b.Imaginari
            };
        }

        public override string ToString()
        {
            return $"({Re} + {Imaginari}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            // Formula for division used:
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            // bRe*bRe - bIm*bIm*i*i
            var tmp = this.Multiply(new ComplexNumber() { Re = b.Re, Imaginari = -b.Imaginari });
            var tmp2 = b.Re * b.Re + b.Imaginari * b.Imaginari;

            return new ComplexNumber()
            {
                Re = tmp.Re / tmp2,
                Imaginari = tmp.Imaginari / tmp2
            };
        }

        public bool ApproximatelyEquals(ComplexNumber other, double eps = 1e-6)
        {
            if (other is null) return false;
            return Math.Abs(Re - other.Re) <= eps && Math.Abs(Imaginari - other.Imaginari) <= eps;
        }
    }
}
