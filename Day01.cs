using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day01
    {
        public static void Part1()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input01.txt").ToList();
            var numbers = new List<int>();

            foreach (var line in lines)
            {
                List<char> charList = new List<char>();
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsNumber(line[i]))
                    {
                        charList.Add(line[i]);
                    }
                }
                numbers.Add(int.Parse(charList.First().ToString() + charList.Last().ToString()));
            }
            Console.WriteLine(numbers.Sum());
        }

        public static void Part2()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input01.txt").ToList();
            var numbers = new List<int>();

            foreach (var line in lines)
            {

                List<char> charList = new List<char>();
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsNumber(line[i]))
                    {
                        charList.Add(line[i]);
                    }
                    else
                    {
                        if (line.Substring(i).StartsWith("one"))
                            charList.Add('1');
                        if (line.Substring(i).StartsWith("two"))
                            charList.Add('2');
                        if (line.Substring(i).StartsWith("three"))
                            charList.Add('3');
                        if (line.Substring(i).StartsWith("four"))
                            charList.Add('4');
                        if (line.Substring(i).StartsWith("five"))
                            charList.Add('5');
                        if (line.Substring(i).StartsWith("six"))
                            charList.Add('6');
                        if (line.Substring(i).StartsWith("seven"))
                            charList.Add('7');
                        if (line.Substring(i).StartsWith("eight"))
                            charList.Add('8');
                        if (line.Substring(i).StartsWith("nine"))
                            charList.Add('9');
                    }


                }
                numbers.Add(int.Parse(charList.First().ToString() + charList.Last().ToString()));
            }
            Console.WriteLine(numbers.Sum());
        }
    }
}
