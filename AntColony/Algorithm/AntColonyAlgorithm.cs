using System;
using System.Collections.Generic;
using System.Linq;
using AntColony.Core;
using AntColony.Core.Ants;
using AntColony.Core.Graphs;

namespace AntColony.Algorithm
{
    internal class AntColonyAlgorithm
    {
        private readonly Graph _graph;
        private readonly List<IAnt> _ants;
        private readonly double[,] _pheromones;
        private readonly int _maxIterations;
        private readonly int _alpha;
        private readonly int _beta;
        private readonly double _rho;
        private readonly int _lmin;

        public AntColonyAlgorithm(Graph graph, Config config)
        {
            _graph = graph;
            _maxIterations = config.MaxIterations;
            _alpha = config.Alpha;
            _beta = config.Beta;
            _rho = config.Rho;
            _lmin = GreedySearch();
            _ants = InitAnts();
            _pheromones = new double[_graph.Size, _graph.Size];
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

            int iteration = 0;
            int bestWay = Int32.MaxValue;
            while (iteration < _maxIterations)
            {
                foreach (IAnt ant in _ants)
                {
                    if (ant.GetType() == typeof(EliteAnt))
                    {
                        int startPosition = ant.StartPoint;
                        EliteAntMove(ant, startPosition);
                    }
                    else
                    {
                        int startPosition = ant.StartPoint;
                        ClassicAntMove(ant, startPosition);
                    }

                    if (ant.PathCost > bestWay)
                    {
                        bestWay = ant.PathCost;
                    }

                    ChangePheromone(ant);
                }

                iteration++;
            }

            return bestWay;
        }

        private int GreedySearch()
        {
            if (_graph is null || _graph.Size == 0)
            {
                throw new Exception("Unable to resolve empty graph");
            }

            int Lmin = Int32.MaxValue;
            for (int i = 0; i < _graph.Matrix.GetLength(0); i++)
            {
                int L = 0;
                List<int> possibleWays = new();

                for (int j = 0; j < _graph.Matrix.GetLength(0); j++)
                {
                    possibleWays.Add(j);
                }

                int startPoint = i;
                int currL = Int32.MaxValue;
                possibleWays.Remove(startPoint);

                while (possibleWays.Any())
                {
                    int nextPosition = 0;
                    foreach (var way in possibleWays)
                    {
                        if (currL > _graph.Matrix[startPoint, way])
                        {
                            currL = _graph.Matrix[startPoint, way];
                            nextPosition = way;
                        }
                    }

                    L += currL;
                    startPoint = nextPosition;
                    currL = Int32.MaxValue;
                    possibleWays.Remove(startPoint);
                }

                if (Lmin > L)
                {
                    Lmin = L;
                }
            }

            return Lmin;
        }

        private void EliteAntMove(IAnt ant, int position)
        {
            List<int> possibleWays = FindPossibleWays(ant, position);

            int nextPosition = 0;
            double maxChance = 0;
            foreach (int way in possibleWays)
            {
                if (_pheromones[position, way] > maxChance)
                {
                    maxChance = _pheromones[position, way];
                    nextPosition = way;
                }
            }

            ant.PathCost += _graph.Matrix[position, nextPosition];
            ant.Path.Add(nextPosition);
            ant.BlackList.Add(position);
            EliteAntMove(ant, nextPosition);
        }

        private void ClassicAntMove(IAnt ant, int position)
        {
            List<int> possibleWays = FindPossibleWays(ant, position);

            if (possibleWays.Count == 0)
            {
                return;
            }

            int nextPosition = 0;
            if (possibleWays.Count == 1)
            {
                nextPosition = possibleWays[0];
                ant.PathCost += _graph.Matrix[position, nextPosition];
                ant.Path.Add(nextPosition);
                ant.BlackList.Add(position);
                ClassicAntMove(ant, nextPosition);
            }

            double summary = 0;
            foreach (int way in possibleWays)
            {
                double eta = (double) 1 / _graph.Matrix[position, way];
                if (_pheromones[position, way] == 0)
                {
                    summary += Math.Pow(eta, _beta);
                    continue;
                }

                summary += Math.Pow(eta, _beta) * Math.Pow(_pheromones[position, way], _alpha);
            }

            double maxChance = 0;
            foreach (int way in possibleWays)
            {
                double eta = (double)1 / _graph.Matrix[position, way];
                double P = Math.Pow(eta, _beta) * Math.Pow(_pheromones[position, way], _alpha) / summary;
                if (P > maxChance)
                {
                    maxChance = P;
                    nextPosition = way;
                }
            }

            ant.PathCost += _graph.Matrix[position, nextPosition];
            ant.Path.Add(nextPosition);
            ant.BlackList.Add(position);
            ClassicAntMove(ant, nextPosition);
        }

        private List<int> FindPossibleWays(IAnt ant, int position)
        {
            List<int> possibleWays = new();
            for (int i = 0; i < _graph.Matrix.GetLength(0); i++)
            {
                if (!ant.BlackList.Contains(i) && i != position)
                {
                    possibleWays.Add(i);
                }
            }

            return possibleWays;
        }

        private void ChangePheromone(IAnt ant)
        {
            for (int i = 0; i < _graph.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _graph.Matrix.GetLength(1); j++)
                {
                    double value = (1 - _rho) * _pheromones[i, j] * (_lmin / ant.PathCost);
                    if (value < 0)
                    {
                        value = 0;
                    }

                    _pheromones[i, j] = value;
                }
            }
        }

        private List<IAnt> InitAnts()
        {
            List<IAnt> ants = new();
            Random random = new();

            for (int i = 0; i < Ant.Count; i++)
            {
                int startPoint = random.Next(_graph.Size);
                ants.Add(new Ant(startPoint, 0.1));
            }

            for (int i = 0; i < EliteAnt.Count; i++)
            {
                int startPoint = random.Next(_graph.Size);
                ants.Add(new EliteAnt(startPoint, 0.2));
            }

            return ants;
        }
    }
}
