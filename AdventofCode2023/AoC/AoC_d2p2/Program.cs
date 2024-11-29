//Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
string[] lines = File.ReadAllLines("input.txt");
int result = 0;

foreach (string line in lines)
{
    int gameSetCounts = SetGameSetCount(line);
    int minRed = 0;
    int minGreen = 0;
    int minBlue = 0;

    for (int i = 0; i < gameSetCounts; i++)
    {
        GameSet gameSet = new GameSet(line, i); //GameSets auslesen
        if(minRed < gameSet.CountRed) { minRed = gameSet.CountRed; }
        if (minGreen < gameSet.CountGreen) { minGreen = gameSet.CountGreen; }
        if (minBlue < gameSet.CountBlue) { minBlue = gameSet.CountBlue; }
    }
    result += minRed * minGreen * minBlue;
}

Console.WriteLine(result);

int SetGameSetCount(string line)
{
    return (line.Split(':')[1].Split(';')).Length;
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
        foreach (string cube in cubes)
        {
            if (cube.Contains(color))
            {
                foreach (char c in cube)
                {
                    if (char.IsDigit(c))
                    {
                        amount += c;
                    }
                }
            }
        }
        if (amount != "")
        {
            return Convert.ToInt32(amount);
        }
        else { return 0; }
    }

}