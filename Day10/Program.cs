using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sinput = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";
            var input = @"addx 1
addx 4
addx -2
addx 3
addx 3
addx 1
noop
addx 5
noop
noop
noop
addx 5
addx 2
addx 3
noop
addx 2
addx 4
noop
addx -1
noop
addx 3
addx -10
addx -17
noop
addx -3
addx 2
addx 25
addx -24
addx 2
addx 5
addx 2
addx 3
noop
addx 2
addx 14
addx -9
noop
addx 5
noop
noop
addx -2
addx 5
addx 2
addx -5
noop
noop
addx -19
addx -11
addx 5
addx 3
noop
addx 2
addx 3
addx -2
addx 2
noop
addx 3
addx 4
noop
noop
addx 5
noop
noop
noop
addx 5
addx -3
addx 8
noop
addx -15
noop
addx -12
addx -9
noop
addx 6
addx 7
addx -6
addx 4
noop
noop
noop
addx 4
addx 1
addx 5
addx -11
addx 29
addx -15
noop
addx -12
addx 17
addx 7
noop
noop
addx -32
addx 3
addx -8
addx 7
noop
addx -2
addx 5
addx 2
addx 6
addx -8
addx 5
addx 2
addx 5
addx 17
addx -12
addx -2
noop
noop
addx 7
addx 9
addx -8
addx 2
addx -33
addx -1
addx 2
noop
addx 26
addx -22
addx 19
addx -16
addx 8
addx -1
addx 3
addx -2
addx 2
addx -17
addx 24
addx 1
noop
addx 5
addx -1
noop
addx 5
noop
noop
addx 1
noop
noop";

            var lines = input.Split('\n');
            int X = 1;
            var values = new List<int>();
            foreach (var lineugly in lines)
            {
                var line = lineugly.TrimEnd('\r');
                if (line == "noop")
                {
                    values.Add(X);
                }
                else
                {
                    values.Add(X);
                    values.Add(X);
                    var add = int.Parse(line.Split(' ')[1]);
                    X += add;
                }
            }

            //int signal = 0;
            //for (int i = 19; i < values.Count; i+=40)
            //{
            //    signal += (i + 1) * values[i];
            //}

            //Console.WriteLine(signal);
            for (int i = 0; i < values.Count; i++)
            {
                int pos = i % 40;
                char pixel = pos == values[i] || pos == values[i] - 1 || pos == values[i] + 1 ? '#' : '.';
                Console.Write(pixel);
                if ((i + 1) % 40 == 0)
                    Console.WriteLine();
            }
        }
    }
}
