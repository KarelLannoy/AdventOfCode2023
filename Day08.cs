using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2023
{
    public static class Day08
    {

        public static void Part1()
        {


            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input08.txt").ToList();

            var instructions = lines[0].ToList();

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            for (int i = 2; i < lines.Count; i++)
            {
                var parts = lines[i].Split(" = ");
                var id = parts[0];
                var leftRight = parts[1].Replace("(", "").Replace(")", "").Split(", ");
                nodes.Add(id, new Node() { Id = id, Left = leftRight[0], Right = leftRight[1] });
            }

            var instructionPointer = 0;
            var currentNode = "AAA";

            while (currentNode != "ZZZ")
            {
                var nextMove = instructions[instructionPointer % instructions.Count];

                if (nextMove == 'L')
                    currentNode = nodes[currentNode].Left;
                if (nextMove == 'R')
                    currentNode = nodes[currentNode].Right;
                instructionPointer++;
            }

            Console.WriteLine(instructionPointer);
        }

        public static void Part2()
        {

            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input08.txt").ToList();

            var instructions = lines[0].ToList();

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            for (int i = 2; i < lines.Count; i++)
            {
                var parts = lines[i].Split(" = ");
                var id = parts[0];
                var leftRight = parts[1].Replace("(", "").Replace(")", "").Split(", ");
                nodes.Add(id, new Node() { Id = id, Left = leftRight[0], Right = leftRight[1] });
            }


            var startNodes = nodes.Keys.Where(k => k.EndsWith('A')).ToList();
            var startNodesInterval = new List<Int64>();
            foreach (var item in startNodes)
            {
                var currentNode = item;
                var instructionPointer = 0;
                while (!currentNode.EndsWith('Z'))
                {
                    var nextMove = instructions[instructionPointer % instructions.Count];

                    if (nextMove == 'L')
                        currentNode = nodes[currentNode].Left;
                    if (nextMove == 'R')
                        currentNode = nodes[currentNode].Right;
                    instructionPointer++;
                }
                startNodesInterval.Add(instructionPointer);
            }

            var result = Helpers.LcmOfList(startNodesInterval);
            Console.WriteLine(result);

        }

        private class Node
        {
            public string Id { get; set; }
            public string Left { get; set; }
            public string Right { get; set; }
        }
    }
}
