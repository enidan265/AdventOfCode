using System.Data;

string[] input = File.ReadAllLines("input.txt");
List<char[]> map = ReadMap(input);
int result = 0; 

Robot robot = new Robot(map, input);
RobotMovement robotMovement = new RobotMovement(robot);
robotMovement.Move();

Console.WriteLine(CalculateGPSCoordinateSum(robot.Map));

int CalculateGPSCoordinateSum(List<char[]> map)
{
    int gpsCoordinateSum = 0; 

    for(int y = 0; y < map.Count; y++)
    {
        for(int x = 0; x < map[0].Length; x++)
        {
            if (map[y][x] == 'O')
            {
                gpsCoordinateSum += 100 * y + x;
            } 
        }
    }

    return gpsCoordinateSum;
}

List<char[]> ReadMap(string[] input)
{
    List<char[]> map = new List<char[]>();

    foreach (string line in input)
    {
        if (line.StartsWith('#'))
        {
            map.Add(line.ToCharArray());
        }
    }
    return map;
}

class RobotMovement
{
    public Robot Robot { get; set; }

    public RobotMovement(Robot robot)
    {
        Robot = robot;
    }

    public void Move()
    {
        foreach(var move in Robot.Movement)
        {
            int[] nextPosition = new int[2];
            nextPosition[0] = Robot.Position[0] + move[0];
            nextPosition[1] = Robot.Position[1] + move[1];

            if (IsMovePossible(nextPosition, move))
            {
                if (IsBlock(nextPosition))
                {
                    PushBlock(nextPosition, move);
                }

                if(IsFreeField(nextPosition))
                {
                    MoveRobotToNextField(nextPosition, move);
                }
            }
        }

        Console.WriteLine();
    }

    private void MoveRobotToNextField(int[] nextPosition, int[] currentDirection)
    {
        Robot.Map[nextPosition[0]][nextPosition[1]] = '@';
        Robot.Map[Robot.Position[0]][Robot.Position[1]] = '.';

        Robot.Position[0] = nextPosition[0];
        Robot.Position[1] = nextPosition[1];
    }

    private void PushBlock(int[] blockPosition, int[] currentDirection)
    {
        int[] nextPosition = new int[2];
        nextPosition[0] = blockPosition[0] + currentDirection[0];
        nextPosition[1] = blockPosition[1] + currentDirection[1];

        while (IsBlock(nextPosition))
        {
            nextPosition[0] += currentDirection[0];
            nextPosition[1] += currentDirection[1];
        }

        if (IsFreeField(nextPosition))
        {
            // switch block to free field
            Robot.Map[nextPosition[0]][nextPosition[1]] = 'O';
            Robot.Map[blockPosition[0]][blockPosition[1]] = '.';
        }

    }

    private bool IsMovePossible(int[] nextPosition, int[] currentDirection)
    {
        int[] fieldBehindBlocks = [nextPosition[0], nextPosition[1]];

        while(IsBlock(fieldBehindBlocks))
        {
            fieldBehindBlocks[0] += currentDirection[0];
            fieldBehindBlocks[1] += currentDirection[1];
        }

        if (IsWall(fieldBehindBlocks)) { return false; }

        return true;
    }

    private bool IsBlock(int[] nextPosition)
    {
        if (Robot.Map[nextPosition[0]][nextPosition[1]] == 'O') { return true; }
        return false;
    }

    private bool IsWall(int[] nextPosition)
    {
        if (Robot.Map[nextPosition[0]][nextPosition[1]] == '#') { return true; }
        return false;
    }

    private bool IsFreeField(int[] nextPosition)
    {
        if (Robot.Map[nextPosition[0]][nextPosition[1]] == '.') { return true; }
        return false;
    }

}




class Robot
{
    public int[] Position { get; set; } = new int[2];
    public List<int[]> Movement { get; set; } = new List<int[]>();
    public List<char[]> Map { get; set; }

    public Robot(List<char[]> map, string[] input)
    {
        Map = map;
        Position = FindPositionRobot(map);
        Movement = ReadMovement(input);
    }
    private int[] FindPositionRobot(List<char[]> map)
    {
        int[] position = new int[2];

        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                if (map[y][x] == '@')
                {
                    return [y, x];
                }
            }
        }
        return position;
    }
    private List<int[]> ReadMovement(string[] input)
    {
        List<int[]> movement = new List<int[]>();

        foreach (string line in input)
        {
            if (line.StartsWith('^') || line.StartsWith('>') || line.StartsWith('v') || line.StartsWith('<'))
            {
                foreach (char move in line)
                {
                    switch (move)
                    {
                        case '^':
                            //Move up
                            movement.Add([-1, 0]);
                            break;
                        case '>':
                            //Move right
                            movement.Add([0, 1]);
                            break;
                        case 'v':
                            //Move down
                            movement.Add([1, 0]);
                            break;
                        case '<':
                            movement.Add([0, -1]);
                            break;
                    }
                }
            }
        }

        return movement;
    }

}

