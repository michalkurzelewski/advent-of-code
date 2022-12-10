var line = File.ReadLines(@"input.txt").First();
int getMarker(string line, int markerCount)
{
    for (int i = markerCount - 1; i < line.Length; i++)
    {
        var sub = line.Substring(i - (markerCount - 1), markerCount);
        if (sub.Distinct().Count() == sub.Count()) return i + 1;
    }
    return 0;
}

Console.WriteLine(getMarker(line, 4));
Console.WriteLine(getMarker(line, 14));
