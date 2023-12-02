var task1 = File.ReadLines(@"input.txt");
var task2 = task1.Select(x => x
    .Replace("one", "on1e")
    .Replace("two", "t2wo")
    .Replace("three", "th3ree")
    .Replace("four", "fo4ur")
    .Replace("five", "fi5ve")
    .Replace("six", "si6x")
    .Replace("seven", "se7ven")
    .Replace("eight", "ei8ght")
    .Replace("nine", "ni9ne"));

static int Sum(IEnumerable<string> lines) => lines
    .Select(x => x.Where(char.IsDigit))
    .Select(x => int.Parse([x.First(), x.Last()]))
    .Sum();

Console.WriteLine(Sum(task1));
Console.WriteLine(Sum(task2));
