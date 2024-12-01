using System.Reflection;
using System.Text.RegularExpressions;
namespace AoC_2024
{
    class Program
    {
        static void Main(string[] args)
        {
            var days = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(Day).IsAssignableFrom(x) && !x.IsAbstract).Select(x => Activator.CreateInstance(x) as Day).OrderBy(x => int.Parse(Regex.Match(x.ToString(), @"\d+$").Value)).ToList();
            days[^1].Run();            
        }
    }
}