using System.Globalization;

namespace NNPTPZ1.Config
{
    public static class CommandLineParser
    {
        public static RenderOptions Parse(string[] args)
        {
            var opts = new RenderOptions();
            if (args == null || args.Length == 0) return opts;

            var culture = CultureInfo.InvariantCulture;

            if (args.Length > 0 && int.TryParse(args[0], out var w)) opts.Width = w;
            if (args.Length > 1 && int.TryParse(args[1], out var h)) opts.Height = h;
            if (args.Length > 2 && double.TryParse(args[2], NumberStyles.Float, culture, out var xmin)) opts.XMin = xmin;
            if (args.Length > 3 && double.TryParse(args[3], NumberStyles.Float, culture, out var xmax)) opts.XMax = xmax;
            if (args.Length > 4 && double.TryParse(args[4], NumberStyles.Float, culture, out var ymin)) opts.YMin = ymin;
            if (args.Length > 5 && double.TryParse(args[5], NumberStyles.Float, culture, out var ymax)) opts.YMax = ymax;
            if (args.Length > 6 && !string.IsNullOrWhiteSpace(args[6])) opts.Output = args[6];

            return opts;
        }
    }
}
