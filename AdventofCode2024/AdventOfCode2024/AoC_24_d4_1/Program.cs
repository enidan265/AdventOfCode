

string[] grid = File.ReadAllLines("input.txt");

int rows = grid.Length;
int cols = grid[0].Length;
string word = "XMAS";

int[] directions = new int[] {  1, 0, -1, 0,    // Horizontal
                                0, 1, 0, -1,     // Vertikal
                                -1, -1, -1, 1,  // Diagonal_links
                                1, -1, 1, 1 };  // Diagonal_rechts

int counter = 0; 


for(int yPos = 0; yPos < rows; yPos++)
{
    for(int xPos = 0; xPos < cols; xPos++)
    {
        if(grid[yPos][xPos] == 'X')
        {
            for(int i = 0; i < directions.Length; i = i + 2)
            {
                int diffX = directions[i];
                int diffY = directions[i + 1];

                if(IsValidPosition(xPos, yPos, diffX, diffY, rows, cols, word) && CheckDirection(grid, xPos, yPos, diffX, diffY, word))
                {
                    counter++;
                };
            }
        }
    }
}


Console.WriteLine(counter);


bool IsValidPosition(int xPos, int yPos, int directionX, int directionY, int rows, int cols, string word)
{
    bool isValid = false;

    int endX = xPos + directionX * (word.Length - 1);
    int endY = yPos + directionY * (word.Length - 1);
    
    if(endX < cols && endX >= 0 && endY < rows && endY >= 0)
    {
        isValid = true;
    }

    return isValid;
}

bool CheckDirection(string[] grid, int xPos, int yPos, int diffX, int diffY, string word)
{
    for(int i = 0; i < word.Length; i++) 
    {
        if (grid[yPos + diffY * i][xPos + diffX * i] != word[i])
        {
            return false;
        }
    }
    return true;
}




