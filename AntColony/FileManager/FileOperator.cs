using AntColony.Core;
using AntColony.Core.Graphs;
using System;
using System.IO;
using System.Text;

namespace AntColony.FileManager
{
    internal class FileOperator
    {
        private readonly string _path;

        public FileOperator(string path) => _path = path;

        public Graph DeserializeGraph()
        {
            /*
            using StreamReader reader = new(_path, Encoding.Default);
            string line = reader.ReadLine();
            if (!Int32.TryParse(line, out int size))
            {
                throw new Exception("Wront file format");
            }

            int[,] matrix = new int[size, size];
            int i = 0;
            int j = 0;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                int[] values = Array.ConvertAll(line.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);

                foreach (int value in values)
                {
                    matrix[i, j++] = value;
                }

                i++;
                j = 0;
            }
            */
            int size = 50;
            int[,] matrix = InitMatrixes(size);
            Graph graph = new(size, matrix);

            return graph;
        }

        private static int[,] InitMatrixes(int size)
        {
            Random rand = new();
            int[,] distance = new int[size, size];
            for (int i = 0; i < distance.GetLength(0); i++)
            {
                for (int j = i + 1; j < distance.GetLength(1); j++)
                {
                    int dist = rand.Next(1, 40);
                    distance[i, j] = dist;
                    distance[j, i] = dist;
                }
            }

            return distance;
        }
    }
}
