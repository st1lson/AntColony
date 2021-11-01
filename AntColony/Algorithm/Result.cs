using System;
using System.Collections.Generic;
using System.Text;

namespace AntColony.Algorithm
{
    internal class Result
    {
        public TimeSpan Time { get; set; }
        public int PathCost { get; set; }
        public List<int> BestPath { get; set; }
        public int CurrentIteration { private get; set; }
        public DateTime StartTime { private get; set; }

        public void SetTime()
        {
            Time = DateTime.Now - StartTime;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(CurrentIteration < 1000 ? $"Current iteration is {CurrentIteration}\n" : $"Total number of iterations is {CurrentIteration}\n");
            stringBuilder.Append(Time == TimeSpan.Zero ? "Algorithm is now in action\n" : $"Algorithm ended in {Time.TotalSeconds} seconds\n");
            stringBuilder.Append(Time == TimeSpan.Zero ? $"Time from start: {(DateTime.Now - StartTime).TotalSeconds}\n" : "");
            stringBuilder.Append(CurrentIteration < 1000 ? $"Current best path cost is {PathCost}\n" : $"Best path cost is {PathCost}\n");
            stringBuilder.Append("Best path is: ");
            stringBuilder.Append(String.Join("-->", BestPath) + '\n');

            return stringBuilder.ToString();
        }
    }
}
