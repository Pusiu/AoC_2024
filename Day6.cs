using System.Dynamic;
using System.Linq.Expressions;

namespace AoC_2024
{
    public class Day6 : Day
    {
        public Day6() { isTest = false; }
        public override string Part1()
        {
            var map = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
            (int x, int y) = map
                .SelectMany((row, y) => row.Select((ch, x) => (ch, x, y)))
                .Where(p => p.ch == '^')
                .Select(p => (p.x, p.y))
                .First();
            return Walk(map, x, y).visited.Keys.Select(x => (x.x, x.y)).Distinct().Count().ToString();
        }

        (bool loop, Dictionary<(int x, int y, int dirx, int diry), int> visited) Walk(char[][] map, int startX, int startY)
        {
            Dictionary<(int x, int y, int dirx, int diry), int> visited = new();
            int currentX=startX;
            int currentY=startY;
            (int x, int y) currentDirection = map[currentY][currentX] switch
            {
                '^' => (0, -1),
                '>' => (1, 0),
                'v' => (0, 1),
                '<' => (-1, 0),
            };

            while (true)
            {
                (int x, int y) nextPos = (currentX + currentDirection.x, currentY + currentDirection.y);
                if (nextPos.x < 0 || nextPos.x >= map[0].Length || nextPos.y < 0 || nextPos.y >= map.Length) break;

                if (map[nextPos.y][nextPos.x] == '#')
                {
                    currentDirection = (currentDirection.x, currentDirection.y) switch
                    {
                        (1, 0) => (0,1), //> -> v,
                        (0, -1) => (1,0), //^ -> '>',
                        (-1, 0) => (0,-1),//< -> '^',
                        (0, 1) => (-1,0)// v -> '<'
                    };    
                }
                else
                {
                    var key = (currentX, currentY, currentDirection.x, currentDirection.y);
                    if (visited.ContainsKey(key) && visited[key] >= 50
                    ) return (true, visited);
                    currentX = nextPos.x;
                    currentY = nextPos.y;
                }
                var k = (currentX, currentY, currentDirection.x,currentDirection.y);
                if (!visited.ContainsKey(k))
                    visited.Add(k, 1);
                else
                    visited[k]++;
            }
            return (false, visited);
        }

        public override string Part2()
        {
            var map = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
            (int startX, int startY) = map
                .SelectMany((row, y) => row.Select((ch, x) => (ch, x, y)))
                .Where(p => p.ch == '^')
                .Select(p => (p.x, p.y))
                .First();
            var loopCount = 0;
            
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[0].Length; x++)
                {
                    if (map[y][x] == '.')
                    {
                        var newMap = map.Select(y => y.Select(x => x).ToArray()).ToArray();
                        newMap[y][x] = '#';
                        var (loop, positions) = Walk(newMap, startX, startY);
                        if (loop) loopCount++;
                    }
                }
            }
            return loopCount.ToString();
        }
    }
}