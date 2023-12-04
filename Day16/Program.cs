using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sinput = @"AA=0;DD,II,BB
BB=13;CC,AA
CC=2;DD,BB
DD=20;CC,AA,EE
EE=3;FF,DD
FF=0;EE,GG
GG=0;FF,HH
HH=22;GG
II=0;AA,JJ
JJ=21;II";
            var input = @"BT=0;EZ,TO
OJ=0;UV,QG
SQ=0;IR,KE
JT=9;ES,RL,BL,BN
PH=20;IL,CA,RL,QD
YI=0;IL,IF
BU=0;IR,BL
IR=13;HV,CA,BU,MA,SQ
SV=16;NL,MA,XQ
JG=0;JL,AA
NL=0;SV,QD
FS=0;KE,XQ
UV=19;YB,OJ,YQ,CX
MA=0;SV,IR
YB=0;UV,TG
YQ=0;UV,RN
EZ=8;BT,EU,VJ,PJ,HX
EU=0;EZ,FC
KE=21;ES,SQ,FS
YW=0;QG,PJ
PJ=0;YW,EZ
OY=11;HX,JL,ZH,EH,JU
CX=0;NT,UV
HV=0;MO,IR
EH=0;OY,GS
NN=23;RM
CP=0;FU,ZH
FU=14;BF,CP
BF=0;RM,FU
RN=0;AA,YQ
BN=0;TO,JT
HX=0;EZ,OY
JL=0;JG,OY
KS=0;TO,AA
JU=0;JJ,OY
PA=0;QG,NH
JJ=0;QG,JU
IL=0;YI,PH
ES=0;KE,JT
TX=0;TO,QG
GS=0;PT,EH
QD=0;NL,PH
TG=0;YB,FC
BL=0;BU,JT
ZH=0;OY,CP
PT=18;GS
CA=0;PH,IR
IF=25;MO,YI
NH=0;PA,AA
TO=3;BT,NT,TX,BN,KS
RL=0;PH,JT
FC=22;TG,EU
AA=0;RN,NH,VJ,JG,KS
XQ=0;SV,FS
QG=5;TX,JJ,PA,YW,OJ
VJ=0;EZ,AA
RM=0;NN,BF
NT=0;TO,CX
MO=0;IF,HV";

            var lines = input.Split('\n');
            var graph = new Dictionary<string, Node>();
            var ids = new Dictionary<Node, int>();
            var byids = new Dictionary<int, Node>();
            int id = 0;
            foreach (var lineugly in lines)
            {
                var line = lineugly.TrimEnd('\r');
                var pairs = line.Split(';');
                var nodeData = pairs[0].Split('=');
                if (!graph.TryGetValue(nodeData[0], out Node node))
                {
                    node = new Node { Name = nodeData[0] };
                    graph.Add(nodeData[0], node);
                    ids.Add(node, id);
                    byids.Add(id, node);
                    id++;
                }

                node.Pressure = int.Parse(nodeData[1]);

                foreach (var edge in pairs[1].Split(','))
                {
                    if (!graph.TryGetValue(edge, out Node neighbor))
                    {
                        neighbor = new Node { Name = edge };
                        graph.Add(edge, neighbor);
                        ids.Add(neighbor, id);
                        byids.Add(id, neighbor);
                        id++;
                    }
                    node.Neighbors.Add(neighbor, 1);
                }
            }

            ReduceGraph(ids);
            PrintGraph(ids);

            startNode = graph["AA"];
            //var max = Step(startNode, new HashSet<Node>(), MAX, 2, ids);
            //Console.WriteLine(max);

            var res = CCalculateWeights(graph, ids[startNode], 30, ids, byids);
        }

        private static int MAX = 26;
        private static Node startNode;


        private static void PrintGraph(Dictionary<Node, int> ids)
        {
            foreach (var node in ids.Keys.Where(n => n.Neighbors.Count > 0).ToList())
            {
                Console.WriteLine($"{node.Name} {node.Pressure}");
                foreach (var neighbor in node.Neighbors)
                {
                    Console.WriteLine($"    {neighbor.Key.Name} {neighbor.Value}");
                }
            }
        }

        private static void ReduceGraph(Dictionary<Node, int> ids)
        {
            foreach (var node in ids.Keys.ToList())
            {
                var nodeNeighbors = node.Neighbors;
                var removed = new List<Node>();
                while (nodeNeighbors.Keys.Any(IsZeroValuedNode))
                {
                    foreach (var neighbor in nodeNeighbors.Where(n => IsZeroValuedNode(n.Key)).ToList())
                    {
                        foreach (var neighbor2 in neighbor.Key.Neighbors.Where(n => !removed.Contains(n.Key)).ToList())
                        {
                            if (nodeNeighbors.ContainsKey(neighbor2.Key))
                            {
                                if (nodeNeighbors[neighbor2.Key] > neighbor2.Value + neighbor.Value)
                                    nodeNeighbors[neighbor2.Key] = neighbor2.Value + neighbor.Value;
                            }
                            else if (neighbor2.Key != node)
                            {
                                nodeNeighbors.Add(neighbor2.Key, neighbor2.Value + neighbor.Value);
                            }
                        }

                        nodeNeighbors.Remove(neighbor.Key);
                        removed.Add(neighbor.Key);
                    }
                }
            }

            foreach (var node in ids.Keys.Where(IsZeroValuedNode))
            {
                node.Neighbors.Clear();
            }
        }

        private static bool IsZeroValuedNode(Node n)
        {
            return n.Pressure == 0 && n.Name != "AA";
        }

        private static Dictionary<string, int> values = new Dictionary<string, int>();
        private static int Step(Node current, HashSet<Node> opened, int max, int rounds,
            Dictionary<Node, int> ids)
        {
            if (max <= 0)
                return rounds > 1 ? Step(startNode, opened, MAX, rounds - 1, ids) : 0;

            var sb = new StringBuilder($"{current.Name}{max}_{rounds}_");
            foreach (var nodeName in opened.Select(o => ids[o]).OrderBy(n => n))
            {
                sb.Append(nodeName);
                sb.Append('_');
            }
            string key = sb.ToString();
            if (values.ContainsKey(key))
            {
                return values[key];
            }

            int value = 0;
            if (!opened.Contains(current) && current.Pressure > 0)
            {
                var openedAfterStep = new HashSet<Node>(opened) { current };
                value = Math.Max(value, (max - 1) * current.Pressure + Step(current, openedAfterStep, max - 1, rounds, ids));
            }

            foreach (var neighbor in current.Neighbors.Where(n => n.Key != current))
            {
                value = Math.Max(value, Step(neighbor.Key, opened, max - neighbor.Value, rounds, ids));
            }

            values.Add(key, value);
            return value;
        }

        private static int[,] CalculateWeights(Dictionary<string, Node> graph, int startId, int MAX, Dictionary<Node, int> ids, Dictionary<int, Node> byids)
        {
            var weights = new int[graph.Count, MAX + 1];
            var opened = new Dictionary<int, int>[graph.Count, MAX + 1];
            for (int i = 0; i <= MAX; i++)
            {
                for (int n = 0; n < graph.Count; n++)
                {
                    if (i == 0 && n == startId)
                        opened[n, 0] = new Dictionary<int, int>();

                    else
                        weights[n, i] = int.MinValue;
                }
            }

            for (int i = 0; i < MAX; i++)
            {
                for (int n = 0; n < graph.Count; n++)
                {
                    if (weights[n, i] < 0)
                        continue;

                    if (!opened[n, i].ContainsKey(n))
                    {
                        var weight = weights[n, i] + (MAX - (i + 1)) * byids[n].Pressure;
                        if (weights[n, i + 1] < weight)
                        {
                            // OPEN VALVE
                            weights[n, i + 1] = weight;
                            opened[n, i + 1] = new Dictionary<int, int>(opened[n, i]) { { n, (MAX - (i + 1)) } };
                        }
                    }

                    foreach (var neighbor in byids[n].Neighbors)
                    {
                        if (weights[n, i] >= 0 && i + neighbor.Value <= MAX && weights[ids[neighbor.Key], i + neighbor.Value] < weights[n, i])
                        {
                            // STEP
                            weights[ids[neighbor.Key], i + neighbor.Value] = weights[n, i];
                            opened[ids[neighbor.Key], i + neighbor.Value] = new Dictionary<int, int>(opened[n, i]);
                        }
                    }
                }
            }

            return weights;
        }
    }

    class Node
    {
        public int Pressure { get; set; }

        public string Name { get; set; }

        public Dictionary<Node, int> Neighbors { get; } = new Dictionary<Node, int>();
    }
}
