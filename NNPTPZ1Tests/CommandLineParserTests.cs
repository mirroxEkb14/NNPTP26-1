using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Config;

namespace NNPTPZ1Tests
{
    [TestClass]
    public class CommandLineParserTests
    {
        [TestMethod]
        public void Parse_NoArgs_ReturnsDefaults()
        {
            var opts = CommandLineParser.Parse(new string[0]);
            Assert.AreEqual(800, opts.Width);
            Assert.AreEqual(600, opts.Height);
            Assert.AreEqual(-1.5, opts.XMin, 1e-9);
            Assert.AreEqual(1.5, opts.XMax, 1e-9);
            Assert.AreEqual(-1.0, opts.YMin, 1e-9);
            Assert.AreEqual(1.0, opts.YMax, 1e-9);
            Assert.AreEqual("out.png", opts.Output);
        }

        [TestMethod]
        public void Parse_PositionalArgs_ParsesAll()
        {
            var args = new string[] { "640", "480", "-2", "2", "-1.2", "1.2", "foo.png" };
            var opts = CommandLineParser.Parse(args);
            Assert.AreEqual(640, opts.Width);
            Assert.AreEqual(480, opts.Height);
            Assert.AreEqual(-2.0, opts.XMin, 1e-9);
            Assert.AreEqual(2.0, opts.XMax, 1e-9);
            Assert.AreEqual(-1.2, opts.YMin, 1e-9);
            Assert.AreEqual(1.2, opts.YMax, 1e-9);
            Assert.AreEqual("foo.png", opts.Output);
        }

        [TestMethod]
        public void Parse_InvalidNumbers_UsesDefaultsForThose()
        {
            var args = new string[] { "bad", "bad", "not", "not", "not", "not", "" };
            var opts = CommandLineParser.Parse(args);
            
            Assert.AreEqual(800, opts.Width);
            Assert.AreEqual(600, opts.Height);
            
            Assert.AreEqual(-1.5, opts.XMin, 1e-9);
            Assert.AreEqual(1.5, opts.XMax, 1e-9);
            Assert.AreEqual(-1.0, opts.YMin, 1e-9);
            Assert.AreEqual(1.0, opts.YMax, 1e-9);
            
            Assert.AreEqual("out.png", opts.Output);
        }
    }
}
