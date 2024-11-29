FileStream file = File.OpenRead("2022_Day1.txt");

StreamReader sr = new StreamReader(file);

List<int> calories = new List<int>();

int caloriesPerElf = 0;
string? line;
int highestValue = 0;
int totalThree = 0;

while (true)
{
    line = sr.ReadLine();
    if (line != null)
    {
        if (line != String.Empty)
        {
            caloriesPerElf += Convert.ToInt32(line);
        }
        else
        {
            calories.Add(caloriesPerElf);
            caloriesPerElf = 0;
        }
    }
    else { break; }
}

for(int i = 0; i < 3; i++)
{
    highestValue = calories.Max();
    totalThree += highestValue;
    calories.Remove(highestValue);
}

Console.WriteLine(totalThree);





