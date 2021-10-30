using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntColony.FileManager
{
    internal class FileOperator
    {
        private readonly string _path;

        public FileOperator(string path) => _path = path;

        public List<int> DeserializeGraph()
        {
            List<int> data = new();
            using StreamReader reader = new(_path, Encoding.Default);
            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!Int32.TryParse(line, out int value))
                {
                    throw new Exception("Wront file format");
                }

                data.Add(value);
            }

            return data;
        }
    }
}
