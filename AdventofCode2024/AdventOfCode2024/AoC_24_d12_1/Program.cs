using System.Collections;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

string[] map = File.ReadAllLines("input.txt");

int result = 0;

List<int[]> countedPlants = new List<int[]>();

for(int y = 0; y < map.Length; y++)
{
    for(int x = 0; x < map[0].Length; x++)
    {
        if(!CheckIfInList(countedPlants, y, x))
        {
            List<int[]> foundpositions = FindOneRegionForPlantType(map, y, x);
            countedPlants.AddRange(foundpositions);
            int areaForRegion = foundpositions.Count();
            int perimeter = CalculatePerimeter(map, foundpositions);

            result += (areaForRegion * perimeter);
        }
    }
}

Console.WriteLine(result);

List<int[]> FindOneRegionForPlantType(string[] map, int y, int x)
{
    List<int[]> foundPositions = new List<int[]>();
    Queue queue = new Queue();
    int rows = map.Length;
    int cols = map[0].Length;
    var plantType = map[y][x];
    int[] startPosition = [y, x];

    queue.Enqueue(startPosition);
    
    while(queue.Count > 0)
    {
        int[] position = (int[])queue.Dequeue();
        int nextY = position[0];
        int nextX = position[1];
        if (!ValidPosition(nextY, nextX, rows, cols) || map[nextY][nextX] != plantType || CheckIfInList(foundPositions, nextY, nextX))
        {
            continue;
        }
        else
        {
            foundPositions.Add(position);
            queue.Enqueue(new int[] { nextY + 1, nextX });
            queue.Enqueue(new int[] { nextY - 1, nextX });
            queue.Enqueue(new int[] { nextY, nextX + 1 });
            queue.Enqueue(new int[] { nextY, nextX - 1 });
        }
    }

    return foundPositions;
}

int CalculatePerimeter(string[] map, List<int[]> foundPositions)
{
    int perimeter = 0;
    int[] diffY = { -1, 1, 0, 0 };
    int[] diffX = { 0, 0, -1, 1 };

    foreach (var position in foundPositions)
    {
        int y = position[0];
        int x = position[1];
       
        //Check if upper or lower border
        if (y == 0 || y + 1 == map.Length) { perimeter++; }
        
        //Check if left or right border
        if(x == 0 || x + 1 == map[0].Length) { perimeter++; }

        //Check if neighbor isnt in list
        for (int i = 0; i < 4; i++)
        {
            if (ValidPosition(y - diffY[i], x - diffX[i], map.Length, map[0].Length) &&
                !CheckIfInList(foundPositions, y - diffY[i], x - diffX[i])) 
            { 
                perimeter++; 
            }
        }
    }

    return perimeter;
}

bool CheckIfInList(List<int[]> list, int y, int x)
{
    return list.Any(p => p[0] == y && p[1] == x);
}


bool ValidPosition(int y, int x, int rows, int cols)
{
    return y >= 0 && y < rows && x >= 0 && x < cols;
}
