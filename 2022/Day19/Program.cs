using System.Text.RegularExpressions;

var lines = File.ReadLines(@"input.txt").ToList();
var (ORE_R, CLAY_R, OBSIDIAN_R, GEODE_R, ORE, CLAY, OBSIDIAN, GEODE) = (0, 1, 2, 3, 4, 5, 6, 7);
var (BLUEPRINT, ORE_R_COST, CLAY_R_COST, OBSIDIAN_R_ORE_COST, OBSIDIAN_R_CLAY_COST, GEODE_R_ORE_COST, GEODE_R_OBSIDIAN_COST) = (0, 1, 2, 3, 4, 5, 6);

(int, int) GetBlueprintMax(string line, int minutes)
{
    var c = Regex.Matches(line, @"\d+").Select(x => int.Parse(x.Value)).ToArray();
    var factories = Enumerable.Range(0, 50).Select(x => new Dictionary<string, int[]>()).ToList();
    var f = new[] { 1, 0, 0, 0, 0, 0, 0, 0 };
    factories[0].Add(FactoryHash(f), f);

    var maxOreCost = Math.Max(c[ORE_R_COST], Math.Max(c[OBSIDIAN_R_ORE_COST], c[GEODE_R_ORE_COST]));
    var maxClayCost = Math.Max(c[CLAY_R_COST], c[OBSIDIAN_R_CLAY_COST]);

    var max = 0;
    for (var i = 0; i < minutes; i++)
    {
        foreach (var factory in factories[i].Values)
        {
            var newMax = factory[GEODE] + (factory[GEODE_R] * (minutes - i));
            if (newMax > max)
            {
                max = newMax;
                //Console.WriteLine($"i:{i} {FactoryHash(factory)}");
            }

            int wait;
            if (factory[ORE_R] < maxOreCost)
            {
                wait = HowLong(c[ORE_R_COST], factory[ORE_R], factory[ORE]);
                var boughtOreR = CloneFactory(factory, wait);
                boughtOreR[ORE_R]++;
                boughtOreR[ORE] -= c[ORE_R_COST];
                factories[i + wait].TryAdd(FactoryHash(boughtOreR), boughtOreR);
                //Console.WriteLine($"i:{i} {FactoryHash(factory)} -> i:{i+wait} {FactoryHash(boughtOreR)}");
            }

            if (factory[CLAY_R] < maxClayCost)
            {
                wait = HowLong(c[CLAY_R_COST], factory[ORE_R], factory[ORE]);
                var boughtClayR = CloneFactory(factory, wait);
                boughtClayR[CLAY_R]++;
                boughtClayR[ORE] -= c[CLAY_R_COST];
                factories[i + wait].TryAdd(FactoryHash(boughtClayR), boughtClayR);
                //Console.WriteLine($"i:{i} {FactoryHash(factory)} -> i:{i+wait} {FactoryHash(boughtClayR)}");
            }

            if (factory[CLAY_R] > 0 && factory[OBSIDIAN_R] < c[GEODE_R_OBSIDIAN_COST])
            {
                wait = Math.Max(HowLong(c[OBSIDIAN_R_ORE_COST], factory[ORE_R], factory[ORE]),
                                HowLong(c[OBSIDIAN_R_CLAY_COST], factory[CLAY_R], factory[CLAY]));
                var boughtObsidianR = CloneFactory(factory, wait);
                boughtObsidianR[OBSIDIAN_R]++;
                boughtObsidianR[ORE] -= c[OBSIDIAN_R_ORE_COST];
                boughtObsidianR[CLAY] -= c[OBSIDIAN_R_CLAY_COST];
                factories[i + wait].TryAdd(FactoryHash(boughtObsidianR), boughtObsidianR);
                //Console.WriteLine($"i:{i} {FactoryHash(factory)} -> i:{i+wait} {FactoryHash(boughtObsidianR)}");
            }

            if (factory[OBSIDIAN_R] > 0)
            {
                wait = Math.Max(HowLong(c[GEODE_R_ORE_COST], factory[ORE_R], factory[ORE]),
                                HowLong(c[GEODE_R_OBSIDIAN_COST], factory[OBSIDIAN_R], factory[OBSIDIAN]));
                var boughtGeodeR = CloneFactory(factory, wait);
                boughtGeodeR[GEODE_R]++;
                boughtGeodeR[ORE] -= c[GEODE_R_ORE_COST];
                boughtGeodeR[OBSIDIAN] -= c[GEODE_R_OBSIDIAN_COST];
                factories[i + wait].TryAdd(FactoryHash(boughtGeodeR), boughtGeodeR);
                //Console.WriteLine($"i:{i} {FactoryHash(factory)} -> i:{i+wait} {FactoryHash(boughtGeodeR)}");
            }
        }
    }

    return (c[BLUEPRINT], max);
}


var sum = 0;
foreach (var line in lines)
{
    var (blueprint, max) = GetBlueprintMax(line, 24);
    sum += blueprint * max;
    Console.WriteLine($"{blueprint} {max}");
}
Console.WriteLine(sum);

Console.WriteLine("Big boi challenge");
sum = 1;
for (var i = 0; i < 3; i++)
{
    var (blueprint, max) = GetBlueprintMax(lines[i], 32);
    sum *= blueprint * max;
    Console.WriteLine($"{blueprint} {max}");
}
Console.WriteLine(sum);

int HowLong(int cost, int robots, int alreadyHave)
{
    if (alreadyHave >= cost) return 1;
    var left = (double)(cost - alreadyHave) / robots;
    return (int)Math.Ceiling(left) + 1;
}

int[] CloneFactory(int[] factory, int wait)
{
    var newFactory = (int[])factory.Clone();
    newFactory[ORE] += factory[ORE_R] * wait;
    newFactory[CLAY] += factory[CLAY_R] * wait;
    newFactory[OBSIDIAN] += factory[OBSIDIAN_R] * wait;
    newFactory[GEODE] += factory[GEODE_R] * wait;

    return newFactory;
}

string FactoryHash(int[] factory) => string.Join(" ", factory);