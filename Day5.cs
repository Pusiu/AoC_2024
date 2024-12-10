namespace AoC_2024
{
    public class Day5 : Day
    {
        public Day5() { isTest = false; }
        public override string Part1()
        {
            var splitted = input.Split("\n\n").Select(x => x.Split("\n")).ToList();
            var rules = splitted[0].Select(x =>
            {
                var parts = x.Split("|");
                return (int.Parse(parts[0]), int.Parse(parts[1]));
            }
            ).ToList();
            var groups = splitted[1].Select(x => x.Split(",").Select(x => int.Parse(x)).ToList()).ToList();

            List<int> validMiddles = new List<int>();
            foreach (var group in groups)
            {
                List<int> printingQueue = new(group);
                var isValid = true;
                for (int i = 0; i < group.Count; i++)
                {
                    isValid = CanBePrinted(printingQueue, rules, i);
                    if (!isValid) break;
                }
                if (isValid)
                {
                    validMiddles.Add(printingQueue[(printingQueue.Count - 1) / 2]);
                }
            }

            return validMiddles.Sum().ToString();
        }

        bool CanBePrinted(List<int> printingUpdate, List<(int left, int right)> rules, int index)
        {
            int pageNumber = printingUpdate[index];
            var pagesBeforeRuleset = rules.Where(x => x.right == pageNumber && printingUpdate.Contains(x.left)).ToList();
            var pagesAfterRuleset = rules.Where(x => x.left == pageNumber && printingUpdate.Contains(x.right)).ToList();
            if (printingUpdate.Skip(index + 1).All(otherPage => pagesAfterRuleset.Any(y => y.right == otherPage)))
            {
                return true;
            }
            foreach (var (left, right) in pagesBeforeRuleset)
            {
                if (!printingUpdate.Take(index).Contains(left))
                {
                    return false;
                };
            }
            return true;
        }

        void Reorder(List<int> printingUpdate, List<(int left, int right)> rules)
        {
            bool valid = false;
            while (!valid)
            {
                valid = true;
                for (int i = 0; i < printingUpdate.Count; i++)
                {
                    if (!CanBePrinted(printingUpdate, rules, i))
                    {
                        valid = false;
                        for (int j = i + 1; j < printingUpdate.Count; j++)
                        {
                            if (CanBePrinted(printingUpdate, rules, j))
                            {
                                int temp = printingUpdate[i];
                                printingUpdate.RemoveAt(i);
                                printingUpdate.Insert(j, temp);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public override string Part2()
        {
            var splitted = input.Split("\n\n").Select(x => x.Split("\n")).ToList();
            var rules = splitted[0].Select(x =>
            {
                var parts = x.Split("|");
                return (int.Parse(parts[0]), int.Parse(parts[1]));
            }
            ).ToList();
            var groups = splitted[1].Select(x => x.Split(",").Select(x => int.Parse(x)).ToList()).ToList();

            List<List<int>> invalid = new();
            foreach (var group in groups)
            {
                List<int> printingQueue = new(group);
                var isValid = true;
                for (int i = 0; i < group.Count; i++)
                {
                    isValid = CanBePrinted(printingQueue, rules, i);
                    if (!isValid) break;
                }
                if (!isValid)
                {
                    invalid.Add(printingQueue);
                }

            }
            foreach (var inv in invalid)
            {
                Reorder(inv, rules);
            }
            return invalid.Select(x => x[x.Count/2]).Sum().ToString();
        }
    }
}