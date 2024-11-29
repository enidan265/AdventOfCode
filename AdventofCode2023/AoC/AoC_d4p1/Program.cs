string[] lines = File.ReadAllLines("input.txt");
int result = 0;

foreach (string line in lines)
{
    Card card = new Card(line);
    result += card.Points;
}

Console.WriteLine(result);


public class Card
{
    public int ID { get; set; }
    public List<int> WinningNumbers { get; set; }
    public List<int> myNumbers { get; set; }
    public List<int> MatchingNumbers { get; set; }
    public int Points { get; set; }

    public Card(string line)
    {
        ID = SetID(line);
        WinningNumbers = AddWinningNumbers(line);
        myNumbers = AddMyNumbers(line);
        MatchingNumbers = FindMatchingNumbers(WinningNumbers, myNumbers);
        Points = CalculatePoints(MatchingNumbers);
    }

    private int SetID(string line)
    {
        return Convert.ToInt32(line.Split(':')[0].Split(' ').Last());
    }
    private List<int> AddWinningNumbers(string line)
    {
        List<int> winningNumbersList = new List<int>();
        string[] winningNumbersStr = line.Split(':')[1].Split('|')[0].Trim().Split(' ');
        foreach (string winningNumberStr in winningNumbersStr)
        {
            if (winningNumberStr != "")
            {
                winningNumbersList.Add(Convert.ToInt32(winningNumberStr));
            }
        }
        return winningNumbersList;
    }
    private List<int> AddMyNumbers(string line)
    {
        List<int> myNumbersList = new List<int>();
        string[] myNumbersStr = line.Split(':')[1].Split('|')[1].Trim().Split(' ');
        foreach (string myNumberStr in myNumbersStr)
        {
            if (myNumberStr != "")
            {
                myNumbersList.Add(Convert.ToInt32(myNumberStr));
            }
        }
        return myNumbersList;
    }
    private List<int> FindMatchingNumbers(List<int> winningNumbers, List<int> myNumbers)
    {
        List<int> matchingNumbersList = new List<int>();

        foreach (int winningNumber in winningNumbers)
        {
            if (myNumbers.Contains(winningNumber))
            {
                matchingNumbersList.Add(winningNumber);
            }
        }
        return matchingNumbersList;
    }
    private int CalculatePoints(List<int> matchingNumbers)
    {
        double counts = Convert.ToDouble(matchingNumbers.Count - 1);
        int points = Convert.ToInt32(Math.Pow(2, counts));

        return points;
    }
}