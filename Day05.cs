using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class Day05
    {
        public static void Part1()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input05.txt").ToList();
            var seeds = lines[0].Replace("seeds: ", "").Split(" ").Select(s => Int64.Parse(s)).ToList();

            //Seed To Soil map
            var counter = 3;
            var seedToSoil = new List<ItemRange>();
            counter = ExtractData(lines, counter, seedToSoil);

            counter += 2;
            var soilToFertilizer = new List<ItemRange>();
            counter = ExtractData(lines, counter, soilToFertilizer);

            counter += 2;
            var fertilizerToWater = new List<ItemRange>();
            counter = ExtractData(lines, counter, fertilizerToWater);

            counter += 2;
            var waterToLight = new List<ItemRange>();
            counter = ExtractData(lines, counter, waterToLight);

            counter += 2;
            var lightToTemp = new List<ItemRange>();
            counter = ExtractData(lines, counter, lightToTemp);

            counter += 2;
            var tempToHumidity = new List<ItemRange>();
            counter = ExtractData(lines, counter, tempToHumidity);

            counter += 2;
            var humidityToLocation = new List<ItemRange>();
            counter = ExtractData(lines, counter, humidityToLocation);

            var locations = new List<Int64>();

            foreach (var seed in seeds)
            {
                var soil = seed;
                var soilRange = seedToSoil.FirstOrDefault(s => s.Source <= seed && s.Source + s.Length > seed);
                if (soilRange != null)
                {
                    soil = soilRange.Destination + (seed - soilRange.Source);
                }

                var fert = soil;
                var fertRange = soilToFertilizer.FirstOrDefault(s => s.Source <= soil && s.Source + s.Length > soil);
                if (fertRange != null)
                {
                    fert = fertRange.Destination + (soil - fertRange.Source);
                }

                var water = fert;
                var waterRange = fertilizerToWater.FirstOrDefault(s => s.Source <= fert && s.Source + s.Length > fert);
                if (waterRange != null)
                {
                    water = waterRange.Destination + (fert - waterRange.Source);
                }

                var light = water;
                var lightRange = waterToLight.FirstOrDefault(s => s.Source <= water && s.Source + s.Length > water);
                if (lightRange != null)
                {
                    light = lightRange.Destination + (water - lightRange.Source);
                }

                var temp = light;
                var tempRange = lightToTemp.FirstOrDefault(s => s.Source <= light && s.Source + s.Length > light);
                if (tempRange != null)
                {
                    temp = tempRange.Destination + (light - tempRange.Source);
                }

                var hum = temp;
                var humRange = tempToHumidity.FirstOrDefault(s => s.Source <= temp && s.Source + s.Length > temp);
                if (humRange != null)
                {
                    hum = humRange.Destination + (temp - humRange.Source);
                }

                var location = hum;
                var locRange = humidityToLocation.FirstOrDefault(s => s.Source <= hum && s.Source + s.Length > hum);
                if (locRange != null)
                {
                    location = locRange.Destination + (hum - locRange.Source);
                }

                locations.Add(location);
            }
            Console.WriteLine(locations.Min());
        }

        private static int ExtractData(List<string> lines, int counter, List<ItemRange> itemRanges)
        {
            while (counter < lines.Count && lines[counter] != "")
            {
                var lineParts = lines[counter].Split(" ");
                var source = Int64.Parse(lineParts[1]);
                var destination = Int64.Parse(lineParts[0]);
                var length = Int64.Parse(lineParts[2]);
                itemRanges.Add(new ItemRange() { Source = source, Destination = destination, Length = length });
                counter++;
            }

            return counter;
        }

        public static void Part2()
        {
            var lines = File.ReadAllLines(@"C:\temp\AOC2023\Input05.txt").ToList();
            var seedRanges = lines[0].Replace("seeds: ", "").Split(" ").Select(s => Int64.Parse(s)).ToList();
            var seeds = new List<Tuple<Int64, Int64>>();
            for (int i = 0; i < seedRanges.Count - 1; i += 2)
            {
                seeds.Add(new Tuple<long, long>(seedRanges[i], seedRanges[i + 1]));
            }

            //Seed To Soil map
            var counter = 3;
            var seedToSoil = new List<ItemRange>();
            counter = ExtractData(lines, counter, seedToSoil);

            counter += 2;
            var soilToFertilizer = new List<ItemRange>();
            counter = ExtractData(lines, counter, soilToFertilizer);

            counter += 2;
            var fertilizerToWater = new List<ItemRange>();
            counter = ExtractData(lines, counter, fertilizerToWater);

            counter += 2;
            var waterToLight = new List<ItemRange>();
            counter = ExtractData(lines, counter, waterToLight);

            counter += 2;
            var lightToTemp = new List<ItemRange>();
            counter = ExtractData(lines, counter, lightToTemp);

            counter += 2;
            var tempToHumidity = new List<ItemRange>();
            counter = ExtractData(lines, counter, tempToHumidity);

            counter += 2;
            var humidityToLocation = new List<ItemRange>();
            counter = ExtractData(lines, counter, humidityToLocation);

            var locationResult = Int64.MaxValue;

            foreach (var seedPair in seeds)
            {
                
                var location = CalculateRange(seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemp, tempToHumidity, humidityToLocation, seedPair);
                if (locationResult > location)
                {
                    locationResult = location;
                }
            }

            Console.WriteLine(locationResult);
        }

        private static long CalculateRange(List<ItemRange> seedToSoil, List<ItemRange> soilToFertilizer, List<ItemRange> fertilizerToWater, List<ItemRange> waterToLight, List<ItemRange> lightToTemp, List<ItemRange> tempToHumidity, List<ItemRange> humidityToLocation, Tuple<long, long> seedPair)
        {
            var firstOfRange = seedPair.Item1;
            var lastOfRange = seedPair.Item1 + seedPair.Item2 - 1;
            long firstLocation = GetLocation(seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemp, tempToHumidity, humidityToLocation, firstOfRange);
            long lastLocation = GetLocation(seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemp, tempToHumidity, humidityToLocation, lastOfRange);

            if (lastLocation != firstLocation + seedPair.Item2 - 1)
            {
                var range1 = new Tuple<Int64, Int64>(firstOfRange,  (seedPair.Item2 / 2));
                var location1 = CalculateRange(seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemp, tempToHumidity, humidityToLocation, range1);              
                var range2 = new Tuple<Int64, Int64>(firstOfRange + seedPair.Item2 / 2, seedPair.Item2 / 2);
                var location2 = CalculateRange(seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemp, tempToHumidity, humidityToLocation, range2);
                return Math.Min(location1, location2);
            }else
            {
                return firstLocation;
            }
        }

        private static long GetLocation(List<ItemRange> seedToSoil, List<ItemRange> soilToFertilizer, List<ItemRange> fertilizerToWater, List<ItemRange> waterToLight, List<ItemRange> lightToTemp, List<ItemRange> tempToHumidity, List<ItemRange> humidityToLocation, long i)
        {
            var seed = i;
            var soil = seed;
            var soilRange = seedToSoil.FirstOrDefault(s => s.Source <= seed && s.Source + s.Length > seed);
            if (soilRange != null)
            {
                soil = soilRange.Destination + (seed - soilRange.Source);
            }

            var fert = soil;
            var fertRange = soilToFertilizer.FirstOrDefault(s => s.Source <= soil && s.Source + s.Length > soil);
            if (fertRange != null)
            {
                fert = fertRange.Destination + (soil - fertRange.Source);
            }

            var water = fert;
            var waterRange = fertilizerToWater.FirstOrDefault(s => s.Source <= fert && s.Source + s.Length > fert);
            if (waterRange != null)
            {
                water = waterRange.Destination + (fert - waterRange.Source);
            }

            var light = water;
            var lightRange = waterToLight.FirstOrDefault(s => s.Source <= water && s.Source + s.Length > water);
            if (lightRange != null)
            {
                light = lightRange.Destination + (water - lightRange.Source);
            }

            var temp = light;
            var tempRange = lightToTemp.FirstOrDefault(s => s.Source <= light && s.Source + s.Length > light);
            if (tempRange != null)
            {
                temp = tempRange.Destination + (light - tempRange.Source);
            }

            var hum = temp;
            var humRange = tempToHumidity.FirstOrDefault(s => s.Source <= temp && s.Source + s.Length > temp);
            if (humRange != null)
            {
                hum = humRange.Destination + (temp - humRange.Source);
            }

            var location = hum;
            var locRange = humidityToLocation.FirstOrDefault(s => s.Source <= hum && s.Source + s.Length > hum);
            if (locRange != null)
            {
                location = locRange.Destination + (hum - locRange.Source);
            }

            return location;
        }

        private class ItemRange
        {
            public long Source { get; set; }
            public long Destination { get; set; }
            public long Length { get; set; }
        }
    }
}
