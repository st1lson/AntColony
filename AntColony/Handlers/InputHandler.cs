﻿using System;
using System.Text;
using AntColony.Algorithm;
using AntColony.Core;
using AntColony.Core.Graphs;
using AntColony.FileManager;

namespace AntColony.Handlers
{
    internal class InputHandler
    {
        private readonly string _menu;
        private readonly Graph _graph;
        private readonly FileOperator _fileOperator;
        private readonly AntColonyAlgorithm _algorithm;
        private readonly Config _config;

        public InputHandler(Config config)
        {
            _config = config;
            _menu = CreateMenu();
            _fileOperator = new FileOperator(_config.Path);
            _graph = _fileOperator.DeserializeGraph();
            _algorithm = new AntColonyAlgorithm(_graph, config);
        }

        public void Menu()
        {
            Console.WriteLine(_menu);
            string input = Console.ReadLine();
            if (!Int32.TryParse(input, out int value))
            {
                Console.WriteLine("Invalid input");
            }

            Action(value);

            Console.WriteLine("Press 'Enter' to continue");
            ConsoleKey consoleKey = Console.ReadKey().Key;

            if (consoleKey is ConsoleKey.Enter)
            {
                Console.Clear();
                Menu();
            }
        }

        public static void Print(IGraph graph)
        {
            StringBuilder stringBuilder = new();
            for (int i = 0; i < graph.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < graph.Matrix.GetLength(1); j++)
                {
                    stringBuilder.Append(graph.Matrix[i, j] + "\t");
                }

                stringBuilder.Append("\n");
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        private void Action(int value)
        {
            switch (value)
            {
                case 1:
                    _algorithm.TrySolve(out int result);
                    Print(_graph);
                    Console.WriteLine(result);
                    return;
                case 2:
                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("Wrong command");
                    return;
            }
        }

        private string CreateMenu()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append("Enter '1' to solve graph\n");
            stringBuilder.Append("Enter '2' to exit\n");
            
            return stringBuilder.ToString();
        }
    }
}
