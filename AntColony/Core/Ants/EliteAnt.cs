using AntColony.Core.Graphs;
using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal class EliteAnt : IAnt
    {
        public static int Count = 15;
        public int StartPoint { get; set; }
        public int Pheromone { get; }
        public int PathCost { get; set; }
        public List<int> Path { get; }
        public List<int> PossibleWays { get; set; }

        public EliteAnt(int startPoint, int pheromone)
        {
            StartPoint = startPoint;
            Pheromone = pheromone;
            Path = new();
            Path.Add(StartPoint);
            PossibleWays = new();
        }

        public List<int> InitWays(int size)
        {
            List<int> possibleWays = new();
            for (int i = 0; i < size; i++)
            {
                if (i == StartPoint)
                {
                    continue;
                }

                possibleWays.Add(i);
            }

            return possibleWays;
        }

        public void Move(Graph graph, double[,] pheromones, int beta = 0, int alpha = 0)
        {
            int position = StartPoint;
            for (int i = 0; i < graph.Size; i++)
            {
                int nextPosition = 0;
                double maxChance = 0;
                foreach (int way in PossibleWays)
                {
                    if (pheromones[position, way] > maxChance)
                    {
                        maxChance = pheromones[position, way];
                        nextPosition = way;
                    }
                }

                PathCost += graph.Matrix[position, nextPosition];
                if (i == graph.Size - 1)
                {
                    nextPosition = StartPoint;
                }

                Path.Add(nextPosition);
                PossibleWays.Remove(nextPosition);
                position = nextPosition;
            }
        }
    }
}
