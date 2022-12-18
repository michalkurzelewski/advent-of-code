var lines = File.ReadLines(@"input.txt").ToList();
var nodes = new Dictionary<string, Node>();

lines.ForEach(line =>
{
    var entry = line.Split(';')[0];
    var name = entry.Split(' ')[1];
    nodes[name] = new Node 
    { 
        Name = name,
        Rate = int.Parse(entry.Split('=')[1]) 
    };
});
foreach (var node in nodes)
    node.Value.Distances = nodes.Select(x => new { x.Key, int.MaxValue }).Where(x => x.Key != node.Key).ToDictionary(x => x.Key, y => y.MaxValue);

lines.ForEach(line =>
{
    var uppers = string.Concat(line.Where(x => char.IsUpper(x) || x == ' '))
                        .Split()
                        .Where(x=> !string.IsNullOrEmpty(x))
                        .ToArray();
    for (int i = 2; i < uppers.Length; i++)
    {
        nodes[uppers[1]].Next.Add(nodes[uppers[i]]);
        nodes[uppers[1]].Distances[uppers[i]] = 2;
    }  
});

foreach (var node in nodes.Values)
{
    var queue = new Queue<string>();
    node.Next.ForEach(x => queue.Enqueue(x.Name));
    var visited = new HashSet<string> { node.Name };
    while(queue.Count > 0)
    {
        var n = queue.Dequeue();
        nodes[n].Next.ForEach(x =>
        {
            if (!visited.Contains(x.Name))
            {
                node.Distances[x.Name] = node.Distances[n] + 1;
                queue.Enqueue(x.Name);
            }
        });
        visited.Add(n);
    }
    node.Distances = node.Distances.Where(x => nodes[x.Key].WorthIt).ToDictionary(x => x.Key, y => y.Value);
}

int Check1(Node node, HashSet<string> visited, int minutesLeft, int sumSoFar)
{
    visited.Add(node.Name);
    var maxSum = sumSoFar;
    foreach (var n in node.Distances)
    {
        if (visited.Contains(n.Key)) continue;
        var leftMinutes = minutesLeft - n.Value;
        if (leftMinutes <= 0) continue;
        var sum = Check1(nodes[n.Key], visited, leftMinutes, sumSoFar + (nodes[n.Key].Rate * leftMinutes));
        if(maxSum < sum)
            maxSum = sum;
    }

    visited.Remove(node.Name);
    return maxSum;
}

int CheckWithElephant(Node node, HashSet<string> visited, int minutesLeft, int sumSoFar)
{
    visited.Add(node.Name);
    var maxSum = Check1(nodes["AA"], visited, 26, sumSoFar);

    foreach (var n in node.Distances)
    {
        if (visited.Contains(n.Key)) continue;
        var leftMinutes = minutesLeft - n.Value;
        if (leftMinutes <= 0) continue;
        var sum = CheckWithElephant(nodes[n.Key], visited, leftMinutes, sumSoFar + (nodes[n.Key].Rate * leftMinutes));
        if (maxSum < sum)
            maxSum = sum;
    }

    visited.Remove(node.Name);
    return maxSum;
}

var sum = Check1(nodes["AA"], new HashSet<string>(), 30, 0);
Console.WriteLine(sum);

sum = CheckWithElephant(nodes["AA"], new HashSet<string>(), 26, 0);
Console.WriteLine(sum);


class Node
{
    public List<Node> Next { get; set; } = new List<Node>();
    public Dictionary<string, int> Distances { get; set; }
    public int Rate { get; set; }
    public string Name { get; set; }
    public bool WorthIt => Rate > 0;
}