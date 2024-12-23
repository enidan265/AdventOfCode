﻿
FileStream fileStream = File.OpenRead("input.txt");
StreamReader streamReader = new StreamReader(fileStream);

List<int[]> rules = new List<int[]>();
List<int[]> updates = new List<int[]>();

// ReadLines in Lists
while (true)
{
    string? line = streamReader.ReadLine();

    if (line == null) break;
    else if (line.Contains('|'))
    {
        int[] ruleDigits = line.Split('|').Select(x => int.Parse(x)).ToArray(); 
        rules.Add(ruleDigits);
    }
    else if (line.Contains(','))
    {
        int[] updateDigits = line.Split(',').Select(x => int.Parse(x)).ToArray();
        updates.Add(updateDigits);
    }
}


int sumMiddleNums = 0; 

foreach(var update in updates)
{
    bool validUpdate = false;
    foreach (int page in update)
    {
        foreach(var rule in rules)
        {
            if (rule[0] == page && update.Contains(rule[1]))
            {
                validUpdate = CheckCorrectOrder(page, rule, update, 0);
                if (!validUpdate) break;
            }
            else if (rule[1] == page && update.Contains(rule[1]))
            {
                validUpdate = CheckCorrectOrder(page, rule, update, 1);
                if (!validUpdate) break;
            }
        }
        if (!validUpdate) break;
    }
    if (validUpdate)
    {
        sumMiddleNums += update[update.Length / 2];
    }
}

Console.WriteLine(sumMiddleNums);


bool CheckCorrectOrder(int page, int[]rule, int[] update, int positionPageInRule)
{
    int indexPage = Array.IndexOf(update, page);
    
    if (positionPageInRule == 0)
    {
        int indexRule = Array.IndexOf(update, rule[1]);
        return indexPage < indexRule;
    }
    else if (positionPageInRule == 1)
    {
        int indexRule = Array.IndexOf(update, rule[0]);
        return indexPage > indexRule;
    }

    return false;
}