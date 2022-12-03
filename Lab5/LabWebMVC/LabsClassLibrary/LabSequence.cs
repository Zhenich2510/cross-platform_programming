using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsClassLibrary
{
    public static class LabSequence
    {
        static Dictionary<uint, ulong> hash = new Dictionary<uint, ulong>
        {
            [0] = 1,
            [1] = 2,
        };
        public static ulong CountCombines(uint N)
        {
            if (hash.TryGetValue(N, out ulong value)) return value;

            var result = CountCombines(N - 2) + CountCombines(N - 1);

            hash[N] = result;

            return result;
        }
    }
}
