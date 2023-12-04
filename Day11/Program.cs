using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var sinput = @"";
            //var input = @"";

            //var lines = input.Split('\n');
            //foreach (var lineugly in lines)
            //{
            //}
            //Monkeys.Monkey = new Monkey[4];
            //Monkeys.Monkey[0] = new Monkey((w) => w * 19, 23, 2, 3, new[] { 79, 98 });
            //Monkeys.Monkey[1] = new Monkey((w) => w + 6, 19, 2, 0, new[] { 54, 65, 75, 74 });
            //Monkeys.Monkey[2] = new Monkey((w) => w * w, 13, 1, 3, new[] { 79, 60, 97 });
            //Monkeys.Monkey[3] = new Monkey((w) => w + 3, 17, 0, 1, new[] { 74 });

            Monkeys.Monkey = new Monkey[8];
            Monkeys.Monkey[0] = new Monkey((w) => w * 17, 2, 2, 6, new[] { 85, 79, 63, 72 });
            Monkeys.Monkey[1] = new Monkey((w) => w * w, 7, 0, 2, new[] { 53, 94, 65, 81, 93, 73, 57, 92 });
            Monkeys.Monkey[2] = new Monkey((w) => w + 7, 13, 7, 6, new[] { 62, 63 });
            Monkeys.Monkey[3] = new Monkey((w) => w + 4, 5, 4, 5, new[] { 57, 92, 56 });
            Monkeys.Monkey[4] = new Monkey((w) => w + 5, 3, 1, 5, new[] { 67 });
            Monkeys.Monkey[5] = new Monkey((w) => w + 6, 19, 1, 0, new[] { 85, 56, 66, 72, 57, 99 });
            Monkeys.Monkey[6] = new Monkey((w) => w * 13, 11, 3, 7, new[] { 86, 65, 98, 97, 69 });
            Monkeys.Monkey[7] = new Monkey((w) => w + 2, 17, 4, 3, new[] { 87, 68, 92, 66, 91, 50, 68 });
            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in Monkeys.Monkey)
                {
                    monkey.InspectItems();
                }
            }

            var topMonkeys = Monkeys.Monkey.OrderByDescending(m => m.Inspections).Select(m =>m.Inspections).Take(2).ToArray();
            Console.WriteLine((long)topMonkeys[0] * topMonkeys[1]);
        }

        static class Monkeys
        {
            public static Monkey[] Monkey { get; set; }
        }

        class Monkey
        {
            public Queue<int> Items { get; } = new Queue<int>();

            public int Inspections { get; private set; }

            private Func<int, int> IncreaseWorry { get; }
            private uint DivisibleBy { get; }

            private int ThrowYes { get; }
            private int ThrowNo { get; }

            public void InspectItems()
            {
                while (Items.Count > 0)
                {
                    int worry = Items.Dequeue();
                    worry = IncreaseWorry(worry) % 9699690;/// 3;
                    Monkeys.Monkey[worry % DivisibleBy == 0 ? ThrowYes : ThrowNo].Items.Enqueue(worry);
                    Inspections++;
                }
            }

            public Monkey(Func<int, int> increaseWorry, uint divisibleBy, int throwYes, int throwNo, IEnumerable<int> start)
            {
                IncreaseWorry = increaseWorry;
                DivisibleBy = divisibleBy;
                ThrowYes = throwYes;
                ThrowNo = throwNo;

                foreach (int w in start)
                {
                    Items.Enqueue(w);
                }
            }
        }
    }
}
