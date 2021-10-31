using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal class EliteAnt : IAnt
    {
        public static int Count = 15;
        public int StartPoint { get; }
        public double Pheromone { get; }
        public int PathCost { get; set; }
        public List<int> Path { get; }
        public List<int> BlackList { get; }

        public EliteAnt(int startPoint, double pheromone)
        {
            StartPoint = startPoint;
            Pheromone = pheromone;
            BlackList = new();
        }
    }
}
