// See https://aka.ms/new-console-template for more information
using AdventOfCode2023;
using System.Diagnostics;

Stopwatch sw = new Stopwatch();
sw.Start();

Day07.Part1();

sw.Stop();
Console.WriteLine($"Timing: {sw.Elapsed}");
sw.Reset();
sw.Start();

Day07.Part2();

sw.Stop();
Console.WriteLine($"Timing: {sw.Elapsed}");
Console.ReadLine();
