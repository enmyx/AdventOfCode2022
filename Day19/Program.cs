using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sinput = @"1:4.2.3 14.2 7
2:2.3.3 8.3 12";
            var input = @"1:3.4.3 10.2 7
2:4.4.4 12.3 8
3:4.4.4 17.2 13
4:2.2.2 20.2 14
5:3.4.4 5.3 12
6:2.4.4 11.3 8
7:3.4.3 17.3 7
8:2.3.3 11.2 16
9:3.3.3 9.3 7
10:4.4.4 9.4 16
11:4.4.2 16.4 16
12:2.3.2 17.3 19
13:4.4.2 10.3 14
14:4.3.2 13.2 10
15:3.3.2 19.2 12
16:4.3.2 20.3 9
17:3.4.2 11.2 10
18:2.4.4 18.2 11
19:4.4.3 14.3 8
20:4.4.4 8.4 14
21:2.4.3 20.2 17
22:3.4.3 15.4 16
23:4.4.2 17.3 11
24:3.3.2 13.3 12
25:3.3.3 19.3 17
26:3.4.3 12.3 17
27:3.4.2 15.3 7
28:3.4.4 14.4 10
29:3.3.2 15.3 9
30:4.4.2 7.4 13";
            var lines = input.Split('\n');

            var blueprints = new List<Blueprint>();
            foreach (var lineugly in lines)
            {
                var line = lineugly.TrimEnd('\r');
                var data = line.Split(':')[1].Split('.');
                var obs = data[2].Split(' ');
                var geo = data[3].Split(' ');
                blueprints.Add(new Blueprint(int.Parse(data[0]), int.Parse(data[1]), 
                    (int.Parse(obs[0]), int.Parse(obs[1])), (int.Parse(geo[0]), int.Parse(geo[1]))));
            }

            var results = new Dictionary<int, int>();
            Parallel.For(0, Math.Min(3, blueprints.Count), i =>
            {
                var blueprint = blueprints[i];
                var value = Step(blueprint, 32, new Dictionary<string, int>());
                results.Add(i + 1, value);
                Console.WriteLine(i + 1 + ": " + value);
            });

            int sum = 0;
            foreach (var result in results)
            {
                sum += result.Value * result.Key;
            }

            Console.WriteLine(results.Values.Sum());
        }

        private static int Step(Blueprint blueprint, int max, Dictionary<string, int> values)
        {
            if (max == 0)
                return blueprint.Material[3];

            OptimizeBlueprint(blueprint, max);

            string key = $"{max}_{blueprint.Robots[0]}_{blueprint.Robots[1]}_{blueprint.Robots[2]}_{blueprint.Robots[3]}_{blueprint.Material[0]}_{blueprint.Material[1]}_{blueprint.Material[2]}_{blueprint.Material[3]}";
            if (values.ContainsKey(key))
                return values[key];

            int value = 0;
            for (int i = 3; i >= 0; i--)
            {
                if (blueprint.CanCreateRobot(i))
                {
                    var copyPrint = new Blueprint(blueprint);
                    copyPrint.CreateRobot(i);
                    value = Math.Max(value, Step(copyPrint, max - 1, values));
                }
            }

            // Do nothing.
            var copy = new Blueprint(blueprint);
            copy.Collect();
            value = Math.Max(value, Step(copy, max - 1, values));

            values.Add(key, value);

            return value;
        }

        private static void OptimizeBlueprint(Blueprint blueprint, int max)
        {
            for (int i = 0; i < 3; i++)
            {
                // Don't buy more robot than needed.
                if (blueprint.Robots[i] > blueprint.MaxCosts[i])
                {
                    blueprint.Robots[i] = blueprint.MaxCosts[i];
                }

                // Don't collect more material than needed.
                var maxNeeded = max * blueprint.MaxCosts[i] - (max - 1) * blueprint.Robots[i];
                if (blueprint.Material[i] > maxNeeded)
                {
                    blueprint.Material[i] = maxNeeded;
                }
            }
        }
    }

    class Blueprint
    {
        public int[] Material { get; }

        public int[] Robots { get; }

        public int[] MaxCosts { get; }

        public int[,] Costs { get; }

        public Blueprint(Blueprint from)
        {
            Costs = from.Costs;
            MaxCosts = from.MaxCosts;
            Material = new int[4] { from.Material[0], from.Material[1], from.Material[2], from.Material[3] };
            Robots = new int[4] { from.Robots[0], from.Robots[1], from.Robots[2], from.Robots[3] };
        }
        public Blueprint(int ore, int clay, (int, int) obsidian, (int, int) geode)
        {
            Costs = new int[,]
            {
                { ore, 0, 0, 0 },
                { clay, 0, 0, 0 },
                { obsidian.Item1, obsidian.Item2, 0, 0 },
                { geode.Item1, 0, geode.Item2, 0 }
            };
            MaxCosts = new int[]
            {
                Math.Max(Math.Max(Math.Max(ore, clay), obsidian.Item1), geode.Item1),
                obsidian.Item2,
                geode.Item2,
                0
            };
            Material = new int[4];
            Robots = new int[4] {1,0,0,0};
        }

        public void Collect()
        {
            for (int i = 0; i < 4; i++)
            {
                Material[i] += Robots[i];
            }
        }

        public void CreateRobot(int i)
        {
            if (CanCreateRobot(i))
            {
                for (int j = 0; j < 4; j++)
                {
                    Material[j] -= Costs[i, j];
                }
                Collect();
                Robots[i]++;
            }
        }

        public bool CanCreateRobot(int i)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Material[j] < Costs[i, j])
                    return false;
            }
            return true;
        }
    }
}
