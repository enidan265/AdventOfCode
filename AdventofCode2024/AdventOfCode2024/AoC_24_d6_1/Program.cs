

using System.ComponentModel;

string[] grid = File.ReadAllLines("input.txt");

// directions [y, x]

Guard guard = new Guard(grid);

while (guard.GuardMoving())
{
    guard.GuardMoving();
}

Console.WriteLine(guard.Path.Count());


class Guard
{
    private char[] _guardDirections = ['^', '>', 'v', '<'];

    public char GuardDirection { get; set; }
    public int[] CurrentPosition { get; set; }
    public string[] Grid { get; set; }

    public List<int[]> Path {  get; set; }

    public Guard(string[] grid)
    {
        GuardDirection = _guardDirections[0];
        Path = new List<int[]>();
        Grid = grid;
        CurrentPosition = InitializeCurrentPosition(Grid);
    }

    public bool GuardMoving()
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
        
        if(!GuardLeftArea(x, y))
        {
            CheckNextPosition(y, x);
        }
        else
        {
            return false;
        }
        return true;
    }


    private void CheckNextPosition(int y, int x) 
    {
        if (Grid[y][x] == '.' || Grid[y][x] == '^')
        {
            if (!Path.Any(pos => pos.SequenceEqual([y, x])))
            {
                Path.Add([y, x]);
            }
            CurrentPosition[0] = y;
            CurrentPosition[1] = x;
        }
        else if (Grid[y][x] == '#')
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
    }

    private bool GuardLeftArea(int y, int x)
    {
        if(y < 0 || y >= Grid.Length || x < 0 || x >= Grid[0].Length)
        {
            return true;
        }

        return false;
    }

    private int[] InitializeCurrentPosition(string[] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;

        int[] currentPosition = new int[2];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (grid[y][x] == GuardDirection)
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










