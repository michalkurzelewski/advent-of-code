var task1 = File.ReadLines(@"input.txt").ToList();
var task2 = task1.Select(x => x.Replace("one", "one1one")
    .Replace("two", "two2two")
    .Replace("three", "three3three")
    .Replace("four", "four4four")
    .Replace("five", "five5five")
    .Replace("six", "six6six")
    .Replace("seven", "seven7seven")
    .Replace("eight", "eight8eight")
    .Replace("nine", "nine9nine"))
    .ToList();

static int Sum(List<string> lines) => lines
    .Select(x => x.Where(char.IsDigit).ToArray())
    .Select(x => int.Parse([x[0], x[^1]]))
    .Sum();

Console.WriteLine(Sum(task1));
Console.WriteLine(Sum(task2));
