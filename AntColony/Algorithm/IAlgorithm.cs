namespace AntColony.Algorithm
{
    internal interface IAlgorithm
    {
        public bool TrySolve(out int result);
        public int Solve();
    }
}
