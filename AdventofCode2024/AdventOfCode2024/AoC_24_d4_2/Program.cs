

string[] grid = File.ReadAllLines("input.txt");

int rows = grid.Length;
int cols = grid[0].Length;

int[] directions = new int[] { -1, -1, -1, 1 };  // Diagonal_links
                                

int counter = 0;


for (int yPos = 0; yPos < rows; yPos++)
{
    for (int xPos = 0; xPos < cols; xPos++)
    {
        if (grid[yPos][xPos] == 'A')
        {
            if (IsValidPosition(xPos, yPos, rows, cols) && CheckDirections(grid, xPos, yPos))
            {
                counter++;
            };
            
        }
    }
}


Console.WriteLine(counter);


bool IsValidPosition(int xPos, int yPos, int rows, int cols)
{

    if (xPos + 1 < cols && xPos -1 >= 0 && yPos + 1 < rows && yPos - 1 >= 0)
    {
        return true; 
    }

    return false;
}

bool CheckDirections(string[] grid, int xPos, int yPos)
{
   
    if ((grid[yPos + 1][xPos + 1] == 'M' && grid[yPos - 1][xPos - 1] == 'S') ||
        (grid[yPos + 1][xPos + 1] == 'S' && grid[yPos - 1][xPos - 1] == 'M'))
    {
        if ((grid[yPos - 1][xPos + 1] == 'M' && grid[yPos + 1][xPos - 1] == 'S') ||
            (grid[yPos - 1][xPos + 1] == 'S' && grid[yPos + 1][xPos - 1] == 'M'))
        {
            return true;
        }
    }

    return false;
}




