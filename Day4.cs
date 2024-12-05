namespace AoC_2024
{
    public class Day4 : Day
    {
        public Day4() { isTest = false; }
        public override string Part1()
        {
            var arr = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var windows = new List<RollingWindow>
            {
                new(new List<(int, int)> {
                new (0, 0),
                new (1, 0),
                new (2, 0),
                new (3, 0),
            }),
            new(new List<(int, int)> {
                new (0, 0),
                new (0, 1),
                new (0, 2),
                new (0, 3),
            }),
                new(new List<(int, int)> {
                new (0, 0),
                new (1, 1),
                new (2, 2),
                new (3, 3),
            }),
                new(new List<(int, int)> {
                new (0, 0),
                new (-1, 1),
                new (-2, 2),
                new (-3, 3),
            })
            };            
            windows.AddRange(windows.Select(x => x.ReverseClone()).ToList());
            return arr.SelectMany(
                (el, y) => el.Select(
                    (el2, x) => windows.Count(
                        w => w.IsValid(arr, x, y))))
                .Sum()
                .ToString();
        }
        private class RollingWindow
        {
            private readonly string word;
            private readonly Dictionary<(int x, int y), char> map = new();
            public RollingWindow(List<(int, int)> s, string word = "XMAS")
            {
                this.word = word;
                for (int i = 0; i < word.Length; i++)
                {
                    map.Add(s[i], word[i]);
                }
            }
            public RollingWindow ReverseClone()
            {
                return new RollingWindow(map.Keys.ToList(), new string(word.Reverse().ToArray()));
            }

            public bool IsValid(string[] inputArray, int startX, int startY)
            {
                foreach (var (k, v) in map)
                {
                    var x = startX + k.x;
                    var y = startY + k.y;
                    if (y < 0 || y >= inputArray.Length || x < 0 || x >= inputArray[y].Length) return false;
                    if (inputArray[y][x] != v) return false;
                }
                return true;
            }
        }

        public override string Part2()
        {
            List<RollingWindow> windows = new()
            {
                new RollingWindow(new List<(int, int)>() {
                (-1,-1),
                (0,0),
                (1,1),
                }, "MAS"),
                new RollingWindow(new List<(int, int)>() {
                (-1,1),
                (0,0),
                (1,-1),
            }, "MAS")
            };
            windows.AddRange(windows.Select(x => x.ReverseClone()).ToList());
            var map = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            return map.SelectMany(
                (el, y) => el.Select(
                    (el2, x) => windows.Count(
                        w => w.IsValid(map, x, y)) >= 2 ? 1 : 0))
                .Sum()
                .ToString();
        }
    }
}