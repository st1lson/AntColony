using System;
using System.Text;

namespace AntColony.Core.Graphs
{
    internal class Graph : IGraph
    {
        public int Size { get; }

        public int[,] Matrix { get; private set; }

        public Graph(int size, int[,] matrix)
        {
            Size = size;
            Matrix = matrix;
        }

        public void RandomMatrix(Random random)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = i + 1; j < Size; j++)
                {
                    Matrix[i, j] = Matrix[j, i] = random.Next(1, 40);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    stringBuilder.Append(Matrix[i, j] + "\t");
                }

                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}
