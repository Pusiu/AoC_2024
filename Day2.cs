namespace AoC_2024
{
    public class Day2 : Day
    {
        public Day2() {isTest = false;}
        public override string Part1()
        {
            var reports = input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(" ").Select(int.Parse).ToList());
            return reports.Count(x => IsIncreasingOrDecreasing(x) && IsValidDifference(x)).ToString();
        }

        private static bool IsIncreasingOrDecreasing(List<int> list)
        {
            bool increasing = list[1]-list[0] > 0;
            return list.Zip(list.Skip(1), (a, b) => (increasing ? 1 : -1)*a.CompareTo(b) < 0)
            .All(b => b);
        }

        private static bool IsValidDifference(List<int> list)
        {
            return list.Zip(
            list.Skip(1), 
            (a, b) => new List<int>(){1,2,3}.Contains(Math.Abs(b-a)))
            .All(b => b);
        }

        public override string Part2()
        {
            return input.Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(" ").Select(int.Parse).ToList())
            .Count(report => {
                bool v = IsValidDifference(report) && IsIncreasingOrDecreasing(report);
                return v ? v :
                    report.Select((x,i) => i).Any(x => {
                        var r = new List<int>(report);
                        r.RemoveAt(x);
                        return IsValidDifference(r) && IsIncreasingOrDecreasing(r);
                    });
                
            }).ToString();
        }
    }
}