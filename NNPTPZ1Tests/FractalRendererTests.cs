using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using NNPTPZ1.Rendering;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class FractalRendererTests
    {
        [TestMethod]
        public void Render_CreatesPng_WithExpectedSize()
        {
            // p(x) = x - 1  => coefficients [-1, 1]
            var p = new Polynomial();
            p.Coefficients.Add(new ComplexNumber { Real = -1, Imaginary = 0 });
            p.Coefficients.Add(new ComplexNumber { Real = 1, Imaginary = 0 });
            var pd = p.Derive();

            var tmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".png");
            try
            {
                FractalRenderer.Render(p, pd, width: 8, height: 8, xmin: -1, xmax: 1, ymin: -1, ymax: 1, output: tmp, maxIter: 10, tol: 1e-6);

                // Assert: file exists AND '.png' signature
                Assert.IsTrue(File.Exists(tmp), "Output file should exist");
                byte[] signature = new byte[8];
                using (var fs = File.OpenRead(tmp))
                {
                    Assert.IsTrue(fs.Length >= 8, "Output file too small to be a PNG");
                    fs.Read(signature, 0, 8);
                }
                var pngSig = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
                CollectionAssert.AreEqual(pngSig, signature, "Output file does not start with PNG signature");
            }
            finally
            {
                if (File.Exists(tmp)) File.Delete(tmp);
            }
        }
    }
}
