string[] lines = File.ReadAllLines("input.txt");
int result = 0;

List<Card> cards = new List<Card>();
foreach (string line in lines)
{
    Card card = new Card(line);
    cards.Add(card);
}

foreach (Card card in cards)
{
    for (int i = 0; i < card.CopieCounts; i++) //Durchläufe für alle Kopien der Karte
    {
        for (int j = 1; j <= card.MatchingNumbers.Count; j++) //Kopien hochzählen
        {
            int index = cards.IndexOf(card);

            cards[index + j].CopieCounts++;
        }
    }
}


foreach (Card card in cards)
{
    result += card.CopieCounts;
}

Console.WriteLine(result);


public class Card
{
    public int ID { get; set; }
    public List<int> WinningNumbers { get; set; }
    public List<int> MyNumbers { get; set; }
    public List<int> MatchingNumbers { get; set; }
    public int CopieCounts { get; set; }

    public Card(string line)
    {
        ID = SetID(line);
        WinningNumbers = AddWinningNumbers(line);
        MyNumbers = AddMyNumbers(line);
        MatchingNumbers = FindMatchingNumbers(WinningNumbers, MyNumbers);
        CopieCounts = 1;
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
}