using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sinput = @"#.######
#>>.<^<#
#.<..<<#
#>v.><>#
#<^v^^>#
######.#";
            var input = @"#.########################################################################################################################
#<><v<^<vv<><^vv><><v^.^>.^.>.<v^>v><v^.v^<v<vvvv.^^><>^vv<<<>>v>>^>v>>>v<.vv>v<.<v>><>^<<v^<<v^vv>^^<v<^<>v>^v<^v^..^^<<#
#<>^<<<^<<vv.<^.^>>>vv.v<<vv^...vvv><><>^<^vv.<v>>v><<^^vvv>v>>^>^<v>^<vv><vv<v^v^.vv<<>vv.^^<>v<<v^>..>v^><v^v<<^^<^.^v<#
#>^>>^<v<v^<v.>vv^>vv^<vv.<>>^vv.<^^>v^vv>v<<vv<>><<^>.v^^v>v<<v^.^v>v>.<^^vvv><^v.v.<v>^>v<<^v.<vv>^v^v^^<>.<<<v^^^>^^^<#
#>>v>^><^v<.<>>>>^<v>>v^^^><<^v^<>^><>.v<.>^vv<^^<v>.<vv>v^>.v<v<vv.^><^v>><<<v<vvv.v^^>^.^>v^>^><>^<<^>v<<v^v>^v^>><>v.<#
#>>vv<^.^<>v<v<^<.>^v^.<>>.v^^^.vv^v.<v<>v.>>>v>>.<<<<v<.^>^v<v^<v^<..^v<v^>^<v<>v<vv>.><<<>^>>>v>>>v^<^v>v^<..v>>>v>^>^>#
#>^^v>.^>^<v>^^v>>v<^<<>^>^^>v<.v^<<><v^>>>><>.><>>>vv<^v^v.<v<v>v^>><v><<><>>^v<^^<^v<v^>v^<>.<.^v>vv.v><<v.>.^>.^^^.<.>#
#<<v>><.>v<<vv>v.vv<><vv<>^>>>^v<<v>^<<<<v^<>>vv^^v^>.<.^><>v>>v<^v<.v^<^<>v^v^v^>^v^<>v<.>v<^^^.<><v.^^..^v>^>^><.>v>v.>#
#<^v>v>^.>>>^vv^<>><>^.<>^^<<<^<><.v>^>.>.vvv><v<<>v^<^^<>^vv<vvv<v<<v<^<<>><v<>vv^v<.^.>^>v<<^<><><<<^<.>.v^^.<>><v^<^<<#
#>.<^<v^^><<.v^v<>v<^vv>>vv>^>^<.>>^v<vv.>.v<>>v<^^<><^^^v.<.v>>^.v^^^>>v^<>.<v^v^.^^v^vv.>>>^^v^..<^v..<.v<>v^<vv.^>^>v>#
#>v^^v>^<v>>vv^v^>.<><>v^^v.^^>.<<.>^<^<v^.^<><v.v<<^^^^<v^v>.^v^<.v^<<^^.><>.<v.v<^^>^^>>>^v^^..vv<v><>^>.><v^^..^^<.<..#
#<^<.v>>vv>v<<v<^<>>v^<v>.v^<>vvv..<^>>v>v^v^<^><^<^^v>v>.>vv>^^^>><.<<>^^>.>.>v><^<^v^^<vv<>>^^v<<v>.^.v.<v.^.^v<<^>^<>>#
#<.^v>^>.>^^>^>^<>>>.>^^^vv^v<>v.<^vv.vv^>>>^^v>^>v>>vvv^><v.<^><<>^^.v.vv>>vv>^vv<<<^..v<^><<<>>^<<v^<^<<.>^^<><<^^<vv^>#
#<^><>>>>^.^^v>v>^^.^<>><.<><v<<^>v><^^>><^^v>^>^^><^^>^v^<<><<.v^v>>>vvv^v>v^.^>.>v^vv>>>v^>^^.>>.>>vvv<^<<vvvv<^>><v^v<#
#<v^<<>^^v<^^<v^<^v><>..^<^>v^^<^<v>v.>>v>><><>v^>^v<vv^v^vv<.^^>>.<<^v>^^<>><<^>v^<^>v>^<>>>v>^.^<.^.^<v>vv.^v>v<vvv^^^<#
#<>v<v^^v^v<<<v^^><v<>v<^<>v^v<^^>^<v<v^.>^<vv^>^^<>v<>>>.v^v.<<<<.v^v>^<>><.v<>^.<>^.^vv<>.^<v.^v<^>>>^<^v^<.<>>v>^.>>^>#
#>>^>>vv.>v^v>>.vv>^<^v^>>^^<<<>v<>>><^.<^>^<>^.>>.<^^.^<.v<^vv>><<..<<.<v^>^^<>.>.<>><>>v^>.v.^^>^vv<<^^v>v^>vv>^<^v.v>>#
#<v.<v^^^<^><<^>v^><<v.^v^^>vv><vvv>^^^<v>^>vv<^^^>.>><<^<^v<>.v><^><>v<<.<^vv>.^^^<v>v>>>v<^.v^>>>^>^<<..v.>vv.>>v>^<^^>#
#<vvv>^^<<vv.v><^..>v.<v<v<<<v^>v<<>.><v<>.<vv^><^<<^v^..>v<v^v>>>^>><v.>>^<>v<^^.vv>>v>.v^^^><^<>>>^<<<<<...v><v>v><^<v<#
#>v^>v^<v>>><<^v>>>v^<^v.^.v^v.<^<^>.<<.<<.^^vv<v>v>><^^.><>v.>>..^.<vv><>.v>.^^>^vvv>.v>.<.^<v<>^><^v<>>>.<^^v<<>>>>^v>.#
#<>^>.v^<<><.v^<^<.>^>..<<vv^v<>>v>vv.<><<>^>><^^<v.<v<^<v^>vv^^<>vv>^<^^<v^.>^<^>v<v><^v>v^<^.v^<<>v>.>vv^^v^>vv>^.v><^<#
#<>>^.v><^v>^><>v><>.v>vv>^v^^v>.^..>^<^<vv^vv>v<<<v<<v<>.<.>>^^v<^<>v>^<^<v.>^<v.^<^^vvv>.<>^><^v.^>>v<v>^^.^^v>v>vv<<<>#
#.>>^^>v.v^>v^^>vv<vv<v.vv>.vv<<<<^^<v.<v><vv.v<^<^v<.>..^>v^<^^.<>v^<v..^^<^v^^>^<^.v>v<^^>vvvvv.>>>^vv^^^<>.v>v^..>v^.>#
#>^>v^.^vv^v.vv>v.>v^<><^<>v^<^v><.v<^v>^v>.v<v>^vvvvv>.<vv>>^^<^^^<.<>v>.<>>.v<^<v<^.v.<><>v^v^^^v^>^^.^^<><><<<v^^<<^v<#
#<>>v.<<v<^<^<^.vv>>>>>v^..<><<v^>^^.>>^<.<>^v^<^>v<.vv<v<<^vv^.<v<<v^vv<<.v<>^v^<^v<^vv>>><v>vvv><><><.><v>^><<.>><<>^^.#
#.<^v><>v>^v^v^><^^.v<<>^<^<>v..^>vv>vvvv<<vv>>>><v<^<^v^>v>^<>^^v.>>v>^.v^vvv<><v^.><<vv.>^.^v<>>v.<vv>^<^>>>v^>><v.>>><#
########################################################################################################################.#";
            var lines = input.Split('\n');

            (int column, int row) start = (lines[0].IndexOf('.') - 1, -1);
            (int column, int row) finish = (lines[lines.Length - 1].IndexOf('.') - 1, lines.Length - 2);

            List<string> upRows = new List<string>();
            List<string> downRows = new List<string>();
            List<string> leftCols = new List<string>();
            List<string> rightCols = new List<string>();
            for (int columnIndex = 1; columnIndex < lines[1].TrimEnd('\r').Length - 1; columnIndex++)
            {
                leftCols.Add("");
                rightCols.Add("");
            }
            for (int rowIndex = 1; rowIndex < lines.Length - 1; rowIndex++)
            {
                var line = lines[rowIndex].TrimEnd('\r');
                var ups = new StringBuilder(line.Length - 2);
                var downs = new StringBuilder(line.Length - 2);
                for (int columnIndex = 1; columnIndex < line.Length - 1; columnIndex++)
                {
                    ups.Append(line[columnIndex] == '^' ? 1 : 0);
                    downs.Append(line[columnIndex] == 'v' ? 1 : 0);
                    leftCols[columnIndex - 1] += line[columnIndex] == '<' ? 1 : 0;
                    rightCols[columnIndex - 1] += line[columnIndex] == '>' ? 1 : 0;
                }

                upRows.Add(ups.ToString());
                downRows.Add(downs.ToString());
            }

            var rowStates = new Dictionary<int, (List<string> up, List<string> down)>();
            var colStates = new Dictionary<int, (List<string> left, List<string> right)>();
            StepWinds(upRows, downRows, leftCols, rightCols, rowStates, colStates);
            
            
            var lcm = Lcm(lines.Length - 2, lines[1].TrimEnd('\r').Length - 2);
            var visitedStates = new Dictionary<int, List<((int, int), int)>>();

            Step(0, start, finish, new Dictionary<int, List<((int, int), int)>>(visitedStates), rowStates, colStates, lines.Length - 2,
                    lines[1].TrimEnd('\r').Length - 2);

            Console.WriteLine(Found);

            Step(Found, finish, start, new Dictionary<int, List<((int, int), int)>>(visitedStates), rowStates, colStates, lines.Length - 2,
                lines[1].TrimEnd('\r').Length - 2);

            Console.WriteLine(Found);

            Step(Found, start, finish, new Dictionary<int, List<((int, int), int)>>(visitedStates), rowStates, colStates, lines.Length - 2,
                lines[1].TrimEnd('\r').Length - 2);

            Console.WriteLine(Found);
        }

        private static int Limit = 1000;//9000;
        private static int Found = 0;

        private static void Step(int minute, (int column, int row) start, (int, int) finish,
            Dictionary<int, List<((int, int), int)>> visitedStates,
            Dictionary<int, (List<string> up, List<string> down)> rowStates,
            Dictionary<int, (List<string> left, List<string> right)> colStates, int rows, int cols)
        {
            var queue = new Stack<(int minute, (int column, int row) start)>();
            queue.Push((minute, start));
            int found = 0;
            while (queue.Count > 0)
            {
                (minute, start) = queue.Pop();

                if (minute > Limit || found !=0 && minute > found)
                    continue;

                int relativeMinute = minute % Lcm(rows, cols);
                if (visitedStates.ContainsKey(relativeMinute))
                {
                    if (visitedStates[relativeMinute].Any(x => x.Item1 == start && x.Item2 <= minute))
                        continue;

                    visitedStates[relativeMinute].Add((start, minute));
                }
                else
                {
                    visitedStates.Add(relativeMinute, new List<((int, int), int)> { (start, minute) });
                }

                if (start == finish)
                {
                    Found = minute;
                    found = minute;
                    //Console.WriteLine(minute);
                    if (minute < found) 
                        found = minute;
                    continue;
                }

                minute++;


                var down = (start.column, start.row + 1);
                if (down == finish || CheckFieldAvailable(minute, down, rowStates, colStates, rows, cols))
                    queue.Push((minute, down));

                var right = (start.column + 1, start.row);
                if (CheckFieldAvailable(minute, right, rowStates, colStates, rows, cols))
                    queue.Push((minute, right));

                var up = (start.column, start.row - 1);
                if (up == (0, -1) || CheckFieldAvailable(minute, up, rowStates, colStates, rows, cols))
                    queue.Push((minute, up));

                var left = (start.column - 1, start.row);
                if (CheckFieldAvailable(minute, left, rowStates, colStates, rows, cols))
                    queue.Push((minute, left));

                //stay
                if (start.row == -1 || start.row == rows || CheckFieldAvailable(minute, start, rowStates, colStates, rows, cols))
                    queue.Push((minute, start));
            }
        }

        private static bool CheckFieldAvailable(int minute, (int column, int row) start, Dictionary<int, (List<string> up, List<string> down)> rowStates, Dictionary<int, (List<string> left, List<string> right)> colStates, int rows, int cols)
        {
            return start.row >= 0 && start.row < rows && start.column >= 0 && start.column < cols &&
                   rowStates[minute % rows].down[start.row][start.column] == '0' &&
                   rowStates[minute % rows].up[start.row][start.column] == '0' &&
                   colStates[minute % cols].left[start.column][start.row] == '0' &&
                   colStates[minute % cols].right[start.column][start.row] == '0';
        }

        private static void StepWinds(List<string> upRows, List<string> downRows, List<string> leftCols, List<string> rightCols, Dictionary<int, (List<string> up, List<string> down)> rowStates, Dictionary<int, (List<string> left, List<string> right)> colStates)
        {
            rowStates.Add(0, (upRows, downRows));
            for (int i = 1; i < upRows.Count; i++)
            {
                upRows = new List<string>(rowStates[i - 1].up);
                downRows = new List<string>(rowStates[i - 1].down);
                StepRows(upRows, downRows);


                rowStates.Add(i, (upRows, downRows));
            }

            colStates.Add(0, (leftCols, rightCols));
            for (int i = 1; i < leftCols.Count; i++)
            {
                leftCols = new List<string>(colStates[i - 1].left);
                rightCols = new List<string>(colStates[i - 1].right);

                StepCols(leftCols, rightCols);
                colStates.Add(i, (leftCols, rightCols));
            }
        }

        private static void StepCols(List<string> leftCols, List<string> rightCols)
        {
            var firstLeft = leftCols[0];
            leftCols.RemoveAt(0);
            leftCols.Add(firstLeft);

            var lastRight = rightCols[rightCols.Count - 1];
            rightCols.RemoveAt(rightCols.Count - 1);
            rightCols.Insert(0, lastRight);
        }

        private static void StepRows(List<string> upRows, List<string> downRows)
        {
            var firstUp = upRows[0];
            upRows.RemoveAt(0);
            upRows.Add(firstUp);

            var lastDown = downRows[downRows.Count - 1];
            downRows.RemoveAt(downRows.Count - 1);
            downRows.Insert(0, lastDown);
        }

        public static int Lcm(int number1, int number2)
        {
            if (number1 == 0 || number2 == 0)
            {
                return 0;
            }
            int absNumber1 = Math.Abs(number1);
            int absNumber2 = Math.Abs(number2);
            int absHigherNumber = Math.Max(absNumber1, absNumber2);
            int absLowerNumber = Math.Min(absNumber1, absNumber2);
            int lcm = absHigherNumber;
            while (lcm % absLowerNumber != 0)
            {
                lcm += absHigherNumber;
            }
            return lcm;
        }
    }
}
