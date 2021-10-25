using System;
using System.Text;

namespace AntColony.Handlers
{
    internal class InputHandler
    {
        private readonly string _menu;

        public InputHandler() => _menu = CreateMenu();

        public void Menu()
        {
            Console.WriteLine(_menu);

            Menu();
        }

        private string CreateMenu()
        {
            StringBuilder stringBuilder = new();

            return stringBuilder.ToString();
        }
    }
}
