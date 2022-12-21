var lines = File.ReadLines(@"input.txt").ToList();

void Mix(List<Number> numbers, Number[] pointers)
{
    foreach (var number in pointers)
    {
        var index = numbers.IndexOf(number) + number.Value;
        numbers.Remove(number);

        index %= numbers.Count;
        if (index < 0) index += numbers.Count;
        numbers.Insert((int)index, number);
    }
}
void Print(List<Number> numbers)
{
    var indexOfZero = numbers.IndexOf(numbers.First(x => x.Value == 0));

    var result = numbers[(indexOfZero + 1000) % numbers.Count].Value +
                 numbers[(indexOfZero + 2000) % numbers.Count].Value +
                 numbers[(indexOfZero + 3000) % numbers.Count].Value;
    Console.WriteLine(result);
}

var numbers = lines.Select(x => new Number {Value = long.Parse(x)}).ToList();
var pointers = numbers.ToArray();

Mix(numbers, pointers);
Print(numbers);

var trueNumbers = lines.Select(x => new Number { Value = long.Parse(x) * 811589153 }).ToList();
var truePointers = trueNumbers.ToArray();

for (int i = 0; i < 10; i++)
    Mix(trueNumbers, truePointers);

Print(trueNumbers);

class Number { public long Value { get; set; } }
