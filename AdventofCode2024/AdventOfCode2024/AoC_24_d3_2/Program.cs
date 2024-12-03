
using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

Regex multiplierPattern = new Regex(@"mul\(\d?\d?\d,\d?\d?\d\)");
MatchCollection multiplierMatches = multiplierPattern.Matches(input);

Regex doInstructionPattern = new Regex(@"do\(\)");
MatchCollection doInstructionMatches = doInstructionPattern.Matches(input);

Regex dontInstructionPattern = new Regex(@"don't\(\)");
MatchCollection dontInstructionMatches = dontInstructionPattern.Matches(input);

int result = 0;

foreach (Match match in multiplierMatches)
{
    int[] nums = FindNumbers(match);
    if (IsMultiplierEnabled(match))
    {
        int mul = nums[0] * nums[1];
        result += mul;
    }

}

Console.WriteLine(result);



bool IsMultiplierEnabled(Match match)
{
    bool isMultiplierEnabled = false;
    int matchIndex = match.Index;
    int doIndex = 0;
    int dontIndex = 0; 

    foreach(Match doMatch in doInstructionMatches)
    {
        if(doMatch.Index < match.Index)
        {
            doIndex = doMatch.Index;
        }
    }

    foreach (Match dontMatch in dontInstructionMatches)
    {
        if (dontMatch.Index < match.Index)
        {
            dontIndex = dontMatch.Index;
        }
    }

    if(doIndex > dontIndex || (doIndex == 0 && dontIndex == 0))
    {
        isMultiplierEnabled = true;
    }

    return isMultiplierEnabled;
}


int[] FindNumbers(Match match)
{
    // mul(111,111)
    int[] nums = new int[2];
    nums[0] = int.Parse(match.Value.Split(',')[0].Split('(')[1]);
    nums[1] = int.Parse(match.Value.Split(',')[1].TrimEnd(')'));

    return nums;
}