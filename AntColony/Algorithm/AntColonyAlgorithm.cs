using System;
using System.Collections.Generic;
using AntColony.Core;

namespace AntColony.Algorithm
{
    internal class AntColonyAlgorithm
    {
        private readonly List<IAnt> _ants;
        private readonly List<int> _pheromones;

        public AntColonyAlgorithm()
        {
            _ants = new();
            _pheromones = new();
        }

        public bool TrySolve(List<int> data, out int result)
        {
            try
            {
                result = Solve(data);
            }
            catch (Exception)
            {
                result = Int32.MaxValue;
                return false;
            }

            return true;
        }

        private int Solve(List<int> data)
        {
            foreach (IAnt ant in _ants)
            {

            }

            return default;
        }
    }
}
