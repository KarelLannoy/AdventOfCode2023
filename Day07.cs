using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day07
    {
        private static Dictionary<char, int> _values = new Dictionary<char, int>();

        public static void Part1()
        {
            _values.Add('2', 1);
            _values.Add('3', 2);
            _values.Add('4', 3);
            _values.Add('5', 4);
            _values.Add('6', 5);
            _values.Add('7', 6);
            _values.Add('8', 7);
            _values.Add('9', 8);
            _values.Add('T', 9);
            _values.Add('J', 10);
            _values.Add('Q', 11);
            _values.Add('K', 12);
            _values.Add('A', 13);

            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input07.txt").ToList();
            var hands = new List<Hand>();
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                hands.Add(new Hand() { Cards = parts[0], Bid = int.Parse(parts[1]) });
            }

            hands.Sort();

            var result = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                result += (i + 1) * hands[i].Bid;
            }
            Console.WriteLine(result);
        }

        public static void Part2()
        {
            _values.Clear();
            _values.Add('J', 0);
            _values.Add('2', 1);
            _values.Add('3', 2);
            _values.Add('4', 3);
            _values.Add('5', 4);
            _values.Add('6', 5);
            _values.Add('7', 6);
            _values.Add('8', 7);
            _values.Add('9', 8);
            _values.Add('T', 9);
            _values.Add('Q', 10);
            _values.Add('K', 11);
            _values.Add('A', 12);

            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input07.txt").ToList();

            var hands = new List<ComplexHand>();
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                hands.Add(new ComplexHand() { Cards = parts[0], Bid = int.Parse(parts[1]) });
            }

            hands.Sort();

            Int64 result = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                result += Convert.ToInt64(i + 1) * Convert.ToInt64(hands[i].Bid);
            }
            Console.WriteLine(result);


        }

        private class Hand : IComparable<Hand>
        {
            public string Cards { get; set; }
            public int Bid { get; set; }

            public int GetHandType()
            {
                var groups = this.Cards.GroupBy(x => x).ToList();

                if (groups.Any(g => g.Count() == 5))
                    return 7;
                if (groups.Any(g => g.Count() == 4))
                    return 6;
                if (groups.Any(g => g.Count() == 3) && groups.Any(g => g.Count() == 2))
                    return 5;
                if (groups.Any(g => g.Count() == 3) && !groups.Any(g => g.Count() == 2))
                    return 4;
                if (groups.Where(g => g.Count() == 2).Count() == 2)
                    return 3;
                if (groups.Where(g => g.Count() == 2).Count() == 1)
                    return 2;
                return 1;
            }

            public int CompareTo(Hand? other)
            {
                var otherHandType = other.GetHandType();
                var thisHandType = this.GetHandType();

                if (otherHandType > thisHandType)
                    return -1;
                if (otherHandType < thisHandType)
                    return 1;

                for (int i = 0; i < this.Cards.Length; i++)
                {
                    var thisCard = this.Cards[i];
                    var otherCard = other.Cards[i];

                    if (thisCard == otherCard)
                        continue;

                    if (_values[otherCard] > _values[thisCard])
                        return -1;
                    if (_values[otherCard] < _values[thisCard])
                        return 1;
                }

                return 0;
            }
        }

        private class ComplexHand : IComparable<ComplexHand>
        {
            public string Cards { get; set; }
            public int Bid { get; set; }

            public int GetHandType()
            {
                var numberOfJokers = this.Cards.Count(c => c == 'J');

                var handWithoutJokers = this.Cards.Replace("J", "");
                var groups = handWithoutJokers.GroupBy(x => x).ToList();

                if (groups.Any(g => g.Count() == 5 - numberOfJokers) || numberOfJokers == 5)
                    return 7;
                if (groups.Any(g => g.Count() == 4 - numberOfJokers))
                    return 6;
                if ((groups.Any(g => g.Count() == 3) && groups.Any(g => g.Count() == 2)) || (groups.Where(g => g.Count() == 2).Count() == 2 && numberOfJokers > 0))
                    return 5;
                if (groups.Any(g => g.Count() == 3 - numberOfJokers))
                    return 4;
                if (groups.Where(g => g.Count() == 2).Count() == 2)
                    return 3;
                if (groups.Where(g => g.Count() == 2).Count() == 1 || numberOfJokers > 0)
                    return 2;
                return 1;
            }

            public int CompareTo(ComplexHand? other)
            {
                var otherHandType = other.GetHandType();
                var thisHandType = this.GetHandType();

                if (otherHandType > thisHandType)
                    return -1;
                if (otherHandType < thisHandType)
                    return 1;

                for (int i = 0; i < this.Cards.Length; i++)
                {
                    var thisCard = this.Cards[i];
                    var otherCard = other.Cards[i];

                    if (thisCard == otherCard)
                        continue;

                    if (_values[otherCard] > _values[thisCard])
                        return -1;
                    if (_values[otherCard] < _values[thisCard])
                        return 1;
                }

                return 0;
            }
        }
    }
}
