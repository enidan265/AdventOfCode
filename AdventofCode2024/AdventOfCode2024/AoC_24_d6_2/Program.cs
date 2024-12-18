
using System;
using System.ComponentModel;

string[] grid = File.ReadAllLines("input.txt");

int possibleLoopPositions = 0;

char[,] originalGrid = ConvertGrid(grid);

for (int y = 0; y < originalGrid.GetLength(0); y++)
{
    for (int x = 0; x < originalGrid.GetLength(1); x++)
    {
        if (originalGrid[y, x] == '.')
        {
            char[,] newGrid = ConvertGrid(grid);

            Guard guard = new Guard(newGrid);

            guard.Grid[y, x] = '#';
            Console.WriteLine($"Grid: y:{y} x:{x}");
            if (guard.FindPathAndChecksForLoop())
            {
                possibleLoopPositions++;
                Console.WriteLine($"PossibleLoopPossitions: {possibleLoopPositions}");
            }
        }
    }
}


Console.WriteLine(possibleLoopPositions);

 char[,] ConvertGrid(string[] gridStringArray)
{
    int rows = gridStringArray.Length;
    int cols = gridStringArray[0].Length;

    char[,] grid = new char[rows, cols];

    for (int y = 0; y < rows; y++)
    {
        for (int x = 0; x < cols; x++)
        {
            grid[y, x] = gridStringArray[y][x];
        }
    }

    return grid;
}

class Guard
{
    private char[] _guardDirections = ['^', '>', 'v', '<'];

    public char GuardDirection { get; set; }
    public int[] CurrentPosition { get; set; }
    public char[,] Grid { get; set; }

    public HashSet<(int, int, char)> Path { get; set; }

    public Guard(char[,] grid)
    {
        GuardDirection = _guardDirections[0];
        Path = new HashSet<(int, int, char)>();
        Grid = grid;
        CurrentPosition = InitializeCurrentPosition();
    }
    
    public bool FindPathAndChecksForLoop()
    {
        bool isLoop = false;
        while (!HasGuardLeftArea(FindNextPosition()) && !isLoop)
        {
            int[] nextPosition = FindNextPosition();
            isLoop = MovesToNextPositionAndChecksForLoop(nextPosition);
        }

        return isLoop;
    }

    private int[] FindNextPosition()
    {
        int y = 0;
        int x = 0;
        switch (GuardDirection)
        {
            case '^':
                y = CurrentPosition[0] - 1;
                x = CurrentPosition[1];
                break;
            case '>':
                y = CurrentPosition[0];
                x = CurrentPosition[1] + 1;
                break;
            case 'v':
                y = CurrentPosition[0] + 1;
                x = CurrentPosition[1];
                break;
            case '<':
                y = CurrentPosition[0];
                x = CurrentPosition[1] - 1;
                break;
            default:
                throw new InvalidDataException();
        }

        return [y, x];
    }

    private bool MovesToNextPositionAndChecksForLoop(int[] nextPosition)
    {
        int y = nextPosition[0];
        int x = nextPosition[1];

        if (Grid[y, x] == '.' || Grid[y, x] == '^')
        {
            if (!Path.Add((y, x, GuardDirection)))
            {
                return true;
            }
            CurrentPosition[0] = y;
            CurrentPosition[1] = x;
        }
        else if (Grid[y, x] == '#')
        {
            switch (GuardDirection)
            {
                case '^':
                    GuardDirection = '>';
                    break;
                case '>':
                    GuardDirection = 'v';
                    break;
                case 'v':
                    GuardDirection = '<';
                    break;
                case '<':
                    GuardDirection = '^';
                    break;
                default:
                    throw new InvalidDataException();
            }
        }
        return false;
    }

    private bool HasGuardLeftArea(int[] nextPosition)
    {
        int y = nextPosition[0];
        int x = nextPosition[1];

        if (y < 0 || y >= Grid.GetLength(0) || x < 0 || x >= Grid.GetLength(1))
        {
            return true;
        }

        return false;
    }

    private int[] InitializeCurrentPosition()
    {
        int rows = Grid.GetLength(0);
        int cols = Grid.GetLength(1);

        int[] currentPosition = new int[2];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (Grid[y, x] == GuardDirection)
                {
                    currentPosition[0] = y;
                    currentPosition[1] = x;
                    break;
                }
            }
        }
        return currentPosition;
    }

}