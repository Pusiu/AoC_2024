namespace AoC_2024
{
    public class Day8 : Day
    {
        public Day8() {isTest = false;}
        public override string Part1()
        {
            char[][] map = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
            var antinodes = CreateAntinodeMap(map);
            return antinodes.Sum(x => x.Where(y => y == '#').Count()).ToString();
        }

        private char[][] CreateAntinodeMap(char[][] originalMap, bool part2=false)
        {
            char[][] antinodeMap = originalMap.Select(y => y.Select(x => x).ToArray()).ToArray();
            var frequencies = antinodeMap.SelectMany((e, y) => e.Select((e2, x) =>             
                 new {
                    x,
                    y,
                    Symbol = e2
                }        
            )).GroupBy(x => x.Symbol).Where(x => x.Key != '.').ToDictionary(x => x.Key, x => x.ToList());

            foreach (var freq in frequencies)
            {
                for (int i=0; i < freq.Value.Count; i++)
                {
                    for (int j=0; j < freq.Value.Count; j++)
                    {
                        if (i==j) continue;
                        (int x, int y) dist = (freq.Value[i].x-freq.Value[j].x, freq.Value[i].y-freq.Value[j].y);
                        (int x, int y) = (freq.Value[i].x+dist.x, freq.Value[i].y+dist.y);
                        if (IsInBounds(originalMap, x,y))
                        {
                            antinodeMap[y][x]='#';
                        }
                        while (true)
                        {
                            x += dist.x;
                            y += dist.y;
                            if (IsInBounds(originalMap, x,y))
                            {
                                antinodeMap[y][x]='#';
                            }
                            else
                                break;
                        }
                        x=freq.Value[j].x-dist.x;
                        y=freq.Value[j].y-dist.y;
                        if (IsInBounds(originalMap, x,y))
                        {
                            antinodeMap[y][x]='#';
                        }
                        while (true)
                        {
                            x -= dist.x;
                            y -= dist.y;
                            if (IsInBounds(originalMap, x,y))
                            {
                                antinodeMap[y][x]='#';
                            }
                            else
                                break;
                        }
                    }
                }
            }

            antinodeMap.ToList().ForEach(x => {
                x.ToList().ForEach(y => Console.Write(y));
                Console.WriteLine();
            });
            
            return antinodeMap;
        }

        private static bool IsInBounds(char[][] map, int x, int y)
        {
            if (x < 0 || x >= map[0].Length || y < 0 || y >= map.Length) return false;
            return true;
        }

        public override string Part2()
        {
            char[][] map = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
            var antinodes = CreateAntinodeMap(map, true);
            return antinodes.Sum(x => x.Where(y => y != '.').Count()).ToString();
        }
    }
}