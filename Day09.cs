using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2023
{
    public static class Day09
    {

        public static void Part1()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input09.txt").ToList();
            Int64 result = 0;

            foreach (var line in lines)
            {
                var numbers = line.Split(' ').Select(n=>Int64.Parse(n)).ToList();
                var diffs = new List<List<Int64>>();
                diffs.Add(numbers);
                while (!diffs.Last().All(n=>n == 0)) 
                {
                    List<Int64> newDiff = new List<Int64>();
                    for (int i = 0; i < diffs.Last().Count-1; i++)
                    {
                        var diff = diffs.Last()[i + 1] - diffs.Last()[i];
                        newDiff.Add(diff);
                    }
                    diffs.Add(newDiff);
                }

                diffs.Last().Add(0);
                for (int i = diffs.Count -2; i >= 0; i--)
                {
                    diffs[i].Add(diffs[i].Last() + diffs[i+1].Last());
                }

                result += diffs[0].Last();
            }

            Console.WriteLine(result);
        }

        public static void Part2()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input09.txt").ToList();
            Int64 result = 0;

            foreach (var line in lines)
            {
                var numbers = line.Split(' ').Select(n => Int64.Parse(n)).ToList();
                var diffs = new List<List<Int64>>();
                diffs.Add(numbers);
                while (!diffs.Last().All(n => n == 0))
                {
                    List<Int64> newDiff = new List<Int64>();
                    for (int i = 0; i < diffs.Last().Count - 1; i++)
                    {
                        var diff = diffs.Last()[i + 1] - diffs.Last()[i];
                        newDiff.Add(diff);
                    }
                    diffs.Add(newDiff);
                }

                diffs.Last().Insert(0, 0);
                for (int i = diffs.Count - 2; i >= 0; i--)
                {
                    diffs[i].Insert(0,(diffs[i].First() - diffs[i + 1].First()));
                }

                result += diffs[0].First();
            }

            Console.WriteLine(result);

        }
    }
}
