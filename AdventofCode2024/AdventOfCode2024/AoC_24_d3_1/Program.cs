
using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

Regex pattern = new Regex(@"mul\(\d?\d?\d,\d?\d?\d\)");

MatchCollection matches = pattern.Matches(input);

int result = 0; 

foreach(Match match in matches)
{
    int[] nums = FindNumbers(match);
    int mul = nums[0] * nums[1];
    result += mul;
}

Console.WriteLine(result);


int[] FindNumbers(Match match)
{
    // mul(111,111)
    int[] nums = new int[2];
    nums[0] = int.Parse(match.Value.Split(',')[0].Split('(')[1]);
    nums[1] = int.Parse(match.Value.Split(',')[1].TrimEnd(')'));

    return nums; 
}