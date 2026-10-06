using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Rendering;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class ColorMapperTests
    {
        [TestMethod]
        public void MapRgb_BasePaletteAndDimming_WorksAsExpected()
        {
            var mapper = new ColorMapper();
            var c0 = mapper.MapRgb(0, 0);
            var c1 = mapper.MapRgb(0, 1);

            // Check dimming behavior:
            // color for the first value should be <=  color for the second value
            Assert.IsTrue(c1.R <= c0.R && c1.G <= c0.G && c1.B <= c0.B);

            // check each channel is within 0..255
            var cHigh = mapper.MapRgb(100, 1000);
            Assert.IsTrue(cHigh.R >= 0 && cHigh.R <= 255);
            Assert.IsTrue(cHigh.G >= 0 && cHigh.G <= 255);
            Assert.IsTrue(cHigh.B >= 0 && cHigh.B <= 255);
        }
    }
}
