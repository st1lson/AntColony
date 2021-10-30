using System.Collections.Generic;

namespace AntColony.Core.Ants
{
    internal interface IAnt
    {
        public static int Count = 45;
        public int StartPoint { get; }
        public int Pheromones { get; }
        public List<(int, int)> BlackList { get; }
    }
}
