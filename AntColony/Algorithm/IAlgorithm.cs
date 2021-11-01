namespace AntColony.Algorithm
{
    internal interface IAlgorithm
    {
        public bool TrySolve(out Result result);
        public Result Solve();
    }
}
