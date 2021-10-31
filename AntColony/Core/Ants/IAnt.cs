using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal interface IAnt
    {
        public static int Count = 45;
        public int StartPoint { get; }
        public double Pheromone { get; }
        public int PathCost { get; set; }
        public List<int> Path { get; }
        public List<int> BlackList { get; }
    }
}
