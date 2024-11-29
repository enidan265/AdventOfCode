/* opponent:    A - Rock / B - Paper / C - Scissors
 * me:          X - Rock / Y - Paper / Z - Scissors
 * 
 * Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock
 * 
 * Shape-Points: 1 - Rock / 2 - Paper / 3 - Scissors
 * Win-Points: 0 -  loss / 3 - draw / 6 - won */


FileStream file = File.OpenRead("2022_Day2.txt");

StreamReader sr = new StreamReader(file);

string line;
int points = 0;

while (true)
{
    line = sr.ReadLine();   
    if(line != null)
    {
        //Shape-Points
        if (line.Contains("X"))
        {
            points += 1;
        }
        else if (line.Contains("Y"))
        {
            points += 2;
        }
        else if (line.Contains("Z"))
        {
            points += 3;
        }

        //Winner
        if ((line.Contains("X") && line.Contains("A")) || (line.Contains("Y") && line.Contains("B")) || (line.Contains("Z") && line.Contains("C")))
        {
            //Unentschieden
            points += 3;
        }
        else if ((line.Contains("X") && line.Contains("C")) || (line.Contains("Y") && line.Contains("A")) || (line.Contains("Z") && line.Contains("B")))
        {
            //Gewonnen
            points += 6;
        }
    }
    else { break; }
}

Console.WriteLine(points);




