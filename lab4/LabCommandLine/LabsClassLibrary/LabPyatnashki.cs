using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsClassLibrary
{
    public static class LabPyatnashki
    {

        public static int ComputeStepsCount(string start, string finish)
        {
            Dictionary<string, int> lenght = new();
            lenght[start] = 0;
            Queue<string> queue = new();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                if (current == finish) //ACM8002# //ACM#2008
                { 
                    return lenght[current];
                }
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i * i + j * j == 1)
                        {
                            string next = current.Shift(i, j);

                            if (!lenght.ContainsKey(next))
                            {
                                lenght[next] = lenght[current] + 1;
                                queue.Enqueue(next);
                            }
                        }
                    }
                }
            }
            return -1;
        }
    }
    static class Ext
    {
        public static string Shift(this string s, int i, int j) //ACM8 002# //ACM#2008
        {
            //var sub1 = s.Substring(0, 4).ToCharArray();
            //var sub2 = s.Substring(4, 4).ToCharArray();

            char[][] state = new char[][] {
            s[..4].ToCharArray(),
            s[4..].ToCharArray()
        };
            int pos = s.IndexOf("#");
            int posi = pos / 4;
            int posj = pos % 4;
            int nexti = posi + i;
            int nextj = posj + j;
            if (0 <= nexti && nexti < 2 && 0 <= nextj && nextj < 4)
            {
                var temp = state[posi][posj];
                state[posi][posj] = state[nexti][nextj];
                state[nexti][nextj] = temp;
            }

            return new string(state[0]) + new string(state[1]);
        }
    }
}
