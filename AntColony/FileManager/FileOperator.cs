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
                int[] values = Array.ConvertAll(line.Split("\t", StringSplitOptions.RemoveEmptyEntries), int.Parse);

                foreach (int value in values)
                {
                    matrix[i, j++] = value;
                }

                i++;
                j = 0;
            }

            Graph graph = new(size, matrix);

            return graph;
        }

        public void SerializeGraph(Graph graph)
        {
            using StreamWriter writer = new(_path, false, Encoding.Default);
            writer.WriteLine(graph.Size);
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    writer.Write(graph.Matrix[i, j] + "\t");
                }

                writer.WriteLine();
            }
        }
    }
}
