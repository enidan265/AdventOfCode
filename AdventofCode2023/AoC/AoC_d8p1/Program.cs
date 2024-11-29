string[] allLines = File.ReadAllLines("input.txt");
string directions = allLines[0];
string start = "AAA";
string end = "ZZZ";
int counter = 0;

List<Map> maps = new List<Map>();
foreach(string line in allLines)
{
    if (line.Contains('='))
    {
        Map map = new Map(line);
        maps.Add(map);
    }
}

while (start != end)
{
    foreach (char direction in directions)
    {
        foreach (Map map in maps)
        {
            if (map.Start == start)
            {
                start = direction switch
                {
                    'L' => map.Left,
                    _ => map.Right,
                };
                break;
            }
        }
        counter++;
    }
}

Console.WriteLine("Anzahl Durchgänge: " + counter);

class Map
{
    public string Start { get; set; }
    public string Left { get; set; }
    public string Right { get; set; }

    public Map(string line)
    {
        Start = line.Split(" ")[0];
        Left = line.Split(" ")[2].TrimStart('(').TrimEnd(',');
        Right = line.Split(" ")[3].TrimEnd(')');
    }
}

