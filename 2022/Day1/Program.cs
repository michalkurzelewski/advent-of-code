var lines = File.ReadLines(@"C:\Projects\AdventOfCode\2022\Day1\input.txt").ToList();
var line = string.Join(",", lines);
var result = line.Split(",,")
    .Select(x => x.Split(",").Sum(y => int.Parse(y)))
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();
Console.WriteLine(result);