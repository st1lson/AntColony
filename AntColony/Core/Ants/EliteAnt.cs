namespace AntColony.Core.Ants
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
