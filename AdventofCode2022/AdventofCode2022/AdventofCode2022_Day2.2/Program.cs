/* opponent:    A - Rock / B - Paper / C - Scissors* 
 * Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock
 * X - lose / Y - draw / Z - win
 * 
 * Shape-Points: 1 - Rock / 2 - Paper / 3 - Scissors
 * Win-Points: 0 -  loss / 3 - draw / 6 - won */

/* X - have to lose / Y - draw / Z - win
 */

FileStream file = File.OpenRead("2022_Day2.txt");

StreamReader sr  = new StreamReader(file);

int points = 0;

while (true)
{
    string line = sr.ReadLine();
    if(line != null)
    {
        if (line.Contains("X"))
        {
            // lose
            if (line.Contains("A"))
            {
                points += 3;
            }
            else if (line.Contains("B"))
            {
                points += 1;
            }
            else if (line.Contains("C"))
            {
                points += 2;
            }
        }
        else if (line.Contains("Y"))
        {
            //draw
            points += 3;
            if (line.Contains("A"))
            {
                points += 1;
            }
            else if (line.Contains("B"))
            {
                points += 2;
            }
            else if (line.Contains("C"))
            {
                points += 3;
            }
        }
        else if (line.Contains("Z"))
        {
            //win
            points += 6;
            if (line.Contains("A"))
            {
                points += 2;
            }
            else if (line.Contains("B"))
            {
                points += 3;
            }
            else if (line.Contains("C"))
            {
                points += 1;
            }
        }
    }
    else { break; }
}


Console.WriteLine(points);