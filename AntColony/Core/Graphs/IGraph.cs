namespace AntColony.Core.Graphs
{
    internal interface IGraph
    {
        public int Size { get; }
        public int[,] Matrix { get; }
    }
}
