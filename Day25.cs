namespace AoC_2024
{
    public class Day25 : Day
    {
        public Day25() { isTest = false; }
        public override string Part1()
        {
            var sets = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).Select(x => {
                var rows = x.Split("\n");
                var isKey = rows[0][0] == '.';
                var heights = new int[rows[0].Length];
                for (var i=0; i < rows[0].Length; i++)
                {
                    var h = -1;
                    for (var j=0; j < rows.Length; j++)
                    {
                        if (rows[j][i] == '#') h++;
                    }
                    heights[i]=h;
                }
                return new {
                    isKey,
        heights,
        totalHeight=rows.Length,
        freeSpaces = heights.Select(y => rows.Length-y-1).ToArray()
                };
                }).GroupBy(x => x.isKey);
            
            var keys = sets.First(g => g.Key).ToList();
            var locks = sets.First(g => !g.Key).ToList();

            List<(int[] lockHeights, int[] keyHeights)> validCombinations = new();
            foreach (var l in locks)
            {
                foreach (var k in keys)
                {
                    bool valid=true;
                    // Console.Write($"Lock {string.Join(',', l.heights)} and key {string.Join(',', k.heights)}: ");
                    for (int i=0; i < k.heights.Length; i++)
                    {
                        if (l.freeSpaces[i] <= k.heights[i]) {
                            valid = false;
                            // Console.WriteLine($"overlap in {i} column");
                            break;
                        };
                    }
                    if (valid)
                    {
                        // Console.WriteLine("all columns fit!");
                        validCombinations.Add((l.heights, k.heights));
                    }
                }
            }
            
            return validCombinations.Count.ToString();
        }

        public override string Part2()
        {
            return "";
        }
    }
}