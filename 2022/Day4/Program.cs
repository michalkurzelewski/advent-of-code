var lines = File.ReadLines(@"input.txt").ToList();
var sum = 0;
var sum2 = 0;
foreach (var line in lines)
{
    var numbers = line.Split(new char[] { ',', '-' }).Select(x => int.Parse(x)).ToArray();
    var left = Enumerable.Range(numbers[0], numbers[1] - numbers[0] + 1);
    var right = Enumerable.Range(numbers[2], numbers[3] - numbers[2] + 1);

    if (left.All(x => right.Contains(x)) || right.All(x => left.Contains(x)))
        sum++;
    if (left.Any(x => right.Contains(x)))
        sum2++;
}

Console.WriteLine(sum);
Console.WriteLine(sum2);