using AntColony.Core.Graphs;
using System;
using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal class Ant : IAnt
    {
        public static int Count = 30;
        public int StartPoint { get; }
        public int Pheromone { get; }
        public int PathCost { get; set; }
        public List<int> Path { get; }
        public List<int> PossibleWays { get; set; }
        public List<int> BlackList { get; }

        public Ant(int startPoint, int pheromone)
        {
            StartPoint = startPoint;
            Pheromone = pheromone;
            Path = new();
            Path.Add(StartPoint);
            PossibleWays = new();
            BlackList = new();
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
        public void Move(Graph graph, int[,] pheromones, int beta, int alpha)
        {
            int position = StartPoint;
            for (int i = 0; i < graph.Size; i++)
            {
                double summary = 0;
                foreach (int way in PossibleWays)
                {
                    double eta = (double)1 / graph.Matrix[position, way];
                    summary += Math.Pow(eta, beta) * Math.Pow(pheromones[position, way], alpha);
                }

                int nextPosition = 0;
                double chance = 0;
                foreach (int way in PossibleWays)
                {
                    double eta = (double)1 / graph.Matrix[position, way];
                    double P = Math.Pow(eta, beta) * Math.Pow(pheromones[position, way], alpha) / summary;
                    if (P > chance)
                    {
                        chance = P;
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
