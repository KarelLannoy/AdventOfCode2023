using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day02
    {
        public static int MaxRed = 12;
        public static int MaxBlue = 14;
        public static int MaxGreen = 13;


        public static void Part1()
        {
            var games = ParseGames();
            var validGames = new List<int>();

            foreach (var game in games)
            {
                if (IsValid(game))
                    validGames.Add(game.ID);
            }

            Console.WriteLine(validGames.Sum());
        }

        private static bool IsValid(Game game)
        {
            foreach (var draw in game.Draws)
            {
                if (draw.Red > MaxRed || draw.Green > MaxGreen || draw.Blue > MaxBlue)
                {
                    return false;
                }
            }
            return true; ;
        }

        public static void Part2()
        {
            var games = ParseGames();
            var powers = new List<long>();
            foreach (var game in games)
            {
                powers.Add(GetMinimumPower(game));
            }
            Console.WriteLine(powers.Sum());
        }

        private static long GetMinimumPower(Game game)
        {
            var redMax = game.Draws.Max(d => d.Red);
            var greenMax = game.Draws.Max(d => d.Green);
            var blueMax = game.Draws.Max(d => d.Blue);

            return (redMax * greenMax * blueMax);
        }

        private static List<Game> ParseGames()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input02.txt").ToList();
            var games = new List<Game>();
            foreach (var line in lines)
            {
                var game = new Game();
                game.ID = int.Parse(line.Substring(5).Split(':').First());
                game.Draws = new List<Draw>();

                var drawString = line.Split(": ")[1].ToString();
                var draws = drawString.Split("; ").ToList();
                foreach (var draw in draws)
                {
                    var newDraw = new Draw();
                    var kubes = draw.Split(", ").ToList();
                    foreach (var kube in kubes)
                    {
                        var parts = kube.Split(" ");
                        if (parts[1] == "green")
                            newDraw.Green = Int64.Parse(parts[0]);
                        if (parts[1] == "red")
                            newDraw.Red = Int64.Parse(parts[0]);
                        if (parts[1] == "blue")
                            newDraw.Blue = Int64.Parse(parts[0]);
                    }
                    game.Draws.Add(newDraw);
                }
                games.Add(game);
            }
            return games;
        }

        public class Game
        {
            public int ID { get; set; }
            public List<Draw> Draws { get; set; }
        }

        public class Draw
        {
            public long Red { get; set; }

            public long Green { get; set; }

            public long Blue { get; set; }
        }
    }
}
