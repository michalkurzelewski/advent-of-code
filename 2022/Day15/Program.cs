using System.Text.RegularExpressions;

var lines = File.ReadLines(@"input.txt").ToList();
var sensors = lines.Select(line =>
{
    var numbers = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToArray();
    var distance = Math.Abs(numbers[2] - numbers[0]) + Math.Abs(numbers[3] - numbers[1]);

    var x = numbers[0];
    var y = numbers[1];

    return new { x, y, distance };
}).ToList();


for (int i = 0; i < 4000000; i++)
{
    var sections = sensors.Select(sensor => new { add = sensor.distance - Math.Abs(i - sensor.y), sensor })
        .Where(x => x.add >= 0)
        .Select(item => new int[] { item.sensor.x - item.add, item.sensor.x + item.add })
        .ToList();
    sections = sections.OrderBy(x => x[0]).ToList();
    var filteredsections = new List<int[]> { sections[0] };
    for (int j = 1; j < sections.Count; j++)
    {
        var last = filteredsections.Last();
        var s = sections[j];
        if (last[1] >= s[0])
        {
            if (s[1] > last[1])
                last[1] = s[1];
        }
        else
            filteredsections.Add(sections[j]);
    }
    if (i == 2000000)
        Console.WriteLine(filteredsections[0][1] - filteredsections[0][0]);
    if (filteredsections.Count > 1)
        Console.WriteLine($"{filteredsections[0][1] + 1} * 4000000 + {i} = {(((long)filteredsections[0][1] + 1) * 4000000) + i}");
}
