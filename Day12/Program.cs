using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sinput = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";
            var input = @"abccccccccccccccccccaaaaaaaaacccccccccccccccccccccccccccccccccccccaaaa
abcccccccccccccccaaaaaaaaaaacccccccccccccccccccccccccccccccccccccaaaaa
abcaaccaacccccccccaaaaaaaaaacccccccccccccccccccccaaacccccccccccccaaaaa
abcaaaaaaccccccccaaaaaaaaaaaaacccccccccccccccccccaacccccccccccccaaaaaa
abcaaaaaacccaaacccccaaaaaaaaaaaccccccccccccccccccaaaccccccccccccccccaa
abaaaaaaacccaaaaccccaaaaaacaaaacccccccccccaaaacjjjacccccccccccccccccca
abaaaaaaaaccaaaaccccaaaaaaccccccaccccccccccaajjjjjkkcccccccccccccccccc
abaaaaaaaaccaaacccccccaaaccccccaaccccccccccajjjjjjkkkaaacccaaaccaccccc
abccaaacccccccccccccccaaccccaaaaaaaacccccccjjjjoookkkkaacccaaaaaaccccc
abcccaacccccccccccccccccccccaaaaaaaaccccccjjjjoooookkkkcccccaaaaaccccc
abcccccccaacccccccccccccccccccaaaacccccccijjjoooooookkkkccaaaaaaaccccc
abccaaccaaaccccccccccccccccccaaaaacccccciijjooouuuoppkkkkkaaaaaaaacccc
abccaaaaaaaccccccccccaaaaacccaacaaaccciiiiiooouuuuupppkkklllaaaaaacccc
abccaaaaaacccccccccccaaaaacccacccaaciiiiiiqooouuuuuupppkllllllacaccccc
abcccaaaaaaaacccccccaaaaaaccccaacaiiiiiqqqqoouuuxuuupppppplllllccccccc
abccaaaaaaaaaccaaaccaaaaaaccccaaaaiiiiqqqqqqttuxxxuuuppppppplllccccccc
abccaaaaaaaacccaaaaaaaaaaacccaaaahiiiqqqttttttuxxxxuuuvvpppplllccccccc
abcaaaaaaacccaaaaaaaaaaacccccaaaahhhqqqqtttttttxxxxuuvvvvvqqlllccccccc
abcccccaaaccaaaaaaaaaccccccccacaahhhqqqttttxxxxxxxyyyyyvvvqqlllccccccc
abcccccaaaccaaaaaaaacccccccccccaahhhqqqtttxxxxxxxyyyyyyvvqqqlllccccccc
SbcccccccccccaaaaaaaaaccccccccccchhhqqqtttxxxxEzzzyyyyvvvqqqmmlccccccc
abcccccccccccaaaaaaaacccaacccccccchhhppptttxxxxyyyyyvvvvqqqmmmcccccccc
abccccccccccaaaaaaaaaaccaacccccccchhhpppptttsxxyyyyyvvvqqqmmmccccccccc
abcaacccccccaaaaaaacaaaaaaccccccccchhhppppsswwyyyyyyyvvqqmmmmccccccccc
abaaaacccccccaccaaaccaaaaaaacccccccchhhpppsswwyywwyyyvvqqmmmddcccccccc
abaaaaccccccccccaaaccaaaaaaacccccccchhhpppsswwwwwwwwwvvqqqmmdddccccccc
abaaaacccccccccaaaccaaaaaaccccccccccgggpppsswwwwrrwwwwvrqqmmdddccccccc
abccccccaaaaaccaaaacaaaaaaccccccaacccggpppssswwsrrrwwwvrrqmmdddacccccc
abccccccaaaaaccaaaacccccaaccccaaaaaacggpppssssssrrrrrrrrrnmmdddaaccccc
abcccccaaaaaaccaaaccccccccccccaaaaaacggppossssssoorrrrrrrnnmdddacccccc
abcccccaaaaaaccccccccaaaaccccccaaaaacgggoooossoooonnnrrnnnnmddaaaacccc
abccccccaaaaaccccccccaaaacccccaaaaaccgggoooooooooonnnnnnnnndddaaaacccc
abccccccaaaccccccccccaaaacccccaaaaacccgggoooooooffennnnnnnedddaaaacccc
abcccccccccccccccccccaaacccccccaacccccggggffffffffeeeeeeeeeedaaacccccc
abccccccccccccccccccaaacccccaccaaccccccggfffffffffeeeeeeeeeecaaacccccc
abccccccccccccccccccaaaacccaaaaaaaaaccccfffffffaaaaaeeeeeecccccccccccc
abccccccccaacaaccccaaaaaacaaaaaaaaaaccccccccccaaaccaaaaccccccccccccccc
abccccccccaaaaacccaaaaaaaaaaacaaaaccccccccccccaaaccccaaccccccccccaaaca
abcccccccaaaaaccccaaaaaaaaaaacaaaaacccccccccccaaaccccccccccccccccaaaaa
abcccccccaaaaaacccaaaaaaaaaacaaaaaacccccccccccaaccccccccccccccccccaaaa
abcccccccccaaaaccaaaaaaaaaaaaaaccaaccccccccccccccccccccccccccccccaaaaa";

            var lines = input.Split('\n');
            int row = 0;
            Node start = null;
            Node target = start;

            var graph = new Dictionary<(int row, int col), Node>();
            foreach (var lineugly in lines)
            {
                var line = lineugly.TrimEnd('\r');

                for (int col = 0; col < line.Length; col++)
                {
                    char value = line[col];
                    if (char.IsLower(value))
                    {
                        var node = new Node { Value = value - 'a' };
                        graph.Add((row, col), node);
                    }
                    else if (value == 'E')
                    {
                        target = new Target { Value = 'z' - 'a' + 1 };
                        graph.Add((row, col), target);

                    }
                    else
                    {
                        start = new Node { Value = 999 };
                        graph.Add((row, col), start);
                    }
                }

                row++;
            }


            for (row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[0].Length - 1; col++)
                {
                    if (row - 1 >= 0 && graph[(row - 1, col)].Value - 1 <= graph[(row, col)].Value)
                    {
                        graph[(row, col)].Neighbours.Add(graph[(row - 1, col)]);
                    }

                    if (row + 1 < lines.Length && graph[(row + 1, col)].Value - 1 <= graph[(row, col)].Value)
                    {
                        graph[(row, col)].Neighbours.Add(graph[(row + 1, col)]);
                    }

                    if (col - 1 >= 0 && graph[(row, col - 1)].Value - 1 <= graph[(row, col)].Value)
                    {
                        graph[(row, col)].Neighbours.Add(graph[(row, col - 1)]);
                    }

                    if (col + 1 < lines[0].Length - 1 && graph[(row, col + 1)].Value - 1 <= graph[(row, col)].Value)
                    {
                        graph[(row, col)].Neighbours.Add(graph[(row, col + 1)]);
                    }
                }
            }

            int min = Dijkstra(start, graph)[target];
            foreach (var st in graph.Values.Where(n => n.Value == 0))
            {
                if (Dijkstra(st, graph).TryGetValue(target, out int res) && res < min) min = res;
            }
            Console.WriteLine(min);
        }

        private static Dictionary<Node, int> Dijkstra(Node start, Dictionary<(int row, int col), Node> graph)
        {
            var dists = new Dictionary<Node, int>
            {
                [start] = 0
            };

            var allNodes = new HashSet<Node>(graph.Values);
            var queue = new Queue<Node>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                allNodes.Remove(current);

                foreach (var neighbour in current.Neighbours)
                {
                    if (allNodes.Contains(neighbour))
                    {
                        int newDist = dists[current] + 1;
                        if (!dists.ContainsKey(neighbour) || dists[neighbour] > newDist)
                        {
                            queue.Enqueue(neighbour);
                            dists[neighbour] = newDist;
                        }
                    }
                }
            }

            return dists;
        }
    }

    class Node
    {
        public List<Node> Neighbours { get; } = new List<Node>();

        public int Value { get; set; }
    }

    class Target : Node
    {
        
    }
}