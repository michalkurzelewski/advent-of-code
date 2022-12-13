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
packets.Add(two);
packets.Add(six);

packets.Sort(comparer);
var i2 = packets.IndexOf(two) + 1;
var i6 = packets.IndexOf(six) + 1;
Console.WriteLine($"{i2} * {i6} = {i2 * i6}");

public class JTokenComparer : IComparer<JToken>
{
    public int Compare(JToken left, JToken right)
    {
        if (left.Type == JTokenType.Array && right.Type == JTokenType.Array)
        {
            for (int i = 0; i < (left as JArray).Count; i++)
            {
                if (i > (right as JArray).Count - 1) return 1;
                var compare = Compare(left[i], right[i]);
                if (compare != 0)
                    return compare;
            }
            return (left as JArray).Count - (right as JArray).Count;
        }

        if (left.Type == JTokenType.Integer && right.Type == JTokenType.Integer)
            return (int)left - (int)right;

        if (left.Type == JTokenType.Integer && right.Type == JTokenType.Array)
            return Compare(new JArray(left), right);

        if (left.Type == JTokenType.Array && right.Type == JTokenType.Integer)
            return Compare(left, new JArray(right));

        return 0;
    }
}