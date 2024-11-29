string[] lines = File.ReadAllLines("input.txt");

int[] times = lines[0].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToArray();
int[] distances = lines[1].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToArray();

Dictionary<int, int> races = new Dictionary<int, int>();
for (int i = 0; i < times.Length; i++)
{
    races.Add(distances[i], times[i]);
}

List<int> waysToWin = new List<int>();

foreach (var race in races)
{
    int counter = 0;
    for (int speed = 0; speed < race.Value; speed++)
    {
        int remainingTime = race.Value - speed;
        int distance = remainingTime * speed;

        if (distance > race.Key)
        {
            counter++;
        }
    }
    waysToWin.Add(counter);
}

Console.WriteLine($"margin of error: {waysToWin.Aggregate((x, y) => x * y)}");