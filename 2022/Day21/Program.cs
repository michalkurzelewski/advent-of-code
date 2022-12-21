using AngouriMath;

var lines = File.ReadLines(@"input.txt").ToList();

var monkeys = new Dictionary<string, Monkey>();
lines.ForEach(line => monkeys.Add(line.Split(':')[0], new Monkey(line, monkeys)));
Console.WriteLine(monkeys["root"].Value);

monkeys["root"].operation = "=";
var humanValue = monkeys["humn"].Value;
var equation = monkeys["root"].Dzialanie.Replace(humanValue.ToString(), "x");
Entity expr = equation;

Console.WriteLine(expr.Solve("x"));


class Monkey
{
    private Dictionary<string, Monkey> _monkeys;
    private long value;
    public string operation;
    private string left;
    private string right;

    public Monkey(string line, Dictionary<string, Monkey> monkeys)
    {
        _monkeys = monkeys;
        var values = line.Split();
        if (values.Length == 2)
            value = long.Parse(values[1]);
        else
        {
            operation = values[2];
            left = values[1];
            right = values[3];
        }
    }

    public long Value
    {
        get
        {
            return operation switch
            {
                "+" => _monkeys[left].Value + _monkeys[right].Value,
                "-" => _monkeys[left].Value - _monkeys[right].Value,
                "*" => _monkeys[left].Value * _monkeys[right].Value,
                "/" => _monkeys[left].Value / _monkeys[right].Value,
                _ => value,
            };
        }
    }

    public string Dzialanie
    {
        get
        {
            return operation switch
            {
                "+" => $"({_monkeys[left].Dzialanie} + {_monkeys[right].Dzialanie})",
                "-" => $"({_monkeys[left].Dzialanie} - {_monkeys[right].Dzialanie})",
                "*" => $"({_monkeys[left].Dzialanie} * {_monkeys[right].Dzialanie})",
                "/" => $"({_monkeys[left].Dzialanie} / {_monkeys[right].Dzialanie})",
                "=" => $"{_monkeys[left].Dzialanie} = {_monkeys[right].Dzialanie}",
                _ => value.ToString(),
            };
        }
    }
}