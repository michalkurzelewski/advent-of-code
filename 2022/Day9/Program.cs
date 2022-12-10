var lines = File.ReadLines(@"input.txt").ToList();
var visited1 = new HashSet<string>();
var visited2 = new HashSet<string>();
var points = Enumerable.Repeat(new[] { 0, 0 }, 10).ToArray();

int[] MovePoint(int[] head, int[] tail)
{
    var distance = Math.Sqrt(Math.Pow(Math.Abs(head[0] - tail[0]), 2) + Math.Pow(Math.Abs(head[1] - tail[1]), 2));

    return distance < 2 ?
        tail :
        tail.Zip(head, (a, b) => Math.Abs(a - b) == 2 ? (a + b) / 2 : b).ToArray();
}

foreach (var line in lines)
{
    var command = line.Split(' ');
    for (int i = 0; i < int.Parse(command[1]); i++)
    {
        points[0] = points[0].Zip(command[0] switch
        {
            "R" => new[] { 1, 0 },
            "L" => new[] { -1, 0 },
            "U" => new[] { 0, 1 },
            "D" => new[] { 0, -1 },
        }, (x, y) => x + y).ToArray();

        for (int j = 0; j < 9; j++) 
            points[j + 1] = MovePoint(points[j], points[j + 1]);

        visited1.Add($"{points[1][0]} {points[1][1]}");
        visited2.Add($"{points[9][0]} {points[9][1]}");
    }
}

Console.WriteLine(visited1.Count);
Console.WriteLine(visited2.Count);