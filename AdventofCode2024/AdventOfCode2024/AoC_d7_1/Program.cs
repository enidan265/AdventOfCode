
string[] input = File.ReadAllLines("input.txt");

char[] possibleOperations = ['+', '*'];

double result = 0; 

foreach(string line in input)
{
    double testValue = double.Parse(line.Split(':')[0]);
    double[] numbers = Array.ConvertAll((line.Split(':')[1].Trim().Split(' ')), double.Parse);

    if(CheckPossibleEquation(testValue, numbers))
    {
        result += testValue;
    };

}

Console.WriteLine(result);


// 3267: 81 40 27

bool CheckPossibleEquation(double testValue, double[] numbers)
{
    double possibleCombinations = (int)Math.Pow(2, numbers.Length);
    
    for(int i = 0; i < possibleCombinations; i++)
    {
        double result = 0;
        int index = i;

        for (int j = 0; j < numbers.Length; j++)
        {
            if ((index & 1) == 0) // Prüfen, ob das Bit 0 oder 1 ist
            {
                result += numbers[j]; // +
            }
            else
            {
                result *= numbers[j]; // *
            }

            index >>= 1; // Verschieben des Bits
        }

        if (result == testValue)
        {
            return true;
        }
    }

    return false; 
}
