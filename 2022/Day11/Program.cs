var modulo = 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19;
var monkeys = new[]{
    new Monkey
    {
        Items = new Queue<long>(new long[] { 83, 97, 95, 67 }),
        Operation = a => (a * 19) % modulo,
        Test = a => a % 17 == 0,
        SendWhenTrue = 2,
        SendWhenFalse = 7
    },
    new Monkey
    {
        Items = new Queue<long>(new long[] { 71, 70, 79, 88, 56, 70 }),
        Operation = a => a + 2,
        Test = a => a % 19 == 0,
        SendWhenTrue = 7,
        SendWhenFalse = 0
    },
    new Monkey
    {
        Items = new Queue<long>(new long[] { 98, 51, 51, 63, 80, 85, 84, 95 }),
        Operation = a => a + 7,
        Test = a => a % 7 == 0,
        SendWhenTrue = 4,
        SendWhenFalse = 3
    },
    new Monkey
    {
        Items = new Queue<long>(new long[] { 77, 90, 82, 80, 79 }),
        Operation = a => a + 1,
        Test = a => a % 11 == 0,
        SendWhenTrue = 6,
        SendWhenFalse = 4
    },
    new Monkey
    {
        Items = new Queue<long>(new long[] { 68 }),
        Operation = a => (a * 5) % modulo,
        Test = a => a % 13 == 0,
        SendWhenTrue = 6,
        SendWhenFalse = 5
    },
    new Monkey
    {
        Items = new Queue<long>(new long[] { 60, 94 }),
        Operation = a => a + 5,
        Test = a => a % 3 == 0,
        SendWhenTrue = 1,
        SendWhenFalse = 0
    },
    new Monkey
    {
        Items = new Queue<long>(new long[] { 81, 51, 85 }),
        Operation = a => (a * a) % modulo,
        Test = a => a % 5 == 0,
        SendWhenTrue = 5,
        SendWhenFalse = 1
    },
    new Monkey
    {
        Items = new Queue<long>(new long[] { 98, 81, 63, 65, 84, 71, 84 }),
        Operation = a => a + 3,
        Test = a => a % 2 == 0,
        SendWhenTrue = 2,
        SendWhenFalse = 3
    },
};

for (int i = 0; i < 10000; i++)
{
    for (int j = 0; j < monkeys.Count(); j++)
    {
        long? itemToThrow;
        int? toWhom;
        do
        {
            (itemToThrow, toWhom) = monkeys[j].ItemToThrow();
            if (itemToThrow.HasValue)
                monkeys[toWhom.Value].Items.Enqueue(itemToThrow.Value);
        } while (itemToThrow.HasValue);
    }
    
}
var round10000 = monkeys.OrderByDescending(x => x.TimesInspectedItem).Select(x => x.TimesInspectedItem).Take(2).ToArray();
Console.WriteLine(round10000[0] * round10000[1]);

class Monkey
{
    public Queue<long> Items { get; set; }
    public Func<long, long> Operation { get; set; }
    public Func<long, bool> Test { get; set; }
    public int SendWhenTrue { get; set; }
    public int SendWhenFalse { get; set; }
    public long TimesInspectedItem { get; set; }

    public (long?, int?) ItemToThrow()
    {
        if (Items.TryDequeue(out var item))
        {
            item = Operation(item);
            TimesInspectedItem++;
            if (Test(item))
                return (item, SendWhenTrue);
            else
                return (item, SendWhenFalse);
        }
        return (null, null);
    }
}