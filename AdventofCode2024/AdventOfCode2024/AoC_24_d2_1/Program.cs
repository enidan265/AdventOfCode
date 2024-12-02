
var input = File.ReadAllLines("input.txt");

int counter = 0; 
foreach(string report in input)
{
    int[] levels = SetLevelsforReport(report);
    
    if (IsSafeReport(levels))
    {
        counter++;
    };
}

Console.WriteLine(counter);




bool IsSafeReport(int[] levels)
{
    bool safeLevel = true;

    // Decreasing?
    bool decreasing = true;
    for (int i = 0; i < levels.Length -1; i++)
    {
        if (levels[i] <= levels[i + 1])
        {
            decreasing = false; break;
        }
    }

    // Increasing?
    bool increasing = true;
    for (int i = 0; i < levels.Length -1; i++)
    {
        if (levels[i] >= levels[i + 1])
        {
            increasing = false; break;
        }
    }

    // Difference between two levels
    if(decreasing || increasing)
    {
        for (int i = 0; i < levels.Length -1; i++)
        {
            if ((Math.Abs(levels[i] - levels[i + 1])) > 3)
            {
                safeLevel = false;
                break;
            }
        }
    }
    else
    {
        safeLevel = false;
    }

    return safeLevel;
}





int[] SetLevelsforReport(string report)
{
   
    var levelStrings = report.Split(' ');
    int[] levels = new int[levelStrings.Length];
    for(int i = 0; i < levelStrings.Length; i++)
    {
        levels[i] = Convert.ToInt32(levelStrings[i]);
    }
    return levels;
}

