string[] lines = File.ReadAllLines("input.txt");

long time = long.Parse(string.Join("", lines[0].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x))));
long recordDist = long.Parse(string.Join("", lines[1].Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x))));

long counter = 0;

for (long speed = 0; speed < time; speed++)
{
    long remainingTime = time - speed;
    long distance = remainingTime * speed;

    if (distance > recordDist)
    {
        counter++;
    }
}

Console.WriteLine(counter);