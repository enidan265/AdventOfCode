string[] lines = File.ReadAllLines("input.txt");
int result = 0;

//Histories auslesen 
List<int[]> histories = new List<int[]>();
foreach (var line in lines)
{
    histories.Add(line.Split(' ').Select(Int32.Parse).ToArray());
}

foreach (var history in histories)
{
    result += SetFirstValue(SetHistoryList(history));
}

Console.WriteLine(result);

int SetFirstValue(List<int[]> historyList)
{
    int value = 0;

    for (int i = historyList.Count() - 1; i > 0; i--)
    {
        value = historyList[i - 1].First() - value;
    }
    return value;
}

List<int[]> SetHistoryList(int[] historyLine)
{
    List<int[]> HistoryList = new List<int[]>();
    HistoryList.Add(historyLine);

    int[] currentLine = HistoryList.Last();

    while (!IsAllZero(currentLine))
    {
        int[] extrapolate = new int[currentLine.Length - 1];
        for (int i = 0; i < extrapolate.Length; i++)
        {
            extrapolate[i] = currentLine[i + 1] - currentLine[i];
        }
        HistoryList.Add(extrapolate);
        currentLine = HistoryList.Last();
    }
    return HistoryList;

    bool IsAllZero(int[] extrapolate)
    {
        foreach (int i in extrapolate)
        {
            if (i != 0)
            {
                return false;
            }
        }
        return true;
    }
}

