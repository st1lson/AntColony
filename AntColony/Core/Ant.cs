namespace AntColony.Core
{
    internal class Ant : IAnt
    {
        public int Pheromones { get; }

        public Ant(int pheromones)
        {
            Pheromones = pheromones;
        }
    }
}
