using System;
using System.Collections.Generic;
using System.Linq;
using AntColony.Core;
using AntColony.Core.Ants;
using AntColony.Core.Graphs;

namespace AntColony.Algorithm
{
    internal class AntColonyAlgorithm : IAlgorithm
    {
        private List<IAnt> _ants;
        private double[,] _pheromones;
        private readonly Graph _graph;
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

        public int Solve()
        {
            if (_graph is null || _graph.Size == 0)
            {
                throw new Exception("Unable to resolve empty graph");
            }

            int iteration = 0;
            int bestWay = Int32.MaxValue;
            _ants = InitAnts();
            _pheromones = InitPheromones();
            while (iteration < _maxIterations)
            {
                foreach (IAnt ant in _ants)
                {
                    if (ant.GetType() == typeof(EliteAnt))
                    {
                        ant.Move(_graph,
                                 _pheromones);
                    }
                    else
                    {
                        ant.Move(_graph,
                                 _pheromones,
                                 beta: _beta,
                                 alpha: _alpha);
                    }

                    ChangePheromone(ant);
                }

                int currentBest = _ants.OrderBy(x => x.PathCost).First().PathCost;
                if (currentBest < bestWay)
                {
                    bestWay = currentBest;
                }

                _ants = InitAnts();
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
            for (int i = 0; i < _graph.Size; i++)
            {
                int L = 0;
                List<int> possibleWays = new();

                for (int j = 0; j < _graph.Size; j++)
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

        private void ChangePheromone(IAnt ant)
        {
            for (int i = 0; i < _graph.Size; i++)
            {
                for (int j = i + 1; j < _graph.Size; j++)
                {
                    double decrease = (1 - _rho) * _pheromones[i, j];
                    _pheromones[i, j] = _pheromones[j, i] = decrease;
                }
            }

            for (int i = 0; i < ant.Path.Count - 1; i++)
            {
                int current = ant.Path[i];
                int next = ant.Path[i + 1];

                double increase = (double)ant.PathCost / _lmin;
                _pheromones[current, next] = _pheromones[next, current] += increase;
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

            foreach (IAnt ant in ants)
            {
                ant.PossibleWays = ant.InitWays(_graph.Size);
            }

            return ants;
        }

        private double[,] InitPheromones()
        {
            double[,] pheromones = new double[_graph.Size, _graph.Size];
            for (int i = 0; i < _graph.Size; i++)
            {
                for (int j = i + 1; j < _graph.Size; j++)
                {
                    pheromones[i, j] = pheromones[j, i] = 1;
                }
            }

            return pheromones;
        }
    }
}
