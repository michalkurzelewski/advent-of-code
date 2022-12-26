var lines = File.ReadLines(@"input.txt").ToList();

var elfs = new List<Elf>();
var board = new Board();
for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '#')
        {
            elfs.Add(new Elf(j, i));
            board.Mark(j, i);
        }
    }
}

foreach (var elf in elfs)
{
    var NW = board.Get(elf.X[0] - 1, elf.Y[0] - 1);
    var N = board.Get(elf.X[0], elf.Y[0] - 1);
    var NE = board.Get(elf.X[0] + 1, elf.Y[0] - 1);
    var E = board.Get(elf.X[0] + 1, elf.Y[0]);
    var SE = board.Get(elf.X[0] + 1, elf.Y[0] + 1);
    var S = board.Get(elf.X[0], elf.Y[0]) + 1;
    var SW = board.Get(elf.X[0] - 1, elf.Y[0] + 1);
    var W = board.Get(elf.X[0] - 1, elf.Y[0]);



}



class Board
{
    private const int offset = 1000;
    public char[,] _board { get; set; } = new char[5000, 5000];

    public void Mark(int x, int y) => _board[x + offset, y + offset] = '#';

    public void Move(int x1, int y1, int x2, int y2)
    {
        _board[x1 + offset, y1 + offset] = default;
        _board[x2 + offset, y2 + offset] = '#';
    }

    public char Get(int x, int y) => _board[x + offset, y + offset];
}

class Elf
{
    public int[] X { get; set; } = new int[2];
    public int[] Y { get; set; } = new int[2];

    public Elf(int x, int y)
    {
        X[0] = x;
        Y[0] = y;
    }
}