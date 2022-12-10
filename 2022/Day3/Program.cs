var lines = File.ReadLines(@"input.txt").ToList();
var result = lines.Sum(line => FindPriority(line));
Console.WriteLine(result);

int FindPriority(string line)
{
    var a = line.Substring(0, line.Length / 2);
    var b = line.Substring(line.Length / 2, line.Length / 2);

    foreach (var character in a)
    {
        if (b.Contains(character)) return ConvertToPriority(character);
    }
    return 0;
}

var linqResult1 = lines.Select(line =>
    {
        var character = line.First(x => line.Substring(line.Length / 2, line.Length / 2).Any(y => y == x));
        return ConvertToPriority(character);
    }).Sum();
Console.WriteLine(linqResult1);

int ConvertToPriority(char a)
{
    if (a >= 'a') return a - 'a' + 1;
    else return a - 'A' + 27;
}

int sum2 = 0;
for (int i = 0; i < lines.Count(); i = i + 3)
{
    foreach (var character in lines[i])
    {
        if (lines[i + 1].Any(x => x == character) && lines[i + 2].Any(x => x == character))
        {
            sum2 += ConvertToPriority(character);
            break;
        }
    }
}
Console.WriteLine(sum2);

var linqResult2 = lines.Where((x, i) => i % 3 == 0)
    .Select((line, i) =>
    {
        var character = line.First(x =>
                lines[i * 3 + 1].Any(y => y == x)
             && lines[i * 3 + 2].Any(y => y == x));
        return ConvertToPriority(character);
    }).Sum();
Console.WriteLine(linqResult2);