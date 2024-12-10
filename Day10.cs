namespace AoC_2024
{
    public class Day10 : Day
    {
        public Day10() { isTest = false; }
        public override string Part1()
        {
            int[][] map = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(y => y.Select(x => x == '.' ? -1 : int.Parse(x.ToString())).ToArray()).ToArray();
            Dictionary<(int x, int y), int> scoreMap = new();
            var zeros = map
    .SelectMany((row, y) => row.Select((value, x) => new { x, y, value }))
    .Where(cell => cell.value == 0)
    .Select(cell => new { cell.x, cell.y });

            var nines = map.SelectMany((row, y) => row.Select((value, x) => new { x, y, value }))
    .Where(cell => cell.value == 9)
    .Select(cell => new { cell.x, cell.y });

            foreach (var zero in zeros)
            {
                scoreMap.Add((zero.x, zero.y), 0);
                foreach (var nine in nines)
                {
                    if (GetPathRank(map, zero.x, zero.y, nine.x, nine.y) > 0)
                        scoreMap[(zero.x, zero.y)]++;
                }
            }

            return scoreMap.Values.Sum().ToString();
        }

        static int GetPathRank(int[][] map, int startX, int startY, int endX, int endY)
        {
            Stack<(int x, int y, List<(int x, int y)> history)> locations = new();
            locations.Push((startX, startY, new()));
            List<List<(int x, int y)>> validPaths = new();
            while (locations.Count > 0)
            {
                (int x, int y, List<(int x, int y)> history) = locations.Pop();
                if (x == endX && y == endY)
                {
                    validPaths.Add(history);
                }
                var directions = new List<(int x, int y)>() {
                    (1,0),
                    (-1,0),
                    (0,1),
                    (0,-1)
                };
                var h = new List<(int x, int y)>(history)
                {
                    (x, y)
                };
                foreach (var direction in directions)
                {
                    if (x + direction.x < 0 || x + direction.x >= map[0].Length || y + direction.y < 0 || y + direction.y >= map.Length) continue;
                    if (map[y + direction.y][x + direction.x] - map[y][x] == 1)
                        locations.Push((x + direction.x, y + direction.y, h));
                }
            }

            return validPaths.Count;
        }

        public override string Part2()
        {
            int[][] map = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(y => y.Select(x => x == '.' ? -1 : int.Parse(x.ToString())).ToArray()).ToArray();
            Dictionary<(int x, int y), int> scoreMap = new();
            var zeros = map
    .SelectMany((row, y) => row.Select((value, x) => new { x, y, value }))
    .Where(cell => cell.value == 0)
    .Select(cell => new { cell.x, cell.y });

            var nines = map.SelectMany((row, y) => row.Select((value, x) => new { x, y, value }))
    .Where(cell => cell.value == 9)
    .Select(cell => new { cell.x, cell.y });

            foreach (var zero in zeros)
            {
                scoreMap.Add((zero.x, zero.y), 0);
                foreach (var nine in nines)
                {
                    scoreMap[(zero.x, zero.y)] += GetPathRank(map, zero.x, zero.y, nine.x, nine.y);
                }
            }

            return scoreMap.Values.Sum().ToString();
        }
    }
}