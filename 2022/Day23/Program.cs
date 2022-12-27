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

var plannedMoves = new Dictionary<string, Elf>();

for (var round = 0; round < 1000; round++)
{
    plannedMoves.Clear();
    foreach (var elf in elfs)
    {
        var NW = board.Get(elf.X[0] - 1, elf.Y[0] - 1);
        var N = board.Get(elf.X[0], elf.Y[0] - 1);
        var NE = board.Get(elf.X[0] + 1, elf.Y[0] - 1);
        var E = board.Get(elf.X[0] + 1, elf.Y[0]);
        var SE = board.Get(elf.X[0] + 1, elf.Y[0] + 1);
        var S = board.Get(elf.X[0], elf.Y[0] + 1);
        var SW = board.Get(elf.X[0] - 1, elf.Y[0] + 1);
        var W = board.Get(elf.X[0] - 1, elf.Y[0]);

        var directions = new[]
        {
            new {available = new[] {NW, N, NE}.All(x => x == default), next = new[] {elf.X[0], elf.Y[0] - 1}},
            new {available = new[] {SE, S, SW}.All(x => x == default), next = new[] {elf.X[0], elf.Y[0] + 1}},
            new {available = new[] {SW, W, NW}.All(x => x == default), next = new[] {elf.X[0] - 1, elf.Y[0]}},
            new {available = new[] {NE, E, SE}.All(x => x == default), next = new[] {elf.X[0] + 1, elf.Y[0]}}
        };

        var all = directions.All(x => x.available);
        var none = directions.All(x => !x.available);

        if (all || none) continue;

        for (var i = 0; i < 4; i++)
        {
            if (directions[(i + round) % 4].available)
            {
                var next = directions[(i + round) % 4].next;
                if (plannedMoves.TryGetValue($"{next[0]} {next[1]}", out var youAreNotGoing))
                    youAreNotGoing.Clear();
                else
                {
                    elf.SetNext(next);
                    plannedMoves[$"{next[0]} {next[1]}"] = elf;
                }
                break;
            }
        }
    }

    if (plannedMoves.Count == 0)
    {
        Console.WriteLine($"No elfs moved on round {round+1}");
        break;
    }

    foreach (var elf in elfs)
    {
        board.Move(elf.X[0], elf.Y[0], elf.X[1], elf.Y[1]);
        elf.Move();
    }

    if (round == 9)
    {
        var minX = elfs.Min(elf => elf.X[0]);
        var maxX = elfs.Max(elf => elf.X[0]);
        var maxY = elfs.Max(elf => elf.Y[0]);
        var minY = elfs.Min(elf => elf.Y[0]);

        Console.WriteLine((Math.Abs(minX - maxX) + 1) * (Math.Abs(minY - maxY) + 1) - elfs.Count);
    }
}


class Board
{
    private const int offset = 1000;

    public char[,] _board { get; set; } = new char[3000, 3000];

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
    public int[] X { get; set; }
    public int[] Y { get; set; }

    public void SetNext(int[] next)
    {
        X[1] = next[0];
        Y[1] = next[1];
    }

    public Elf(int x, int y)
    {
        X = new[] {x, x};
        Y = new[] {y, y};
    }

    public void Clear()
    {
        X[1] = X[0];
        Y[1] = Y[0];
    }

    public void Move()
    {
        X[0] = X[1];
        Y[0] = Y[1];
    }
}