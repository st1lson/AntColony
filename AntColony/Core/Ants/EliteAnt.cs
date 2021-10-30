using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal class EliteAnt : IAnt
    {
        public static int Count = 15;
        public int StartPoint { get; }
        public int Pheromones { get; }
        public List<(int, int)> BlackList { get; }

        public EliteAnt(int startPoint, int pheromones)
        {
            StartPoint = startPoint;
            Pheromones = pheromones;
            BlackList = new();
        }
    }
}
