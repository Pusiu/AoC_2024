namespace AoC_2024
{
    public class Day11 : Day
    {
        public Day11() { isTest = false; }
        public override string Part1()
        {
            var stones = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            return Simulate(stones, 25).ToString();
        }

        private static long Simulate(List<long> stones, int steps)
        {
            Dictionary<long,long> dict = stones.ToDictionary(x => x, v => 1L);
            for (int i = 0; i < steps; i++)
            {
                Dictionary<long,long> newDict = new ();
                for (int j = 0; j < dict.Keys.Count; j++)
                {
                    var key = dict.Keys.ElementAt(j);
                    if (dict[key]==0) continue;

                    if (key == 0)
                    {
                        if (newDict.ContainsKey(1))
                            newDict[1]+=dict[key];
                        else
                            newDict.Add(1, dict[key]);
                    }
                    else if (key.ToString().Length % 2 == 0)
                    {
                        var s = key.ToString();
                        var left = long.Parse(s.Take(s.Length / 2).ToArray());
                        var right = long.Parse(s.Skip(s.Length / 2).ToArray());
                        var value = dict[key];
                        if (newDict.ContainsKey(left))
                            newDict[left]+=value;
                        else
                            newDict.Add(left, value);

                        if (newDict.ContainsKey(right))
                            newDict[right]+=value;
                        else
                            newDict.Add(right, value);
                    }
                    else
                    {
                        long newNumber = key*2024;
                        if (newDict.ContainsKey(newNumber))
                            newDict[newNumber]+=dict[key];
                        else
                            newDict.Add(newNumber,dict[key]);
                    }
                }
                dict=newDict;
            }
            return dict.Values.Sum();
        }

        public override string Part2()
        {
            var stones = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            return Simulate(stones, 75).ToString();
        }
    }
}