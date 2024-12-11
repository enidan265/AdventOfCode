
string input = File.ReadAllText("input.txt");

List<long> nums = Array.ConvertAll(input.Split(' '), long.Parse).ToList(); ;

LinkedList<long> numbers = new LinkedList<long>(nums);

for (int i = 0; i < 25; i++)
{
    numbers = Blinking(numbers);
}

Console.WriteLine(numbers.Count());


LinkedList<long> Blinking(LinkedList<long> numbers)
{
    int currentCountNums = numbers.Count();
    for (int i = 0; i < currentCountNums; i++)
    {
        long secondNumber = -1;
        long newNumber = ApplyRules(numbers.Find(), out secondNumber);
        
        numbers[i] = newNumber;
        if (secondNumber >= 0)
        {
            numbers.Insert(i + 1, secondNumber);
            i++;
            currentCountNums++;
        }
    }

    return numbers;
}


long ApplyRules(LinkedListNode<long> number, out long secondNumber)
{

    if (number == 0)
    {
        number = 1;
        secondNumber = -1;
    }
    else if (number.ToString().Length % 2 == 0)
    {
        string fullNum = number.ToString();
        number = int.Parse(fullNum.Substring(0, fullNum.Length / 2));
        secondNumber = int.Parse(fullNum.Substring(fullNum.Length / 2));
    }
    else
    {
        long newNumber = number * 2024;
        number = newNumber;
        secondNumber = -1;
    }

    return number;
}


