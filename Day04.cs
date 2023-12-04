using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day04
    {
        public static void Part1()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input04.txt").ToList();
            var scratchCards = new List<ScratchCard>();
            foreach (var line in lines)
            {
                var numbers = line.Split(": ")[1].Split(" | ");
                var scratch = new ScratchCard();
                scratch.WinningNumbers = numbers[0].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x)).ToList();
                scratch.ActualNumbers = numbers[1].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x)).ToList();
                scratchCards.Add(scratch);
            }

            Console.WriteLine(scratchCards.Sum(s => s.Points));
        }

        public static void Part2()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input04.txt").ToList();
            var scratchCards = new List<ScratchCard>();
            foreach (var line in lines)
            {
                var numbers = line.Split(": ")[1].Split(" | ");
                var scratch = new ScratchCard();
                scratch.WinningNumbers = numbers[0].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x)).ToList();
                scratch.ActualNumbers = numbers[1].Trim().Replace("  ", " ").Split(" ").Select(x => int.Parse(x)).ToList();
                scratch.ID = line.Split(": ")[0].Replace("Card","").Trim();
                scratchCards.Add(scratch);
            }

            Dictionary<int, int> amounts = new Dictionary<int, int>();
            for (int i = 1; i <= scratchCards.Count; i++)
            {
                amounts.Add(i, 1);
            }

            foreach (var scratchCard in scratchCards)
            {
                var id = int.Parse(scratchCard.ID);
                var multiplier = amounts[id];
                var winners = scratchCard.AmountOfWinners;

                for (int i = id + 1; i <= id + winners; i++)
                {
                    if (i <= scratchCards.Count)
                    {
                        amounts[i] += multiplier;
                    }
                }
            }

            Console.WriteLine(amounts.Values.Sum());
        }

        public class ScratchCard
        {
            public string ID { get; set; }
            public List<int> WinningNumbers { get; set; }
            public List<int> ActualNumbers { get; set; }

            public double AmountOfWinners
            {
                get
                {
                    return WinningNumbers.Intersect(ActualNumbers).ToList().Count();
                }
            }


            public double Points
            {
                get
                {
                    var duplicates = WinningNumbers.Intersect(ActualNumbers).ToList();
                    if (duplicates.Count == 1)
                        return 1;
                    else if (duplicates.Count == 0)
                        return 0;
                    else
                        return Math.Pow(2, Convert.ToDouble(duplicates.Count) - 1);

                }
            }
        }
    }
}
