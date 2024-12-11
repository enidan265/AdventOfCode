
string[] input = File.ReadAllLines("input.txt");

List<int[]> map = ReadFileInList(input);
int rows = map.Count();
int cols = map[0].Length;

int totalScore = CalculateTotalScore(map);
Console.WriteLine(totalScore);



int CalculateTotalScore(List<int[]> map)
{
    int totalScore = 0;

    for(int y = 0; y < rows; y++)
    {
        for(int x = 0; x < cols; x++)
        {
            if (map[y][x] == 0)
            {
                bool[,] visited = new bool[rows, cols];
                int score = CountTrailheads(map, y, x, visited);
                totalScore += score;
            }
        }
    }
    return totalScore;
}

int CountTrailheads(List<int[]> map, int y, int x, bool[,] visited)
{
    if(!IsValidPosition(y, x) || visited[y, x])
    {
        return 0;
    }

    visited[y,x] = true;
    int currentHeight = map[y][x];

    if(currentHeight == 9)
    {
        return 1;
    }

    int trailheads = 0; 
    int[] diffY = { -1, 1, 0, 0 };
    int[] diffX = { 0, 0, -1, 1 };

    for(int i = 0; i < 4; i++)
    {
        int newY = y + diffY[i];
        int newX = x + diffX[i];
        
        if(IsValidPosition(newY, newX))
        {
            if (map[newY][newX] == currentHeight + 1)
            {
                trailheads += CountTrailheads(map, newY, newX, visited);
            }
        }
    }
    visited[y, x] = false;
    return trailheads;

}



bool IsValidPosition(int y, int x)
{
    if(y >= 0 && y < rows && x >= 0 && x < cols)
    {
        return true; 
    }
    return false;
}


List<int[]> ReadFileInList(string[] input)
{
    List<int[]> list = new List<int[]>();
    
    foreach (string line in input)
    {
        int[] heights = new int[line.Length];
        for (int i = 0; i < line.Length; i++)
        {
            heights[i] = line[i] - '0';
        }
        list.Add(heights);
    }
    return list;
}



