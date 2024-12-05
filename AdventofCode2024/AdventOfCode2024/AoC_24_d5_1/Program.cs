
FileStream fileStream = File.OpenRead("input_test.txt");
StreamReader streamReader = new StreamReader(fileStream);

List<int[]> rules = new List<int[]>();
List<int[]> updates = new List<int[]>();

while (true)
{
    string line = streamReader.ReadLine();

    if (line == null) break;
    else if (line.Contains('|'))
    {
        int[] ruleDigits = line.Split('|').Select(x => int.Parse(x)).ToArray(); 
        rules.Add(ruleDigits);
    }
    else if (line.Contains(','))
    {
        int[] updateDigits = line.Split(',').Select(x => int.Parse(x)).ToArray();
        updates.Add(updateDigits);
    }
}

foreach(var update in updates)
{

}