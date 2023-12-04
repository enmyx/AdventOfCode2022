using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sinput = @"2,18:-2,15
9,16:10,16
13,2:15,3
12,14:10,16
10,20:10,16
14,17:10,16
8,7:2,10
2,0:2,10
0,11:2,10
20,14:25,17
17,20:21,22
16,7:15,3
14,3:15,3
20,1:15,3";
            var input = @"3999724,2000469:4281123,2282046
3995530,8733:3321979,-692911
3016889,2550239:2408038,2645605
3443945,3604888:3610223,3768674
168575,491461:1053731,-142061
2820722,3865596:3191440,3801895
2329102,2456329:2408038,2645605
3889469,3781572:3610223,3768674
3256726,3882107:3191440,3801895
3729564,3214899:3610223,3768674
206718,2732608:-152842,3117903
2178192,2132103:2175035,2000000
1884402,214904:1053731,-142061
3060435,980430:2175035,2000000
3998355,3965954:3610223,3768674
3704399,3973731:3610223,3768674
1421672,3446889:2408038,2645605
3415633,3916020:3191440,3801895
2408019,2263990:2408038,2645605
3735247,2533767:4281123,2282046
1756494,1928662:2175035,2000000
780161,1907142:2175035,2000000
3036853,3294727:3191440,3801895
53246,3908582:-152842,3117903
2110517,2243287:2175035,2000000
3149491,3998374:3191440,3801895";

            var lines = input.Split('\n');

            (int x, int y) min = (int.MaxValue, int.MaxValue);
            (int x, int y) max = (int.MinValue, int.MinValue);

            var sensors = new Dictionary<(int x, int y), (int x, int y)>();
            foreach (var lineugly in lines)
            {
                var line = lineugly.TrimEnd('\r');
                var pairs = line.Split(':');
                var sensorCoord = pairs[0].Split(',');
                var beaconCoord = pairs[1].Split(',');

                var sensor = (int.Parse(sensorCoord[0]), int.Parse(sensorCoord[1]));
                var beacon = (int.Parse(beaconCoord[0]), int.Parse(beaconCoord[1]));

                var curMinx = Math.Min(sensor.Item1, beacon.Item1);
                var curMaxx = Math.Max(sensor.Item1, beacon.Item1);
                var curMiny = Math.Min(sensor.Item2, beacon.Item2);
                var curMaxy = Math.Max(sensor.Item2, beacon.Item2);

                if (curMinx < min.x) min.x = curMinx;
                if (curMaxx > max.x) max.x = curMaxx;
                if (curMiny < min.y) min.y = curMiny;
                if (curMaxy > max.y) max.y = curMaxy;

                sensors.Add(sensor, beacon);
            }

            int MAX = 4000000;
            var map = new Dictionary<int, (int min, int max)>();




            var dists = new Dictionary<KeyValuePair<(int x, int y), (int x, int y)>, int>();
            foreach (var sensor in sensors)
            {
                var distance = Math.Abs(sensor.Key.x - sensor.Value.x) +
                               Math.Abs(sensor.Key.y - sensor.Value.y);
                dists.Add(sensor, distance);

                int xxx = 3337614;
                int yyy = 2933732;
                if (Math.Abs(sensor.Key.x - xxx) + Math.Abs(sensor.Key.y - yyy) <= distance)
                {
                    Console.WriteLine("OHNO");
                }
            }

            Parallel.For(0, MAX + 1, y =>
            {
                //for (int y = 2933732 * 0; y <= MAX; y++)
                {
                    if (y % 100000 == 0) Console.WriteLine(y);

                    var starts = new List<(int, int)>();
                    foreach (var sensor in sensors)
                    {
                        var distance = dists[sensor];
                        if (y >= sensor.Key.y - distance && y <= sensor.Key.y + distance)
                        {
                            int left = Math.Max(0, sensor.Key.x - distance + Math.Abs(sensor.Key.y - y));
                            int right = Math.Min(MAX, sensor.Key.x + distance - Math.Abs(sensor.Key.y - y));

                            var me = (left, right);

                            starts.Add(me);
                        }
                    }

                    starts = starts.OrderBy(x => x.Item1).ThenByDescending(x => x.Item2).Distinct().ToList();

                    var filtered = new List<(int, int)>();
                    filtered.Add(starts[0]);
                    int prev = 1;
                    for (int i = 1; i < starts.Count; i++)
                    {
                        if (starts[i].Item1 == starts[i - prev].Item1)
                        {
                            prev++;
                            continue;

                        }

                        if (starts[i].Item1 >= starts[i - prev].Item1 && starts[i].Item2 <= starts[i - prev].Item2)
                        {
                            prev++;
                            continue;
                        }

                        prev = 1;
                        filtered.Add(starts[i]);
                    }

                    for (int i = 1; i < filtered.Count; i++)
                    {
                        if (filtered[i].Item1 > filtered[i - 1].Item2 + 1)
                        {
                            Console.WriteLine($"x: {(filtered[i].Item1 - 1)} y: {y}");
                            Console.WriteLine((long)y + (4000000 * (long)(filtered[i].Item1 - 1)));
                            return;
                        }
                    }

                    ;
                }
            });

        }
    }
}
