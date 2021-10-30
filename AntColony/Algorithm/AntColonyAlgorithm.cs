using System;
using System.Collections.Generic;
using AntColony.Core;
using AntColony.Core.Ants;
using AntColony.Core.Graphs;

namespace AntColony.Algorithm
{
    internal class AntColonyAlgorithm
    {
        private readonly IGraph _graph;
        private readonly List<IAnt> _ants;
        private readonly int[,] _pheromones;
        private readonly int Alpha;
        private readonly int Beta;
        private readonly double Rho;

        public AntColonyAlgorithm(IGraph graph, Config config)
        {
            _graph = graph;
            Alpha = config.Alpha;
            Beta = config.Beta;
            Rho = config.Rho;

            _ants = InitAnts();

            _pheromones = new int[_graph.Size, _graph.Size];
        }

        public bool TrySolve(out int result)
        {
            try
            {
                result = Solve();
            }
            catch (Exception)
            {
                result = Int32.MaxValue;
                return false;
            }

            return true;
        }

        private int Solve()
        {
            if (_graph is null || _graph.Size == 0)
            {
                throw new Exception("Unable to resolve empty graph");
            }

            foreach (IAnt ant in _ants)
            {
                if (ant.GetType() == typeof(EliteAnt))
                {
                    Console.WriteLine("Elite ant");
                    int position = ant.StartPoint;
                    MoveToPheromones(position);
                }
                else
                {
                    Console.WriteLine("Classic ant");
                    int position = ant.StartPoint;
                    ClassicMove(position);
                }
            }

            return default;
        }

        private void MoveToPheromones(int position)
        {

        }

        private void ClassicMove(int position)
        {

        }

        private List<IAnt> InitAnts()
        {
            List<IAnt> ants = new();
            Random random = new();

            for (int i = 0; i < Ant.Count; i++)
            {
                int startPoint = random.Next(_graph.Size);
                ants.Add(new Ant(startPoint, 1));
            }

            for (int i = 0; i < EliteAnt.Count; i++)
            {
                int startPoint = random.Next(_graph.Size);
                ants.Add(new EliteAnt(startPoint, 2));
            }

            return ants;
        }
    }
}
