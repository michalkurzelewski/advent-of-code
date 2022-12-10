var lines = File.ReadLines(@"C:\Projects\AdventOfCode\2022\Day10\input.txt").ToList();
var sum = 0;
var register = 1;
var cycle = 0;

for (int i = 0; i < lines.Count; i++)
{
    Console.Write(Math.Abs(register - (cycle % 40)) < 2 ? '#' : '.');
    cycle++;
    if (cycle % 40 == 0) Console.WriteLine();

    if (cycle % 40 == 20) 
        sum += cycle * register;

    var command = lines[i].Split(' ');
    if (command[0] == "addx")
    {
        Console.Write(Math.Abs(register - (cycle % 40)) < 2 ? '#' : '.');
        cycle++;
        if (cycle % 40 == 0) Console.WriteLine();
    }
    else
        continue;

    if (cycle % 40 == 20)
        sum += cycle * register;

    if (command[0] == "addx")
        register += int.Parse(command[1]);
}
Console.WriteLine();
Console.WriteLine(sum);