var lines = File.ReadLines(@"input.txt").ToList();
const int MAX = 22;
var grid = new int[MAX, MAX, MAX];

int countWalls(int x, int y, int z, int color, int[,,] grid)
{
    var walls = 0;
    if (!(grid[x + 1, y, z] == color)) walls++; else walls--;
    if (!(grid[x - 1, y, z] == color)) walls++; else walls--;
    if (!(grid[x, y + 1, z] == color)) walls++; else walls--;
    if (!(grid[x, y - 1, z] == color)) walls++; else walls--;
    if (!(grid[x, y, z + 1] == color)) walls++; else walls--;
    if (!(grid[x, y, z - 1] == color)) walls++; else walls--;
    return walls;
}

var sum = 0;
foreach (var line in lines)
{
    var c = line.Split(',').Select(int.Parse).Select(x => x + 1).ToArray();
    sum += countWalls(c[0], c[1], c[2], 1, grid);
    grid[c[0], c[1], c[2]] = 1;
}
Console.WriteLine(sum);

void ColorPocket(int x, int y, int z, int color)
{
    if (x < 0 || x > MAX - 1 ||
        y < 0 || y > MAX - 1 ||
        z < 0 || z > MAX - 1) return;
    if (grid[x, y, z] == 0)
    {
        grid[x, y, z] = color;
        ColorPocket(x + 1, y, z, color);
        ColorPocket(x - 1, y, z, color);
        ColorPocket(x, y + 1, z, color);
        ColorPocket(x, y - 1, z, color);
        ColorPocket(x, y, z + 1, color);
        ColorPocket(x, y, z - 1, color);
    }
    if (grid[x, y, z] == 1) return;
}

var color = 2;
for (int x = 0; x < MAX; x++)
    for (int y = 0; y < MAX; y++)
        for (int z = 0; z < MAX; z++)
            if (grid[x, y, z] == 0)
                ColorPocket(x, y, z, color++);

var pocketSums = Enumerable.Range(3, color).ToDictionary(x => x, y => 0);
var colorsToIgnore = new int[]{ 0, 1, 2 };
for (int x = 0; x < MAX; x++)
{
    for (int y = 0; y < MAX; y++)
    {
        for (int z = 0; z < MAX; z++)
        {
            color = grid[x, y, z];
            if (colorsToIgnore.Contains(color)) 
                continue;
            else
            {
                pocketSums[color] += countWalls(x, y, z, color, grid);
                grid[x, y, z] = 1;
            }
        }
    }
}

foreach (var pocketSum in pocketSums.Values)
    sum -= pocketSum;

Console.WriteLine(sum);