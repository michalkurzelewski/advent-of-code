var lines = File.ReadLines(@"input.txt").ToList();
var decoder = new Dictionary<char, long> {{'2', 2}, {'1', 1}, {'0', 0}, {'-', -1}, {'=', -2}};
var encoder = new Dictionary<long, Tuple<char, long>>
{
    {0, new Tuple<char, long>('0', 0)},
    {1, new Tuple<char, long>('1', 0)},
    {2, new Tuple<char, long>('2', 0)},
    {3, new Tuple<char, long>('=', 1)},
    {4, new Tuple<char, long>('-', 1)},
    {5, new Tuple<char, long>('0', 1)}
};

Console.WriteLine(Encode(lines.Select(Decode).Sum()));

long Decode(string line) => line.Select((c, i) => (long) Math.Pow(5, line.Length - i - 1) * decoder[c]).Sum();

string Encode(long number)
{
    var (snafu, index) = (new char[20], 0);
    while (number > 0)
    {
        var (div, rem) = Math.DivRem(number, 5);
        number = div + encoder[rem].Item2;
        snafu[index++] = encoder[rem].Item1;
    }
    return string.Concat(snafu.Reverse());
}