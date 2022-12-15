var input = File.ReadLines(@"input.txt").ToList();
var lines = input.Select(x => x.Split(" -> ").Select(y => y.Split(',').Select(z=> int.Parse(z)).ToArray()).ToArray()).ToList();

var maxY = lines.Max(x => x.Max(y => y[1]));
var board = new char[1000, maxY + 4];
var sum = 0;

lines.ForEach(points =>
{
    for (int j = 1; j < points.Count(); j++)
    {
        var startX = points[j - 1][0];
        var startY = points[j - 1][1];
        var endX = points[j][0];
        var endY = points[j][1];

        var addX = Math.Sign(endX - startX);
        var addY = Math.Sign(endY - startY);

        for (int x = startX, y = startY; x != endX || y != endY; x += addX, y += addY) 
            board[x, y] = '#';

        board[endX, endY] = '#';
    }
});

while (true) 
    if (!AddZiarenko(500, 0)) 
        break;
Console.WriteLine(sum);

maxY += 2;
for (int i = 0; i < board.GetLength(0); i++)
    board[i, maxY] = '#';

while (true) 
    if (!AddZiarenko(500, 0)) 
        break;
Console.WriteLine(sum);


bool AddZiarenko(int x, int y)
{
    while(true)
    {
        if (y == maxY + 1)
            return false;
        else if (y == 0 && board[x - 1, y + 1] != default && board[x, y + 1] != default && board[x + 1, y + 1] != default) 
        {
            board[x, y] = 'o';
            sum++;
            return false;
        }
        else if (board[x, y + 1] == default) 
            y++;
        else if (board[x - 1, y + 1] != default && board[x + 1, y + 1] != default)
        {
            board[x, y] = 'o';
            sum++;
            return true;
        }
        else if (board[x - 1, y + 1] == default)
        {
            x--; y++; 
        }
        else if (board[x + 1, y + 1] == default)
        {
            x++; y++; 
        }
    }
}
