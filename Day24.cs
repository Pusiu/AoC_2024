namespace AoC_2024
{
    public class Day24 : Day
    {
        public Day24() { isTest = false; }
        public override string Part1()
        {
            var split = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wires = new();
            split.First().Split("\n").Select(x =>
            {
                var s = x.Split(":");
                var name = s[0];
                var value = int.Parse(s[1]);
                return new
                {
                    name,
                    value
                };
            }).ToList().ForEach(x => wires.Add(x.name, x.value));

            var gates = split.Last().Split("\n").ToList();
            var q = new Queue<string>(gates);
            while (q.Count > 0)
            {
                var x = q.Dequeue();
                var s = x.Split(" ");
                if (!wires.ContainsKey(s[0]) || !wires.ContainsKey(s[2]))
                {
                    q.Enqueue(x);
                    continue;
                }
                var left = wires[s[0]] == 1;
                var right = wires[s[2]] == 1;
                var op = s[1];
                var value = op switch
                {
                    "AND" => left && right ? 1 : 0,
                    "OR" => left || right ? 1 : 0,
                    "XOR" => left != right ? 1 : 0,
                };
                wires.Add(s[4], value);

            }
            return wires.Where(x => x.Key.Contains('z')).OrderBy(x => int.Parse(x.Key.Substring(1))).Select((x, i) => x.Value * Math.Pow(2, i)).Sum().ToString();
        }

        public override string Part2()
        {
            return "";
        }
    }
}