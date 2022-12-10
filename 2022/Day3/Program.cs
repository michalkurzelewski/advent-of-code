var lines = File.ReadLines(@"input.txt").ToList();

int ConvertToPriority(char a)
{
    if (a >= 'a') return a - 'a' + 1;
    else return a - 'A' + 27;
}

var sum1 = lines.Sum(line => 
{
    var halfs = line.Insert(line.Length / 2, "|").Split('|');

    foreach (var c in halfs[0])
        if (halfs[1].Contains(c)) return ConvertToPriority(c);
    return 0;
});
Console.WriteLine(sum1);


var linqSum1 = lines.Select(line =>
    {
        var c = line.First(x => line.Substring(line.Length / 2, line.Length / 2).Any(y => y == x));
        return ConvertToPriority(c);
    }).Sum();
Console.WriteLine(linqSum1);


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


var linqSum2 = lines.Where((x, i) => i % 3 == 0)
    .Select((line, i) =>
    {
        var character = line.First(x =>
                lines[i * 3 + 1].Any(y => y == x)
             && lines[i * 3 + 2].Any(y => y == x));
        return ConvertToPriority(character);
    }).Sum();
Console.WriteLine(linqSum2);