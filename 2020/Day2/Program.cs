var lines = File.ReadLines(@"input.txt").ToList();
char[] split = [':', ' ', '-'];

var entries = lines.Select(x => x.Split(split));

var part1 = 0;
var part2 = 0;
foreach (var entry in entries)
{
    var from = int.Parse(entry[0]);
    var to = int.Parse(entry[1]);
    var character = entry[2][0];
    var password = entry[4];

    var count = password.Count(x => x == character);
    if (from <= count && count <= to) part1++;

    if ((password[from - 1] != password[to - 1]) &&
        (password[from - 1] == character || password[to - 1] == character)) part2++;
}

Console.WriteLine(part1);
Console.WriteLine(part2);
