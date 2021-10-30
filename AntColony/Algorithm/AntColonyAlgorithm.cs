using System;
using System.Collections.Generic;
using AntColony.Core;
using AntColony.Core.Ants;

namespace AntColony.Algorithm
{
    internal class AntColonyAlgorithm
    {
        private readonly List<IAnt> _ants;
        private readonly List<(int, int)> _pheromones;

        public AntColonyAlgorithm()
        {
            _ants = new();
            _pheromones = new();
        }

        public bool TrySolve(IGraph graph, out int result)
        {
            try
            {
                result = Solve(graph);
            }
            catch (Exception)
            {
                result = Int32.MaxValue;
                return false;
            }

            return true;
        }

        private int Solve(IGraph graph)
        {
            if (graph is null || graph.Size == 0)
            {
                throw new Exception("Unable to resolve empty graph");
            }

            return default;
        }
    }
}
