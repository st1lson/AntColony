using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal class Ant : IAnt
    {
        public static int Count = 30;
        public int StartPoint { get; }
        public int Pheromones { get; }
        public List<(int, int)> BlackList { get; }

        public Ant(int startPoint, int pheromones)
        {
            StartPoint = startPoint;
            Pheromones = pheromones;
            BlackList = new();
        }
    }
}
