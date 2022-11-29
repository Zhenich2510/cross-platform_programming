using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsClassLibrary
{
    public static class LabSequence
    {
        static Dictionary<uint, long> hash = new Dictionary<uint, long>
        {
            [0] = 1,
            [1] = 2,
        };
        public static long CountCombines(uint N)
        {
            if (hash.TryGetValue(N, out long value)) return value;

            var result = CountCombines(N - 2) + CountCombines(N - 1);

            hash[N] = result;

            return result;
        }
    }
}
