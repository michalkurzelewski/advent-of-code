var lines = File.ReadLines(@"input.txt").ToList();

int k;
var sum = 0;
long sum2 = 0;
var dict = new Dictionary<string, int>();

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (char.IsDigit(lines[i][j])){
            for (k = j + 1; k < lines[i].Length; k++)
                if (char.IsDigit(lines[i][k])) 
                    continue;
                else
                    break;

            var number = int.Parse(lines[i][j..k]);

            var startx = Math.Max(0, j-1);
            var endx = Math.Min(lines[i].Length-1, k);
            var starty = Math.Max(0, i - 1);
            var endy = Math.Min(lines[i].Length-1, i + 1);
            var isSymbol = false;
            for (int a = starty; a <= endy; a++)
            {
                for (int b = startx; b <= endx; b++)
                {
                    if (lines[a][b] != '.' && !char.IsDigit(lines[a][b]))
                    {
                        isSymbol = true;

                        if(lines[a][b] == '*')
                        {
                            var key = $"{a},{b}";
                            if (dict.ContainsKey(key))
                            {
                                sum2 += dict[key] * number;
                            }
                            else
                            {
                                dict[key] = number;
                            }
                        }

                        break;
                    }
                }
                if (isSymbol) break;
            }

            if (isSymbol) sum += number;
            j = k - 1;
        }
    }
}


Console.WriteLine(sum);
Console.WriteLine(sum2);

//Didn't like that challenge, so I'm not planning to make this solution better and I'm putting the first version
