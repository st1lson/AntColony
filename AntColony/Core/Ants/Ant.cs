namespace AntColony.Core.Ants
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
