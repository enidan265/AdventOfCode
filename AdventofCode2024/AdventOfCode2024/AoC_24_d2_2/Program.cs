
var input = File.ReadAllLines("input.txt");

int counter = 0;
foreach (string report in input)
{
    List<int> levels = SetLevelsforReport(report);
    bool safeLevel = IsSafeReport(levels);

    if (!safeLevel)
    {
        safeLevel = ProblemDampener(levels);
    }

    if (safeLevel)
    {
        counter++;
    };
}

Console.WriteLine(counter);



bool ProblemDampener(List<int> levels)
{
    bool safeLevel = false;

    for (int i = 0; i < levels.Count; i++)
    {
        List<int> levelsDamped = new List<int>(levels);
        levelsDamped.RemoveAt(i);

        if (IsSafeReport(levelsDamped))
        {
            safeLevel = true;
            break;
        };
    }

    return safeLevel;
}


bool IsSafeReport(List<int> levels)
{
    bool safeLevel = true;

    // Decreasing?
    bool isDecreasing = true;
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (levels[i] <= levels[i + 1])
        {
            isDecreasing = false; break;
        }
    }

    // Increasing?
    bool isIncreasing = true;
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (levels[i] >= levels[i + 1])
        {
            isIncreasing = false; break;
        }
    }

    // Difference between two levels
    if (isDecreasing || isIncreasing)
    {
        for (int i = 0; i < levels.Count - 1; i++)
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

List<int> SetLevelsforReport(string report)
{
    var levelStrings = report.Split(' ');
    List<int> levels = new List<int>();
    for (int i = 0; i < levelStrings.Length; i++)
    {
        levels.Add(Convert.ToInt32(levelStrings[i]));
    }
    return levels;
}
