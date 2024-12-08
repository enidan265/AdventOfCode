
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;

string[] grid = File.ReadAllLines("input.txt");
int rows = grid.Length;
int cols = grid[0].Length;

Dictionary<char, List<int[]>> antennas = new Dictionary<char, List<int[]>>();

List<int[]> antinodes = new List<int[]>();

int counter = 0;

for (int y = 0; y < rows; y++)
{
    for (int x = 0; x < cols; x++)
    {
        char c = grid[y][x];

        if (c != '.' && !antennas.ContainsKey(c))
        {
            antennas.Add(c, new List<int[]>() { ([y, x]) });
        }
        else if (antennas.ContainsKey(c))
        {
            antennas[c].Add([y, x]);
        }
    }
}


foreach (char c in antennas.Keys)
{
    for (int i = 0; i < antennas[c].Count; i++)
    {
        for (int j = i + 1; j < antennas[c].Count; j++)
        {
            int y1 = antennas[c][i][0];
            int x1 = antennas[c][i][1];
            int y2 = antennas[c][j][0];
            int x2 = antennas[c][j][1];

            int diffy = y2 - y1;
            int diffx = x2 - x1;

            int y3 = y1 - diffy;
            int x3 = x1 - diffx;
            if (IsValidPosition(y3, x3))
            {
                antinodes.Add([y3, x3]);
            }
            int y4 = y2 + diffy;
            int x4 = x2 + diffx;
            if (IsValidPosition(y4, x4))
            {
                antinodes.Add([y4, x4]);
            }
        }
    }
}

Console.WriteLine(antinodes.Count);


bool IsValidPosition(int y, int x)
{
    if (y < 0 || y >= rows || x < 0 || x >= cols || antinodes.Any(arr => arr.SequenceEqual([y, x])))
        return false;
    return true;
}
