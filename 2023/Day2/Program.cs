var lines = File.ReadLines(@"input.txt");

var maxCubes = new Dictionary<string, int>
{
    { "red" , 12},
    { "green" , 13},
    { "blue" , 14}
};
var sum = 0;
var sum2 = 0;
foreach (var line in lines)
{
    var gameId = int.Parse(line.Split(":")[0].Split(" ")[1]);
    var draws = line.Replace(";", ",")
        .Split(":")[1]
        .Split(",")
        .Select(x => x.Split(" "))
        .Select(x => new { Color = x[2], Number = int.Parse(x[1]) });

    if (draws.All(x => x.Number <= maxCubes[x.Color]))
        sum += gameId;

    var power = draws.GroupBy(x => x.Color)
        .Select(x => x.Max(y => y.Number))
        .Aggregate((x, y) => x * y);

    sum2 += power;
}

Console.WriteLine(sum);
Console.WriteLine(sum2);
