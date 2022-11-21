// See https://aka.ms/new-console-template for more information

var lines = File.ReadLines("INPUT.txt").Take(4).ToArray();

string s1 = lines[0];
string s2 = lines[1];
string s3 = lines[2];
string s4 = lines[3];

Console.WriteLine("Input string: ");
Console.WriteLine(s1);
Console.WriteLine(s2);
Console.WriteLine(s3);
Console.WriteLine(s4);

string start = s1 + s2;
string finish = s3 + s4;

Queue<string> queue = new();
queue.Enqueue(start);

Dictionary<string, int> lenght = new()
{
    { start, 0 }
};

while (queue.Count > 0)
{
    string current = queue.Dequeue();

    if (current == finish) //ACM8002# //ACM#2008
    {
        Console.WriteLine($"Result: {lenght[current]}");
        File.WriteAllText("OUTPUT.txt", $"{lenght[current]}");

        return;
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
File.WriteAllText("OUTPUT.txt", "No way");
Console.WriteLine($"Result: No way");

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