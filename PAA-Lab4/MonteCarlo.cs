namespace PAA_Lab4
{
    public delegate bool BoundCondition(double x, double y, double z);

    public static class MonteCarlo
    {
        public static double ComputeVolume(
                BoundCondition[] boundConditions,
                int pointsCount,
                double xMin,
                double xMax,
                double yMin,
                double yMax,
                double zMin,
                double zMax)
        {
            Random rand = new();
            int count = 0;
            double v = (xMax - xMin) * (yMax - yMin) * (zMax - zMin);
            for (int i = 0; i < pointsCount; i++)
            {
                double x = rand.NextDouble(xMin, xMax);
                double y = rand.NextDouble(yMin, yMax);
                double z = rand.NextDouble(zMin, zMax);

                if (boundConditions.All(c => c(x, y, z)))
                {
                    count++;
                }
            }
            return v / pointsCount * count;
        }

        private static double NextDouble(this Random rand, double min, double max)
        {
            return rand.NextDouble() * (max - min) + min;
        }
    }
}
