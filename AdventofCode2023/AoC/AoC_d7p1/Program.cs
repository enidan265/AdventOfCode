string[] lines = File.ReadAllLines("input.txt");
int result = 0;

//Hands auslesen
List<Hand> hands = new List<Hand>();
foreach(string line in lines)
{
    hands.Add(new Hand(line));
}

//Hands sortieren
hands.Sort();

//Set Rank & Ergebnis ermitteln
foreach (Hand hand in hands)
{
    hand.Rank = hands.IndexOf(hand) + 1;
    result += hand.Bid * hand.Rank;
}

Console.WriteLine(result);

class Hand : IComparable<Hand>
{
    public string Cards { get; set; }
    public Dictionary<char, char> StrengthCards = new()
    { {'2', 'A' }, {'3', 'B'}, {'4', 'C'}, {'5', 'D'}, {'6', 'E'}, {'7', 'F'}, {'8', 'G'}, {'9', 'H'}, {'T', 'I'}, {'J', 'J'}, {'Q', 'K'}, {'K', 'L'}, {'A', 'M'}};
    public string CardsStrength { get; set; }
    public int Bid { get; set; }
    public Handtype Handtype { get; set; }
    public int Rank { get; set; }

    public Hand(string line)
    {
        Cards = line.Split(' ')[0];
        CardsStrength = SetCardStrength(Cards);
        Bid = Convert.ToInt32(line.Split(' ')[1]);
        Handtype = SetHandtype(Cards);
    }

    private string SetCardStrength(string cards)
    {
        string strengthCard = "";
        foreach (char c in cards)
        {
            strengthCard += StrengthCards[c];
        }
        return strengthCard;
    }
    private Handtype SetHandtype(string cards)
    {
        char[] distinctvalues = cards.Distinct().ToArray();

       return distinctvalues.Length switch
        {
            1 => Handtype.FiveOfKind,
            2 when (cards.Count(f => f == distinctvalues[0]) == 3 || cards.Count(f => f == distinctvalues[1]) == 3) => Handtype.FullHouse,
            2 => Handtype.FourOfKind,
            3 when (cards.Count(f => f == distinctvalues[0]) == 3 || cards.Count(f => f == distinctvalues[1]) == 3 || cards.Count(f => f == distinctvalues[2]) == 3) => Handtype.ThreeOfKind,
            3 => Handtype.TwoPair,
            4 => Handtype.OnePair,
            _ => Handtype.HighCard,
        };
    }
    public int CompareTo(Hand other)
    {
        if (Handtype != other.Handtype)
        {
            return Handtype.CompareTo(other.Handtype);
        }
        else
        {
            return CardsStrength.CompareTo(other.CardsStrength);
        }
    }
}

enum Handtype { HighCard, OnePair, TwoPair, ThreeOfKind, FullHouse, FourOfKind, FiveOfKind }