using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day06
    {
        public static void Part1()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input06.txt").ToList();
            var times = lines[0].Replace("Time:", "").Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToList();
            var distances = lines[1].Replace("Distance:", "").Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToList();

            var Result = 1;
            var raceCounter = 0;
            foreach (var time in times)
            {
                var speed = 0;
                var winningDistances = new List<int>();
                for (int i = 0; i < time; i++)
                {
                    speed = i;
                    var distance = speed * (time - i);
                    if (distance > distances[raceCounter])
                    {
                        winningDistances.Add(distance);
                    }
                }

                Result *= winningDistances.Count();
                raceCounter++;
            }
            Console.WriteLine(Result);
        }

        public static void Part2()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input06.txt").ToList();
            var time = Int64.Parse(lines[0].Replace("Time:", "").Replace(" ", ""));
            var championDistance = Int64.Parse(lines[1].Replace("Distance:", "").Replace(" ", ""));


            Int64 speed = 0;
            var winningDistances = new List<Int64>();
            for (Int64 i = 0; i < time; i++)
            {
                speed = i;
                var distance = speed * (time - i);
                if (distance > championDistance)
                {
                    winningDistances.Add(distance);
                }
            }


            Console.WriteLine(winningDistances.Count());
        }
    }
}
