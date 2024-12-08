
// Funktioniert noch nicht, testinput klappt, echter input nicht 

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

foreach (var update in updates)
{
    bool validUpdate = false;
    int[] sortedUpdate = new int[update.Length];
    foreach (int page in update)
    {
        foreach (var rule in rules)
        {
            if (rule[0] == page && update.Contains(rule[1]))
            {
                if (!CheckCorrectOrder(page, rule, update, 0))
                {
                    sortedUpdate = TopologicalSort(update);
                    validUpdate = true;
                    break;
                }    
            }
            else if (rule[1] == page && update.Contains(rule[0]))
            {
                if (!CheckCorrectOrder(page, rule, update, 1))
                {
                    sortedUpdate = TopologicalSort(update);
                    validUpdate = true;
                    break;
                }
            }
        }
        if (validUpdate)
        {
            break;
        }
    }
    if (validUpdate)
    {
        int middleNums = sortedUpdate[sortedUpdate.Length / 2];
        sumMiddleNums += sortedUpdate[sortedUpdate.Length / 2];
    }
}

Console.WriteLine(sumMiddleNums);

int[] TopologicalSort(int[] update)
{
    var graph = new Dictionary<int, List<int>>();
    var inDegree = new Dictionary<int, int>();

    // Initialisiere den Graph und In-Degree-Map
    foreach (var page in update)
    {
        graph[page] = new List<int>();
        inDegree[page] = 0;
    }

    // Baue den Graph basierend auf den Regeln
    foreach (var rule in rules)
    {
        if (graph.ContainsKey(rule[0]) && graph.ContainsKey(rule[1]))
        {
            graph[rule[0]].Add(rule[1]);
            inDegree[rule[1]]++;
        }
    }

    // Knoten mit In-Degree 0 finden
    var queue = new Queue<int>(inDegree.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key));
    var sorted = new List<int>();

    // Knoten in topologischer Reihenfolge sortieren
    while (queue.Count > 0)
    {
        var node = queue.Dequeue();
        sorted.Add(node);

        foreach (var neighbor in graph[node])
        {
            inDegree[neighbor]--;
            if (inDegree[neighbor] == 0) queue.Enqueue(neighbor);
        }
    }

    return sorted.ToArray(); 
}







bool CheckCorrectOrder(int page, int[] rule, int[] update, int positionPageInRule)
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

