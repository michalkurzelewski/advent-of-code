var lines = File.ReadLines(@"input.txt").ToList();
var top3 = string.Join(",", lines).Split(",,")
    .Select(x => x.Split(",").Sum(y => int.Parse(y)))
    .OrderByDescending(x => x)
    .Take(3);
Console.WriteLine(top3.First());
Console.WriteLine(top3.Sum());