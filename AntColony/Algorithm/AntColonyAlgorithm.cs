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
        private int[,] _pheromones;
        private readonly Graph _graph;
        private readonly int _maxIterations;
        private readonly int _alpha;
        private readonly int _beta;
        private readonly double _rho;
        private readonly int _lmin;
        private readonly Random _random;

        public AntColonyAlgorithm(Graph graph, Config config, Random random)
        {
            _graph = graph;
            _maxIterations = config.MaxIterations;
            _alpha = config.Alpha;
            _beta = config.Beta;
            _rho = config.Rho;
            _lmin = GreedySearch();
            _random = random;
        }

        public bool TrySolve(out Result result)
        {
            try
            {
                result = Solve();
            }
            catch (Exception)
            {
                result = default;
                return false;
            }

            return true;
        }

        public Result Solve()
        {
            if (_graph is null || _graph.Size == 0)
            {
                throw new Exception("Unable to resolve empty graph");
            }

            int iteration = 0;
            int bestWay = Int32.MaxValue;
            _ants = InitAnts();
            _pheromones = InitPheromones();
            Result result = new()
            {
                StartTime = DateTime.Now,
            };

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

                IAnt bestAnt = _ants.OrderBy(x => x.PathCost).First();
                int currentBest = bestAnt.PathCost;
                if (currentBest < bestWay)
                {
                    bestWay = currentBest;
                    result.PathCost = bestWay;
                    result.BestPath = bestAnt.Path;
                }

                if (iteration % 20 == 0)
                {
                    result.CurrentIteration = iteration;
                    Console.WriteLine(result);
                }

                _ants = InitAnts();
                iteration++;
            }

            result.SetTime();
            result.CurrentIteration = _maxIterations;
            return result;
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
                    int decrease = (int)(1 - _rho) * _pheromones[i, j];
                    _pheromones[i, j] = _pheromones[j, i] = decrease;
                }
            }

            for (int i = 0; i < ant.Path.Count - 1; i++)
            {
                int current = ant.Path[i];
                int next = ant.Path[i + 1];

                int increase = ant.PathCost / _lmin;
                increase *= ant.Pheromone;
                _pheromones[current, next] += increase;
                _pheromones[next, current] = _pheromones[current, next];
            }
        }

        private List<IAnt> InitAnts()
        {
            List<IAnt> ants = new();

            for (int i = 0; i < Ant.Count; i++)
            {
                int startPoint = _random.Next(_graph.Size);
                ants.Add(new Ant(startPoint, 1));
            }

            for (int i = 0; i < EliteAnt.Count; i++)
            {
                int startPoint = _random.Next(_graph.Size);
                ants.Add(new EliteAnt(startPoint, 2));
            }

            foreach (IAnt ant in ants)
            {
                ant.PossibleWays = ant.InitWays(_graph.Size);
            }

            return ants;
        }

        private int[,] InitPheromones()
        {
            int[,] pheromones = new int[_graph.Size, _graph.Size];
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
