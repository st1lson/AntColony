using System.Text;

namespace AntColony.Core.Graphs
{
    internal class Graph : IGraph
    {
        public int Size { get; }

        public int[,] Matrix { get; }

        public Graph(int size, int[,] matrix)
        {
            Size = size;
            Matrix = matrix;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    stringBuilder.Append(Matrix[i, j] + '\t');
                }

                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}
