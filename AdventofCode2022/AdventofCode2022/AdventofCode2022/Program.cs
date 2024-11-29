
FileStream file = File.OpenRead("2022_Day1.txt");

StreamReader sr = new StreamReader(file);

int caloriesPerElf = 0;
string? line;
int maxCalories = 0;

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
            if (caloriesPerElf > maxCalories)
            {
                maxCalories = caloriesPerElf;
            }
            caloriesPerElf = 0;
        }
    }
    else { break; }
}

Console.WriteLine(maxCalories);
