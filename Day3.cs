using System.Text.RegularExpressions;

namespace AoC_2024
{
    public class Day3 : Day
    {
        public Day3() {isTest = false;}
        public override string Part1()
        {
            return Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)")
            .Select(x => int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value))
            .Sum()
            .ToString();
        }

        public override string Part2()
        {
            bool doFlag=true;
            return Regex.Matches(input, @"((mul)\((\d{1,3}),(\d{1,3})\))|(do(?:n't)?)\(\)")
            .Select(x => new {
                Operation = string.IsNullOrEmpty(x.Groups[2].Value) ? x.Groups[5].Value : x.Groups[2].Value,
                Value = x.Groups[2].Value == "mul" ? int.Parse(x.Groups[3].Value) * int.Parse(x.Groups[4].Value) : (null as int?)
                })
            .Aggregate(0, (acc, x) => {
                switch (x.Operation) {
                    case "mul":
                        return doFlag ? acc+(x.Value ?? 0) : acc;
                    case "do":
                    doFlag=true; return acc;
                    case "don't":
                    default:
                    doFlag=false; return acc;
                }
            })
            .ToString();
            
        }
    }
}