namespace NNPTPZ1.Config
{
    public class RenderOptions
    {
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;
        public double XMin { get; set; } = -1.5;
        public double XMax { get; set; } = 1.5;
        public double YMin { get; set; } = -1.0;
        public double YMax { get; set; } = 1.0;
        public string Output { get; set; } = "out.png";
        public int MaxIter { get; set; } = 100;
        public double Tolerance { get; set; } = 1e-6;
    }
}
