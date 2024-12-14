
string[] input = File.ReadAllLines("input.txt");

// p=0,4 v=3,-3

int rows = 103;   // real input: 103  // test input: 7
int cols = 101;  // real input: 101  // test input: 11
int time = 100; 

List<Robot> robots = FindAllRobots(input);

foreach(Robot robot in robots)
{
    robot.MoveRobot(time, rows, cols);
}

int[] countsQuadrants = CountQuadrants(robots, cols, rows);

int safetyFactor = countsQuadrants[0] * countsQuadrants[1] * countsQuadrants[2] * countsQuadrants[3];

Console.WriteLine(safetyFactor);


int[] CountQuadrants(List<Robot> robots, int cols, int rows)
{
    int[] countsQuadrants = new int[4];

    countsQuadrants[0] = robots.Where(r => r.Position[0] < cols / 2 && r.Position[1] < rows / 2).Count();
    countsQuadrants[1] = robots.Where(r => r.Position[0] > cols / 2 && r.Position[1] < rows / 2).Count();
    countsQuadrants[2] = robots.Where(r => r.Position[0] < cols / 2 && r.Position[1] > rows / 2).Count();
    countsQuadrants[3] = robots.Where(r => r.Position[0] > cols / 2 && r.Position[1] > rows / 2).Count();
    
    return countsQuadrants;
}


// quadranten aufteilen 

// robots in quadranten zählen 

// multiplizieren 








List<Robot> FindAllRobots(string[] input)
{
    List<Robot> robots = new List<Robot>();

    foreach (string line in input)
    {
        int positionX = int.Parse(line.Split(' ')[0].Split('=')[1].Split(',')[0]);
        int positionY = int.Parse(line.Split(' ')[0].Split('=')[1].Split(',')[1]);
        int velocityX = int.Parse(line.Split(' ')[1].Split('=')[1].Split(',')[0]);
        int velocityY = int.Parse(line.Split(' ')[1].Split('=')[1].Split(',')[1]);

        Robot robot = new Robot { Position = [positionX, positionY], Velocity = [velocityX, velocityY] };

        robots.Add(robot);
    }

    return robots;

}

class Robot()
{
    public int[] Position { get; set; }
    public int[] Velocity { get; set; }

    public Robot(int[] position, int[] velocity) : this()
    {
        Position = position;
        Velocity = velocity;
    }

    public void MoveRobot(int time, int rows, int cols)
    {
        int[] nextPosition = new int[2];

        for (int i = 0; i < time; i++)
        {
            nextPosition = [Position[0] + Velocity[0], Position[1] + Velocity[1]];

            if (nextPosition[0] < 0)
            {
                // teleport X-Achse von links nach rechts
                nextPosition[0] = cols + nextPosition[0];
            }
            else if (nextPosition[0] >= cols)
            {
                // teleport X-Achse von rechts nach links
                nextPosition[0] = nextPosition[0] - cols;

            }
            if (nextPosition[1] < 0)
            {
                // teleport Y-Achse von oben nach unten 
                nextPosition[1] = rows + nextPosition[1];
            }
            else if (nextPosition[1] >= rows)
            {
                // teleport Y-Achse von unten nach oben 
                nextPosition[1] = nextPosition[1] - rows;
            }
            Position = nextPosition;
        }

    }
}

