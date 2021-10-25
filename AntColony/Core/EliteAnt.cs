namespace AntColony.Core
{
    internal class EliteAnt : IAnt
    {
        public int Pheromones { get; }

        public EliteAnt(int pheromones)
        {
            Pheromones = pheromones;
        }
    }
}
