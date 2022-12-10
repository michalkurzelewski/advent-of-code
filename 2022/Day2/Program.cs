var lines = File.ReadLines(@"input.txt").ToList();
//Rock (A) (X)
//Paper (B) (Y) 
//Scissors (C) (Z)

var a = new Dictionary<string, int>
{
    { "A X", 3},
    { "A Y", 6},
    { "A Z", 0},
    { "B X", 0},
    { "B Y", 3},
    { "B Z", 6},
    { "C X", 6},
    { "C Y", 0},
    { "C Z", 3},
};
var b = new Dictionary<string, int>
{
    { "X", 1},
    { "Y", 2},
    { "Z", 3}
};
var sum1 = lines.Sum(x => a[x] + b[x[2].ToString()]);
Console.WriteLine(sum1);

var c = new Dictionary<string, int>
{
    { "X", 0},
    { "Y", 3},
    { "Z", 6}
};
var d = new Dictionary<string, int>
{
    { "A X", 3},
    { "A Y", 1},
    { "A Z", 2},
    { "B X", 1},
    { "B Y", 2},
    { "B Z", 3},
    { "C X", 2},
    { "C Y", 3},
    { "C Z", 1},
};
var sum2 = lines.Sum(x => c[x[2].ToString()] + d[x]);
Console.WriteLine(sum2);