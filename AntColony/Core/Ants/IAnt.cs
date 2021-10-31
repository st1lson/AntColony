using AntColony.Core.Graphs;
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
        public List<int> PossibleWays { get; set; }
        public List<int> BlackList { get; }
        public List<int> InitWays(int size);
        public void Move(Graph graph, double[,] pheromones, int beta = 0, int alpha = 0);
    }
}
