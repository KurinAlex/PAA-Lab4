using System.Globalization;
using System.Text;

namespace PAA_Lab4
{
    public class Program
    {
        const double M = 0.01;
        const double Alpha = 0.95;
        const double Z = 1.9599639845400542;

        const double XMin = 0.0;
        const double XMax = 1.0;

        const double YMin = 0.0;
        const double YMax = 1.0;

        const double ZMin = 0.0;
        const double ZMax = 0.5;

        const double V1 = (XMax - XMin) * (YMax - YMin) * (ZMax - ZMin);
        const double V2 = 1.0 / 48.0;
        const double P2 = V2 / V1;
        const double ActualV = Math.PI / 64;

        const int N = (int)(Z * Z / M / M * (1.0 - P2) / P2);

        static readonly BoundCondition[] boundConditions =
        {
            (_, _, z) => z >= 0.0,
            (x, y, _) => x <= y,
            (x, y, z) => x >= y * y + z * z,
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine($" m = {M}");
            Console.WriteLine($" α = {Alpha}");
            Console.WriteLine($" z = {Z}");
            Console.WriteLine($"V1 = {V1}");
            Console.WriteLine($"V2 = {V2}");
            Console.WriteLine($"p2 = {P2}");
            Console.WriteLine($"N* = {N}");
            Console.WriteLine();

            double v = MonteCarlo.ComputeVolume(boundConditions, N, XMin, XMax, YMin, YMax, ZMin, ZMax);
            Console.WriteLine($"Обчисленне значення V = {v}");
            Console.WriteLine($"  Справжнє значення V = {ActualV}");
            Console.WriteLine();

            double p = v / V1;
            double m = Z * Math.Sqrt((1 - p) / N / p);
            Console.WriteLine($"m = {m}");
            Console.WriteLine();

            double minV = v * (1 - m);
            double maxV = v * (1 + m);
            Console.WriteLine($"З ймовірністю, не меншою за {Alpha}, можна стверджувати, що об'єм належить інтервалу");
            Console.WriteLine($"[{minV}; {maxV}]");

            Console.ReadKey();
        }
    }
}