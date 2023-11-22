var numbers = File.ReadLines(@"input.txt").Select(int.Parse).ToArray();

//part1
foreach (var first in numbers)
{
    var second = numbers.FirstOrDefault(x => first + x == 2020);
    if (second != default) Console.WriteLine(first * second);
}

//part2
foreach (var first in numbers)
{
    foreach (var second in numbers)
    {
        var third = numbers.FirstOrDefault(x => first + second + x == 2020);
        if (third != default) Console.WriteLine(first * second * third);
    }
}
