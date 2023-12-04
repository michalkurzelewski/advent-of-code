var lines = File.ReadLines(@"input.txt").Select(x => x.Replace("  ", " ")).ToList();

var sum = 0;
var cards = Enumerable.Repeat(1, lines.Count).ToArray();
for (int i = 0; i < lines.Count; i++)
{
    var left = lines[i].Split(": ")[1].Split(" | ")[0].Split(" ").Select(int.Parse);
    var right = lines[i].Split(": ")[1].Split(" | ")[1].Split(" ").Select(int.Parse).ToHashSet();

    var score = left.Where(right.Contains).Count();
    sum += (int)Math.Pow(2, score - 1);

    for (int j = 1; j <= score; j++)
        cards[i + j] += cards[i];
}

Console.WriteLine(sum);
Console.WriteLine(cards.Sum());
