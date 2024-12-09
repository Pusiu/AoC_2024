using System.Text.RegularExpressions;

namespace AoC_2024
{
    public class Day7 : Day
    {
        public Day7() {isTest = false;}
        public override string Part1()
        {
            return input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => {
                var split = x.Split(":");                
                return new {
                    Result = long.Parse(split[0]),
                    Operands = Regex.Matches(split[1], @"\d+").Select(k => long.Parse(k.Value)).ToList()
                };                
                }
            ).Select(x => {
                var op = string.Join(" ", x.Operands);
                var results = CalculateRecursed(op);
                return results.Where(y => y == x.Result).FirstOrDefault();
            }).Sum().ToString();
        }

        List<string> GetCombinations(string operation, bool part2=false)
        {
            var list = new List<string>();
            var m = Regex.Matches(operation, @"\d+");
            if (m.Count == 1)
            {
                return new() {m[0].Value};
            }
            var left = m[0].Value;
            var right = string.Join(",", m.Skip(1));
            var combs = GetCombinations(right, part2);
            foreach (var comb in combs)
            {
                list.Add($"{left}+{comb}");
                list.Add($"{left}*{comb}");
                if (part2)
                {
                    list.Add($"{left}|{comb}");
                }
            }

            return list;
        }

        List<long> CalculateRecursed(string operation, bool part2=false)
        {
            var list = new List<long>();
            var combs = GetCombinations(operation, part2);
            foreach (var comb in combs)
            {
                long acc=0;
                string? op = null;
                long? left = null;
                var m = Regex.Matches(comb,@"\d+|\*|\+|\|");
                for (int i=0; i < m.Count; i++)
                {
                    if (m[i].Value == "*" || m[i].Value == "+" || m[i].Value == "|")
                    {
                        op=m[i].Value;
                    }
                    else
                    {
                        if (left == null)
                        {
                            left=long.Parse(m[i].Value);
                        }
                        else
                        {
                            var right = long.Parse(m[i].Value);
                            if (op == "+") acc=left.Value+right;
                            else if (op =="*") acc=left.Value*right;
                            else if (op == "|") {
                                acc=long.Parse(left.Value.ToString()+right.ToString());
                            }
                            left=acc;
                        }
                    }
                }
                list.Add(acc);
            }

            return list;
        }

        public override string Part2()
        {
            return input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => {
                var split = x.Split(":");                
                return new {
                    Result = long.Parse(split[0]),
                    Operands = Regex.Matches(split[1], @"\d+").Select(k => long.Parse(k.Value)).ToList()
                };                
                }
            ).Select(x => {
                var op = string.Join(" ", x.Operands);
                var results = CalculateRecursed(op, true);
                return results.Where(y => y == x.Result).FirstOrDefault();
            }).Sum().ToString();}
    }
}