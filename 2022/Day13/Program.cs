using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var lines = File.ReadLines(@"input.txt").ToList();
var comparer = new JTokenComparer();
int sum = 0;
for (int i = 0, count = 1; i < lines.Count; i += 3, count++)
{
    var left = JsonConvert.DeserializeObject<JArray>(lines[i]);
    var right = JsonConvert.DeserializeObject<JArray>(lines[i + 1]);

    if (comparer.Compare(left, right) < 0)
        sum += count;
}
Console.WriteLine(sum);

var packets = lines.Where(x => !string.IsNullOrEmpty(x)).Select(x => JsonConvert.DeserializeObject<JToken>(x)).ToList();
var two = JsonConvert.DeserializeObject<JToken>("[[2]]");
var six = JsonConvert.DeserializeObject<JToken>("[[6]]");
packets.AddRange(new[] { two, six });

packets.Sort(comparer);
var i2 = packets.IndexOf(two) + 1;
var i6 = packets.IndexOf(six) + 1;
Console.WriteLine($"{i2} * {i6} = {i2 * i6}");

public class JTokenComparer : IComparer<JToken>
{
    public int Compare(JToken left, JToken right)
    {
        return (left, right) switch 
        {
            { left.Type: JTokenType.Array, right.Type: JTokenType.Array } => CompareArrays(left, right),
            { left.Type: JTokenType.Integer, right.Type: JTokenType.Integer } => (int)left - (int)right,
            { left.Type: JTokenType.Integer, right.Type: JTokenType.Array } => Compare(new JArray(left), right),
            { left.Type: JTokenType.Array, right.Type: JTokenType.Integer } => Compare(left, new JArray(right)),
            _ => 0
        };
    }

    int CompareArrays(JToken left, JToken right)
    {
        for (int i = 0; i < left.Count(); i++)
        {
            if (i > right.Count() - 1) return 1;
            var compare = Compare(left[i], right[i]);
            if (compare != 0) return compare;
        }
        return left.Count() - right.Count();
    }
}