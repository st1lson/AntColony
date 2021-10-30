using System;
using System.Text;
using AntColony.Algorithm;
using AntColony.Core;

namespace AntColony.Handlers
{
    internal class InputHandler
    {
        private readonly string _menu;
        private Graph _graph;
        private AntColonyAlgorithm _algorithm;

        public InputHandler() => _menu = CreateMenu();

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

        private void Action(int value)
        {
            switch (value)
            {
                case 1:
                    _algorithm ??= new AntColonyAlgorithm();
                    _algorithm.TrySolve(_graph, out int result);
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
