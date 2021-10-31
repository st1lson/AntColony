using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal class Ant : IAnt
    {
        public static int Count = 30;
        public int StartPoint { get; }
        public double Pheromone { get; }
        public int PathCost { get; set; }
        public List<int> Path { get; }
        public List<int> BlackList { get; }

        public Ant(int startPoint, double pheromone)
        {
            StartPoint = startPoint;
            Pheromone = pheromone;
            Path = new();
            Path.Add(StartPoint);
            BlackList = new();
        }
    }
}
