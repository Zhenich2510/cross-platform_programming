namespace Lab1_61
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                WriteShuffledStr(streamWriter, File.ReadLines(InputFilePath).First().ToCharArray());
            }
        }

        public static void WriteShuffledStr(StreamWriter streamWriter, char[] list)
        {
            if (list.Length < 1 || list.Length > 8)
            {
                streamWriter.WriteLine("RangeError");
            }
            else
            {
                int x = list.Length - 1;
                WriteShuffledStr(streamWriter, list, 0, x);
            }
        }

        private static void WriteShuffledStr(StreamWriter streamWriter, char[] list, int k, int m)
        {
            if (k == m)
            {
                streamWriter.WriteLine(list);
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    WriteShuffledStr(streamWriter, list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;
            a ^= b;
            b ^= a;
            a ^= b;
        }
    }
}