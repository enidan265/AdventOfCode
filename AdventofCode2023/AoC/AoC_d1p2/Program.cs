
Dictionary<string, char> map = new Dictionary<string, char>();
map.Add("one", '1');
map.Add("two", '2');
map.Add("three", '3');
map.Add("four", '4');
map.Add("five", '5');
map.Add("six", '6');
map.Add("seven", '7');
map.Add("eight", '8');
map.Add("nine", '9');


string[] lines = File.ReadAllLines("input.txt");

char firstChar = '0';
char lastChar = '0';

int result = 0;

foreach(string line in lines)
{
    int firstIndex = int.MaxValue;
    int lastIndex = 0;

    for (int i = 0; i< line.Length; i++) //von vorne durchsuchen
    {
        if (Char.IsDigit(line[i]))
        {
            firstIndex = i;
            firstChar = line[i];
            break;
        }
    }
    
    for(int i = line.Length - 1; i >= 0; i--) // von hinten durchsuchen
    {
        if (Char.IsDigit(line[i]))
        {
            lastIndex = i;
            lastChar = line[i];
            break;
        }
    }

    foreach(var item in map)
    {
        if (line.Contains(item.Key) && firstIndex > line.IndexOf(item.Key))
        {
            firstIndex = line.IndexOf(item.Key);
            firstChar = item.Value;
        }

        if(line.Contains(item.Key) && lastIndex < line.LastIndexOf(item.Key))
        {
            lastIndex = line.LastIndexOf(item.Key);
            lastChar = item.Value;
        }
    }

    int resultLine = Convert.ToInt32(String.Concat(firstChar, lastChar));
    result += resultLine;
}

Console.WriteLine("RESULT: " + result);

