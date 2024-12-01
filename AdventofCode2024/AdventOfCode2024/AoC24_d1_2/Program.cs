
string[] input = File.ReadAllLines("input.txt");
List<int> leftList = new List<int>();
List<int> rightList = new List<int>();
int result = 0;

foreach (var line in input)
{
    string[] splitLine = line.Split(' ');
    leftList.Add(Convert.ToInt32(splitLine[0]));
    rightList.Add(Convert.ToInt32(splitLine[3]));
}

result = 0; 

foreach(int leftNum in leftList)
{
    int counter = 0; 
    foreach(int rightNum in rightList)
    {
        if(leftNum == rightNum)
        {
            counter++;
        }
    }

    result += leftNum * counter;
}

Console.WriteLine(result);