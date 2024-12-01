
string[] input = File.ReadAllLines("input.txt");
List<int> leftList = new List<int>();
List<int> rightList = new List<int>();
int result = 0;

foreach(var line in input)
{
    string[] splitLine =  line.Split(' ');
    leftList.Add(Convert.ToInt32(splitLine[0]));
    rightList.Add(Convert.ToInt32(splitLine[3]));
}

leftList.Sort();
rightList.Sort();

for(int i = 0; i < leftList.Count; i++)
{
    result += Math.Abs(leftList[i] - rightList[i]);
}

Console.WriteLine(result);