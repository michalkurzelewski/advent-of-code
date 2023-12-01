var task1 = File.ReadLines(@"input.txt").ToList();
var task2 = task1.Select(x => x.Replace("one", "one1one")
    .Replace("two", "t2wo")
    .Replace("three", "th3ree")
    .Replace("four", "fo4ur")
    .Replace("five", "fi5ve")
    .Replace("six", "si6x")
    .Replace("seven", "se7ven")
    .Replace("eight", "ei8ght")
    .Replace("nine", "ni9ne"))
    .ToList();

static int Sum(List<string> lines) => lines
    .Select(x => x.Where(char.IsDigit).ToArray())
    .Select(x => int.Parse([x[0], x[^1]]))
    .Sum();

Console.WriteLine(Sum(task1));
Console.WriteLine(Sum(task2));
