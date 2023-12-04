using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sinput = @"1=-0-2
12111
2=0=
21
2=01
111
20012
112
1=-1=
1-12
12
1=
122";
            var input = @"1=021=20---11-1=1
222==-=1-2=-11-11
1010==0=0=1--=1
2
1122=
2222==2222-2--10
100=-2-002=
12221-2-10-=1=
1-=211000=221-1
2=--12==11221-2-002
1101==-01===01=
220=0=-2-1-0===10=2
2-0-
1=-12=2==
1-=-02==-1
100-0-11--02--0=-
1==0-1220011=-
21
1102-22==
222
1==0=22-0--011-0
1020
2==2-102
1010-02==02=
1=1
2=010==12022=
1=-22001-2=
101=2-201122111
1=-0021211-1-1-1202
2-22-2
1212002==1
2=-0-
1-1==1122110100
1--020-2-=-0-21--
2=22-21220022==1=
1=-00-121-20-2122
1-==-00
20=21=0
20-02-0-=-==-=22
1120==-2
2=--0-0-000201=101
1---=02=21
1-011
211-2112=20--2
1=-
1=--=021
1-1-122200==1-1111
11001-
200=200
1===21=1-2
1===10
2=22
10-110210=01112-
2121=1=2=-201-
1---2=-0=-
211-120---000=-0-
1-
1-101-2=01020=-0=
112-12--0=12=12--
1-0-=2--=--12=20
1=0=-121
1000122-210--011200
120==-1-=01
1=2=1--
101-1=--2-
2=
2--10
2201020-=2-
1===2001-1=0=
222=-2---2=112
2010=-22=012=202=
111
1-2112=0200=1
2=02211=2
2-121
1----12=-2011
2==0
112
22=0==1-1=
1-20=0-
1=110211-022201222
20
1-111
1=-=022011=2=2=1
1-02122
2-1
1-2-=2-0=0---
1-20
1=1=102
1=1=0--1
11222-==0101-
2122100=0
2011==12-10
210--121-0=
22121=01=0=-=-=-==
10-0-=1-12
2-2=21
1=1-0-0-
10020
10-0-0120=211-2-
1-==10-0-==001201-
1-0
21=00202002-0
1-1=2021
11=1==1=0
1=102-1=-20-0
2=100-12-0=
1=0
1--=
12=212110200-
222002100";
            var lines = input.Split('\n');

            var decimalPlaces = new Dictionary<int, char>();
            foreach (var lineugly in lines)
            {
                var line = lineugly.TrimEnd('\r');
                var places = line.Reverse();

                int multiplier = 1;

                int carry = 0;
                foreach (char place in places)
                {
                    carry = Add(decimalPlaces, multiplier, carry, place);
                    multiplier++;
                }

                while (carry != 0)
                {
                    carry = Add(decimalPlaces, multiplier, carry, '0');
                    multiplier++;
                }

            }

            for (int i = decimalPlaces.Keys.Max(); i > 0; i--)
                Console.Write(decimalPlaces[i]);

            Console.WriteLine();
        }

        private static int Add(Dictionary<int, char> decimalPlaces, int multiplier, int carry, char place)
        {
            if (!decimalPlaces.ContainsKey(multiplier))
                decimalPlaces.Add(multiplier, '0');

            int current = Decode(decimalPlaces[multiplier]);
            current += carry;
            carry = 0;

            int now = Decode(place);
            current += now;
            while (current < -2)
            {
                carry--;
                current += 5;
            }
            while (current > 2)
            {
                carry++;
                current -= 5;
            }

            decimalPlaces[multiplier] = Encode(current);
            return carry;
        }

        private static char Encode(int current)
        {
            switch (current)
            {
                case -2:
                    return '=';
                case -1:
                    return '-';
                case 0:
                    return '0';
                case 1:
                    return '1';
                case 2:
                    return '2';
            }

            return '0';
        }

        private static int Decode(char place)
        {
            switch (place)
            {
                case '=':
                    return -2;
                case '-':
                    return -1;
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
            }

            return 0;
        }
    }
}
