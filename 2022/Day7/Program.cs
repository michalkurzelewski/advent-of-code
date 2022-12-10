var lines = File.ReadLines(@"input.txt").ToList();

var paths = new Dictionary<string, long>();
var path = string.Empty;

for (int i = 0; i < lines.Count; i++)
{

    if (lines[i][0] == '$')
    {
        var command = lines[i].Split(' ');
        switch (command[1])
        {
            case "cd":
                if (command[2] == "..")
                    path = path.Substring(0, path.LastIndexOf('-'));
                else
                {
                    path += $"-{command[2]}";
                    paths[path] = 0;
                }
                break;
            case "ls":
                i++;
                while (lines[i][0] != '$')
                {
                    if (lines[i][0] != 'd')
                    {
                        var temp = string.Empty;
                        path.Split('-')
                            .Where(x => !string.IsNullOrEmpty(x))
                            .Select(x =>
                            {
                                temp += $"-{x}";
                                return temp;
                            }).ToList()
                            .ForEach(x => paths[x] += long.Parse(lines[i].Split(' ')[0]));
                    }
                    i++;
                    if (i == lines.Count) break;
                }
                i--;
                break;
        }
    }
}
long result1 = paths.Where(x => x.Value <= 100000).Sum(x => x.Value);
var minnimum = 30000000 - (70000000 - paths["-/"]);
var result2 = paths.Where(x => x.Value >= minnimum).Min(x => x.Value);
Console.WriteLine(result1);
Console.WriteLine(result2);


var directory = new Directory();
var allDirectories = new List<Directory>();
for (int i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    if (lines[i][0] == '$')
    {
        var command = lines[i].Split(' ');
        switch (command[1])
        {
            case "cd":
                if (command[2][0] == '.')
                    directory = directory.Parent;
                else
                {
                    directory = directory.AddDir(command[2]);
                    allDirectories.Add(directory);
                }
                break;
            case "ls":
                i++;
                while (lines[i][0] != '$')
                {
                    line = lines[i];
                    if (lines[i][0] != 'd')
                    {
                        var file = lines[i].Split(' ');
                        directory.AddFile(file[1], int.Parse(file[0]));
                    }
                    i++;
                    if (i == lines.Count) break;
                }
                i--;
                break;
            default:
                break;
        }
    }
}
long result5 = allDirectories.Where(x => x.Size <= 100000).Sum(x => x.Size);
var minnimum2 = 30000000 - (70000000 - allDirectories[0].Size);
var result6 = allDirectories.Where(x => x.Size >= minnimum2).Min(x => x.Size);

Console.WriteLine(result5);
Console.WriteLine(result6);

class Directory
{
    public string Name { get; set; }
    public long Size { get; set; }
    public Directory Parent { get; set; }
    public List<string> Files { get; set; } = new List<string>();
    public List<Directory> Directories { get; set; } = new List<Directory>();

    public void AddFile(string file, long size)
    {
        Files.Add(file);
        Size += size;
        var parent = Parent;
        while (parent is not null)
        {
            parent.Size += size;
            parent = parent.Parent;
        }
    }

    public Directory AddDir(string dir)
    {
        var directory = new Directory { Name = dir, Parent = this };
        Directories.Add(directory);
        return directory;
    }

    public Directory GetDirectory(string dir) => Directories.First(x => x.Name == dir);
}