//Inhalt des Beutels
int maxRed = 12;
int maxGreen = 13;
int maxBlue = 14;

//Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
string[] lines = File.ReadAllLines("input.txt");

int result = 0;

foreach(string line in lines)
{
    int gameID = SetGameID(line);
    int gameSetCounts = SetGameSetCount(line);
    bool gamePossible = true;

    for(int i = 0; i < gameSetCounts; i++)
    {
        GameSet gameSet = new GameSet(line, i); //GameSets auslesen
        gamePossible = GamePossible(maxRed, maxGreen, maxBlue, gameSet); //Prüfung durchführen
        if(!gamePossible)
        {
            break;
        }
    }
    if (gamePossible)
    {
        result += gameID;
    }
}

Console.WriteLine(result);

int SetGameID(string line)
{
    return Convert.ToInt32(line.Split(':')[0].Split(' ')[1]);
}

int SetGameSetCount(string line)
{
    return (line.Split(':')[1].Split(';')).Length;
}

bool GamePossible(int maxRed, int maxGreen, int maxBlue, GameSet gameSet)
{
    if(gameSet.CountRed > maxRed || gameSet.CountGreen > maxGreen || gameSet.CountBlue > maxBlue)
    {
        return false;
    }
    else { return true; }
}

class GameSet
{
    public int GameSetID { get; set; }
    public int CountRed { get; set; }
    public int CountGreen { get; set; }
    public int CountBlue { get; set; }

    public GameSet(string line, int i)
    {
        GameSetID = i;
        CountRed = SetColorCounts(line, "red");
        CountGreen = SetColorCounts(line, "green");
        CountBlue = SetColorCounts(line, "blue");
    }

    private int SetColorCounts(string line, string color)
    {
        string amount = "";
        string[] cubes = line.Split(':')[1].Split(';')[GameSetID].Split(',');
        foreach(string cube in cubes)
        {
            if (cube.Contains(color))
            {
                foreach(char c in cube)
                {
                    if (char.IsDigit(c))
                    {
                        amount += c;
                    }
                }
            }
        }
        if(amount != "")
        {
            return Convert.ToInt32(amount);
        }
        else { return 0; }
    }

}