var lines = File.ReadLines(@"input.txt").ToList();
var nodes = lines.Select((x, i) => x.Select((y, j) => new Node
{
    X = i,
    Y = j,
    Distance = int.MaxValue,
    Character = y,
    Visited = false
}).ToArray()).ToArray();

int endX = 0, endY = 0;
var nextToCheck = new List<Node>();

for (int i = 0; i < lines.Count(); i++)
{
    for (int j = 0; j < lines[0].Count(); j++)
    {
        if (lines[i][j] == 'S' || lines[i][j] == 'a')
        //if (lines[i][j] == 'S') // for task 1
        {
            nodes[i][j].Distance = 0;
            nodes[i][j].Character = 'a';
            nodes[i][j].Visited = true;
            nextToCheck.Add(nodes[i][j]);
        }
        if (lines[i][j] == 'E') 
        {
            nodes[i][j].Character = 'z';
            endX = i;
            endY = j;
        }
    }
}

int counter = 0;

while (true)
{
    var check = nextToCheck.OrderBy(x => x.Distance).First();
    nextToCheck.Remove(check);
    counter++;
    if (counter > nodes[0].Count() * nodes.Count()) break;

    if (check.X == endX && check.Y == endY) break;

    checkNode(check.X - 1, check.Y, check);
    checkNode(check.X + 1, check.Y, check);
    checkNode(check.X, check.Y - 1, check);
    checkNode(check.X, check.Y + 1, check);
}

Console.WriteLine(nodes[endX][endY].Distance);

void checkNode(int x, int y, Node prev)
{
    if (x < 0 || y < 0 || x >= nodes.Count() || y >= nodes[0].Count()) return;
    if (nodes[x][y].Visited) return;

    var node = nodes[x][y];
    if (node.Character - prev.Character <= 1)
    {
        node.Visited = true;
        node.Distance = prev.Distance + 1;
        nextToCheck.Add(node);
    }
}

class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Character { get; set; }
    public int Distance { get; set; }
    public bool Visited { get; set; }
}