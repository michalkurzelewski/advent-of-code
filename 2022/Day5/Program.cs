using System.Text.RegularExpressions;

var lines = File.ReadLines(@"C:\Projects\AdventOfCode\2022\Day5\input.txt").ToList();

//Stack[] stacks = new[] {
//    new Stack("ZN"),
//    new Stack("MCD"),
//    new Stack("P")
//};

Stack[] stacks = new[] {
    new Stack("QFMRLWCV"),
    new Stack("DQL"),
    new Stack("PSRGWCNB"),
    new Stack("LCDHBQG"),
    new Stack("VGLFZS"),
    new Stack("DGNP"),
    new Stack("DZPVFCW"),
    new Stack("CPDMS"),
    new Stack("ZNWTVMPC"),
};

foreach (var line in lines)
{
    var numbers = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToArray();
    string move = "";
    for (int i = 0; i < numbers[0]; i++)
    {
        move += stacks[numbers[1] - 1].Pop();
    }
    for (int i = move.Length-1; i >=0; i--)
    {
        stacks[numbers[2] - 1].Push(move[i]);
    }
}

for (int i = 0; i < stacks.Length; i++)
    stacks[i].Peek();


class Stack
{
    static readonly int MAX = 1000;
    int top = -1;
    char[] stack = new char[MAX];

    public Stack(string start)
    {
        foreach (var character in start) Push(character);
    }
    public void Push(char data) => stack[++top] = data;
    public char Pop() => stack[top--];
    public void Peek() => Console.Write(stack[top]);
}
