using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day03
    {
        public static void Part1()
        {
            var text = File.ReadAllLines(@"C:\temp\AOC2023\Input03.txt").ToList();
            Dictionary<Point, char> schematic = new Dictionary<Point, char>();

            for (int y = 0; y < text.Count; y++)
            {
                for (int x = 0; x < text[y].Length; x++)
                {
                    if (text[y][x] != '.')
                    {
                        schematic.Add(new Point(x, y), text[y][x]);
                    }
                }
            }

            var machineParts = new List<long>();
            var symbols = schematic.Where(s => !char.IsNumber(s.Value)).ToDictionary();
            foreach (var symbol in symbols)
            {
                var adjPoints = GetAdjPoints(symbol.Key);
                var usedPoints = new List<Point>();
                foreach (var point in adjPoints)
                {
                    if (!usedPoints.Contains(point) && schematic.ContainsKey(point))
                    {
                        if (char.IsNumber(schematic[point]))
                        {
                            var beginX = 0;
                            for (int i = point.X - 1; i >= 0; i--)
                            {
                                if (!schematic.ContainsKey(new Point(i, point.Y)) || !char.IsNumber(schematic[new Point(i, point.Y)]))
                                {
                                    beginX = i + 1;
                                    break;
                                }
                            }

                            var number = "";
                            for (int i = beginX; i < text[0].Length; i++)
                            {
                                if (schematic.ContainsKey(new Point(i, point.Y)) && char.IsNumber(schematic[new Point(i, point.Y)]))
                                {
                                    number += schematic[new Point(i, point.Y)];
                                    usedPoints.Add(new Point(i, point.Y));
                                }else
                                {
                                    break;
                                }
                            }
                            machineParts.Add(Int64.Parse(number));
                        }
                    }
                }
            }
            Console.WriteLine(machineParts.Sum());
        }

        private static List<Point> GetAdjPoints(Point key)
        {
            var newList = new List<Point>();
            newList.Add(new Point(key.X - 1, key.Y));
            newList.Add(new Point(key.X - 1, key.Y - 1));
            newList.Add(new Point(key.X, key.Y - 1));
            newList.Add(new Point(key.X + 1, key.Y - 1));
            newList.Add(new Point(key.X + 1, key.Y));
            newList.Add(new Point(key.X + 1, key.Y + 1));
            newList.Add(new Point(key.X, key.Y + 1));
            newList.Add(new Point(key.X - 1, key.Y + 1));
            return newList;
        }

        public static void Part2()
        {
            var text = File.ReadAllLines(@"C:\temp\AOC2023\Input03.txt").ToList();
            Dictionary<Point, char> schematic = new Dictionary<Point, char>();

            for (int y = 0; y < text.Count; y++)
            {
                for (int x = 0; x < text[y].Length; x++)
                {
                    if (text[y][x] != '.')
                    {
                        schematic.Add(new Point(x, y), text[y][x]);
                    }
                }
            }

            var gearRatios = new List<long>();
            var symbols = schematic.Where(s => s.Value == '*').ToDictionary();
            foreach (var symbol in symbols)
            {
                var gearParts = new List<long>();
                var adjPoints = GetAdjPoints(symbol.Key);
                var usedPoints = new List<Point>();
                foreach (var point in adjPoints)
                {
                    if (!usedPoints.Contains(point) && schematic.ContainsKey(point))
                    {
                        if (char.IsNumber(schematic[point]))
                        {
                            var beginX = 0;
                            for (int i = point.X - 1; i >= 0; i--)
                            {
                                if (!schematic.ContainsKey(new Point(i, point.Y)) || !char.IsNumber(schematic[new Point(i, point.Y)]))
                                {
                                    beginX = i + 1;
                                    break;
                                }
                            }

                            var number = "";
                            for (int i = beginX; i < text[0].Length; i++)
                            {
                                if (schematic.ContainsKey(new Point(i, point.Y)) && char.IsNumber(schematic[new Point(i, point.Y)]))
                                {
                                    number += schematic[new Point(i, point.Y)];
                                    usedPoints.Add(new Point(i, point.Y));
                                }
                                else
                                {
                                    break;
                                }
                            }
                            gearParts.Add(Int64.Parse(number));
                        }
                    }
                }
                if (gearParts.Count == 2)
                {
                    gearRatios.Add(gearParts[0] * gearParts[1]);
                }
            }
            Console.WriteLine(gearRatios.Sum());

        }
    }
}
