var lines = File.ReadLines(@"input.txt").ToList();
var line = string.Join(",", lines);
var result = line.Split(",,")
    .Select(x => x.Split(",").Sum(y => int.Parse(y)))
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();
Console.WriteLine(result);