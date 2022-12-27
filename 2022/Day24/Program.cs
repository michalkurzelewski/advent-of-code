var lines = File.ReadLines(@"input.txt").ToList();

int FindRoute(List<Blizzard>? blizzards, int[] start, int[] end)
{
    
    var moves = new Dictionary<string, int[]> { { "x", new[] { start[0], start[1] } } };
    var round = 0;
    while (true)
    {
        var nextMoves = new Dictionary<string, int[]>();
        var blizzardsHash = blizzards.Select(x => $"{x.Coord[0]} {x.Coord[1]}").ToHashSet();

        foreach (var move in moves.Values)
        {
            if (move[0] == end[0] && move[1] == end[1]) return round;
            if (move[1] != start[1] || (move[1] == start[1] && move[0]!= start[0]))
            {
                if (move[0] <= 0 ||
                    move[0] >= lines[0].Length - 1 ||
                    move[1] <= 0  ||
                    move[1] >= lines.Count - 1) continue;
            }
            
            if (blizzardsHash.Contains($"{move[0]} {move[1]}")) continue;

            nextMoves[$"{move[0]} {move[1]}"] = new[] { move[0], move[1] };
            nextMoves[$"{move[0] - 1} {move[1]}"] = new[] { move[0] - 1, move[1] };
            nextMoves[$"{move[0] + 1} {move[1]}"] = new[] { move[0] + 1, move[1] };
            nextMoves[$"{move[0]} {move[1] - 1}"] = new[] { move[0], move[1] - 1 };
            nextMoves[$"{move[0]} {move[1] + 1}"] = new[] { move[0], move[1] + 1 };
        }

        moves = nextMoves;
        blizzards.ForEach(x => x.Move());
        round++;
    }
}

var (maxX, maxY) = (lines[0].Length - 2, lines.Count - 2);
var blizzards = lines.Select((line, y) => line
    .Select((c, x) =>
    {
        if (c == '.' || c == '#') 
            return null;
        return new Blizzard(new[] {x, y}, c switch
        {
            '^' => new[] {0, -1},
            '>' => new[] {1, 0},
            '<' => new[] {-1, 0},
            'v' => new[] {0, 1}
        }, maxX, maxY);
    }).Where(x=> x is not null))
    .SelectMany(x => x).ToList();


var start = new[] { 1, 0 };
var end = new[] { lines[0].Length - 2, lines.Count - 1 };
var round1 = FindRoute(blizzards, start,end );
var round2 = FindRoute(blizzards, end,start );
var round3 = FindRoute(blizzards, start,end );

Console.WriteLine(round1);
Console.WriteLine(round1 + round2 + round3);


class Blizzard
{
    private readonly int[] _iterator;
    private readonly int _maxX;
    private readonly int _maxY;
    public int[] Coord { get; set; }


    public Blizzard(int[] coord, int[] iterator, int maxX, int maxY)
    {
        Coord = coord;
        _iterator = iterator;
        _maxX = maxX;
        _maxY = maxY;
    }

    public void Move()
    {
        Coord[0] += _iterator[0];
        if (Coord[0] < 1) Coord[0] = _maxX;
        if (Coord[0] > _maxX) Coord[0] = 1;

        Coord[1] += _iterator[1];
        if (Coord[1] < 1) Coord[1] = _maxY;
        if (Coord[1] > _maxY) Coord[1] = 1;
    }
}
