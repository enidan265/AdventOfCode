string[] lines = File.ReadAllLines("input.txt");
int result = 0;

//Hands auslesen
List<Hand> hands = new List<Hand>();
foreach (string line in lines)
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
    public Dictionary<char, char> StrengthCardsJoker = new()
    { {'J', 'A' }, {'2', 'B' }, {'3', 'C'}, {'4', 'D'}, {'5', 'E'}, {'6', 'F'}, {'7', 'G'}, {'8', 'H'}, {'9', 'I'}, {'T', 'J'}, {'Q', 'K'}, {'K', 'L'}, {'A', 'M'}};
    public string CardsStrength { get; set; }
    public int Bid { get; set; }
    public Handtype Handtype { get; set; }
    public Handtype HandtypeJoker { get; set; }
    public int Rank { get; set; }

    public Hand(string line)
    {
        Cards = line.Split(' ')[0];
        CardsStrength = SetCardStrength(Cards);
        Bid = Convert.ToInt32(line.Split(' ')[1]);
        Handtype = SetHandtype(Cards);
        HandtypeJoker = SetHandtypeJoker(Cards, Handtype);
    }

    private string SetCardStrength(string cards)
    {
        string strengthCard = "";
        foreach (char c in cards)
        {
            strengthCard += StrengthCardsJoker[c];
        }
        return strengthCard;
    }
    private Handtype SetHandtype(string cards)
    {
        char[] distinctvalues = cards.Distinct().ToArray();

        return distinctvalues.Length switch
        {
            1 => Handtype.FiveOfKind,
            2 when (cards.Count(x => x == distinctvalues[0]) == 3 || cards.Count(x => x == distinctvalues[1]) == 3) => Handtype.FullHouse,
            2 => Handtype.FourOfKind,
            3 when (cards.Count(x => x == distinctvalues[0]) == 3 || cards.Count(x => x == distinctvalues[1]) == 3 || cards.Count(x => x == distinctvalues[2]) == 3) => Handtype.ThreeOfKind,
            3 => Handtype.TwoPair,
            4 => Handtype.OnePair,
            _ => Handtype.HighCard,
        };
    }

    private Handtype SetHandtypeJoker(string cards, Handtype handtype)
    {
        if (cards.Contains('J'))
        {
            return handtype switch
            {
                Handtype.HighCard => Handtype.OnePair,
                Handtype.OnePair => Handtype.ThreeOfKind,
                Handtype.TwoPair when (cards.Count(x => x == 'J') == 2) => Handtype.FourOfKind,
                Handtype.TwoPair when (cards.Count(x => x == 'J') == 1) => Handtype.FullHouse,
                Handtype.ThreeOfKind => Handtype.FourOfKind,
                _ => Handtype.FiveOfKind,
            };
        }
        else { return handtype; }
    }

    public int CompareTo(Hand other)
    {
        if (HandtypeJoker != other.HandtypeJoker)
        {
            return HandtypeJoker.CompareTo(other.HandtypeJoker);
        }
        else
        {
            return CardsStrength.CompareTo(other.CardsStrength);
        }
    }
}

enum Handtype { HighCard, OnePair, TwoPair, ThreeOfKind, FullHouse, FourOfKind, FiveOfKind }