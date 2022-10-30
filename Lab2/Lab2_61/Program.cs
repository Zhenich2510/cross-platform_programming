namespace Lab2_61
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            var inputNumber = Convert.ToInt32(File.ReadLines(InputFilePath).First().Trim());
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                if (inputNumber < 1 || inputNumber > 1000)
                {
                    streamWriter.WriteLine("Out of range exception!");
                }
                else
                {
                    streamWriter.WriteLine(CalculateCountOfNumsNonconsecutiveOnes(inputNumber));
                }
            }
        }

        public static int CalculateCountOfNumsNonconsecutiveOnes(int n)
        {
            if (n == 1)
            {
                return 2;
            }
            if (n == 2)
            {
                return 3;
            }
            int n1 = 2, n2 = 3;
            for (int i = 3; i <= n; i++)
            {
                n2 += n1;
                n1 = n2 - n1;
            }
            return n2;
        }
    }
}