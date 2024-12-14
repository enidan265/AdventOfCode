
using System.Numerics;

string[] input = File.ReadAllLines("input.txt");

List<Machine> machines = ReadAllMachines(input);

int costs = 0; 
foreach(Machine machine in machines)
{
    int[] correctSolution = new int[2];

    if (ExistsPossibleCombination(machine))
    {
        List<int[]> possibleSolutions = FindPositiveSolutionsInRange(machine.ButtonA[0], machine.ButtonB[0], machine.PosPrize[0]);

        foreach(var possibleSolution in possibleSolutions)
        {
            //check if Yprize = solution[0] * ButtonAy + solution[1] * ButtonBy
            if(VerifyPrizeEquation(possibleSolution[0], possibleSolution[1], machine))
            {
                correctSolution[0] = possibleSolution[0];
                correctSolution[1] = possibleSolution[1];
            }
        }
        int costsMachine = correctSolution[0] * 3 + correctSolution[1] * 1;
        costs += costsMachine;
    }
}


Console.WriteLine(costs);

bool ExistsPossibleCombination(Machine machine)
{
    BigInteger ggTX = BigInteger.GreatestCommonDivisor(machine.ButtonA[0], machine.ButtonB[0]);
    BigInteger ggTY = BigInteger.GreatestCommonDivisor(machine.ButtonA[1], machine.ButtonB[1]);

    if (machine.PosPrize[0] % ggTX != 0 || machine.PosPrize[1] % ggTY != 0)
    {
        return false; 
    }

    return true; 
}

List<int[]> FindPositiveSolutionsInRange(int buttonAx, int buttonBx, int prizeX)
{
    var solutions = new List<int[]>();

    int range = 100;

    for (int a = -range; a <= range; a++)
    {
        if ((prizeX - a * buttonAx) % buttonBx == 0)
        {
            int b = (prizeX - a * buttonAx) / buttonBx;

            if (b >= -range && b <= range)
            {
                solutions.Add([a,b]);
            }
        }
    }

    return solutions;
}

bool VerifyPrizeEquation(int a, int b, Machine machine)
{
    int YA = machine.ButtonA[1];
    int YB = machine.ButtonB[1];
    int YPrize = machine.PosPrize[1];

    bool isYPrizeCorrect = (a * YA + b * YB) == YPrize;

    return isYPrizeCorrect;
}

List<Machine> ReadAllMachines(string[] input)
{
    List<Machine> machines = new List<Machine>();

    foreach (string line in input)
    {
        if (line.StartsWith("Button A:"))
        {
            Machine machine = new Machine();
            machines.Add(machine);
            machine.ButtonA = [int.Parse(line.Split(':')[1].Split(',')[0].Split('+')[1].Trim()),
                             int.Parse(line.Split(':')[1].Split(',')[1].Split('+')[1].Trim())];
        }
        if (line.StartsWith("Button B:"))
        {
            machines.Last().ButtonB = [int.Parse(line.Split(':')[1].Split(',')[0].Split('+')[1].Trim()),
                                     int.Parse(line.Split(':')[1].Split(',')[1].Split('+')[1].Trim())];
        }
        if (line.StartsWith("Prize:"))
        {
            machines.Last().PosPrize = [int.Parse(line.Split(':')[1].Split(',')[0].Split('=')[1].Trim()),
                                        int.Parse(line.Split(':')[1].Split(',')[1].Split('=')[1].Trim())];
        }
    }

    return machines;
}

record Machine
{
    public int[] ButtonA = new int[2];
    public int[] ButtonB = new int[2];
    public int[] PosPrize = new int[2];
}