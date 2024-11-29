string[] lines = File.ReadAllLines("input.txt");

//seeds auslesen
long[] seeds = lines[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToArray();

//maps auslesen
List<Map> maps = ReadMaps(lines);

//Liste für Ergebnisse
List<long> locations = new List<long>();


foreach(long seed in seeds)
{
    long source = seed;
    long destination = 0;

    foreach (Map map in maps)
    {
        foreach (Range range in map.Ranges)
        {
            if (range.SourceStart <= source && source <= range.SourceEnd)
            {
                destination = source + range.Offset;
                break;
            }
            else
            {
                destination = source;
            }
        }
        source = destination;
    }
    locations.Add(destination);
}

Console.WriteLine(locations.Min());


//Methoden
List<Map> ReadMaps(string[] lines)
{
    List<Map> maps = new List<Map>();

    for (int i = 0; i < lines.Length; i++)
    {
        string line = lines[i];
        if (line.EndsWith(":"))
        {
            Map map = new Map();
            maps.Add(map);
            continue;
        }
        else if (line != "" && Char.IsDigit(line[0]))
        {
            Range range = new Range(line);
            maps.Last<Map>().Ranges.Add(range);
            continue;
        }
    }
    return maps;
}

//Klassen
class Map
{
    public List<Range> Ranges = new List<Range>();
}

class Range
{
    public long SourceStart { get; set; }
    public long SourceEnd { get; set; }
    public long Offset { get; set; }

    public Range(string line)
    {
        long[] infos = line.Split(' ').Select(long.Parse).ToArray();

        SourceStart = infos[1];
        SourceEnd = infos[1] + infos[2];
        Offset = infos[0] - infos[1];
    }

}

