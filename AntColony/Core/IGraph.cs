namespace AntColony.Core
{
    internal interface IGraph
    {
        public int Size { get; }
        public int[,] Matrix { get; }
    }
}
