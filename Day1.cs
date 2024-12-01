using System.Linq;

namespace AoC_2024
{
    public class Day1 : Day
    {
        public Day1() {isTest = false;}
        public override string Part1()
        {
            var (leftColumn, rightColumn) = input
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .Aggregate((left: new List<int>(), right: new List<int>()), (acc, pair) => {
                acc.left.Add(pair[0]);
                acc.right.Add(pair[1]);
                return acc;
            });
            return leftColumn.OrderBy(x=>x).Zip(rightColumn.OrderBy(x=>x)).Aggregate(0, (acc, x) =>
            {
                return acc + Math.Abs(x.First - x.Second);
            }).ToString();
        }

        public override string Part2()
        {
            var (leftColumn, rightColumn) = input
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .Aggregate((left: new List<int>(), right: new List<int>()), (acc, pair) => {
                acc.left.Add(pair[0]);
                acc.right.Add(pair[1]);
                return acc;
            });

            return leftColumn.Aggregate(0, (acc, x) =>
            {
                return acc + (x*rightColumn.Count(y => y == x));
            }).ToString();
        }
    }
}