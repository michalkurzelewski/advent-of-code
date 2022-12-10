var lines = File.ReadLines(@"C:\Projects\AdventOfCode\2022\Day8\input.txt").ToList();

var sum = lines.Count() * 4 - 4;
int sum2 = 0;
for (int i = 1; i < lines.Count() - 1; i++)
{
    for (int j = 1; j < lines.Count() - 1; j++)
    {
        var leftArray = lines[i].Where((c, ix) => ix < j);
        var left = leftArray.All(x => x < lines[i][j]);

        var rightArray = lines[i].Where((c, ix) => ix > j);
        var right = rightArray.All(x => x > lines[i][j]);

        var topArray = lines.Where((line, ix) => ix < i).Select(x => x[j]);
        var top = topArray.All(x => x < lines[i][j]);

        var bottomArray = lines.Where((line, ix) => ix > i).Select(x => x[j]);
        var bottom = bottomArray.All(x => x < lines[i][j]);

        if (left || right || top || bottom)
            sum++;
        var view =
           find(leftArray.Reverse().ToList(), lines[i][j]) *
           find(rightArray.ToList(), lines[i][j]) *
           find(topArray.Reverse().ToList(), lines[i][j]) *
           find(bottomArray.ToList(), lines[i][j]);
        if (sum2 < view)
            sum2 = view;
    }
}
Console.WriteLine(sum);
Console.WriteLine(sum2);


int find(List<char> array, char character)
{
    var first = array.FirstOrDefault(x => x >= character);
    if (first == default)
        return array.Count();
    else
        return array.IndexOf(first) + 1;
}