FileStream file = File.OpenRead("input.txt");
StreamReader sr = new StreamReader(file);

//alle Zielen einlesen
string[] lines = sr.ReadToEnd().Split('\n');
int result = 0;

//Symbole finden und Symbol-Objekte erstellen
List<Symbol> symbolList = new List<Symbol>();
FindSymbols(lines, symbolList);

//Zahlen finden und Number-Objekte erstellen
List<Number> numberList = new List<Number>();
FindNumbers(lines, numberList);

//PartNumber-Liste erstellen
List<Number> partNumberList = new List<Number>();
partNumberList = FindPartNumbers(symbolList, numberList);

foreach (Symbol symbol in symbolList)
{
    if (symbol.Value == '*')
    {
        result += CheckIfGear(symbol, numberList);
    }
}

Console.WriteLine(result);


//Methoden
List<Number> SelectNumberLines(List<Number> numberList, Symbol symbolStar)
{
    List<Number> selectedNumberLines = new List<Number>();
    foreach (Number number in numberList)
    {
        if (number.Line == symbolStar.Line || number.Line == symbolStar.Line - 1 || number.Line == symbolStar.Line + 1)
        {
            selectedNumberLines.Add(number);
        }
    }
    return selectedNumberLines;
}
List<Symbol> SelectSymbolLines(List<Symbol> symbolList, Number number)
{
    List<Symbol> selectedSymbolLines = new List<Symbol>();
    foreach (Symbol symbol in symbolList)
    {
        if (symbol.Line == number.Line || symbol.Line == number.Line + 1 || symbol.Line == number.Line - 1)
        {
            selectedSymbolLines.Add(symbol);
        }
    }
    return selectedSymbolLines;
}

int CheckIfGear(Symbol symbolStar, List<Number> numberList)
{
    int counter = 0;
    int result = 1;
    foreach (Number number in SelectNumberLines(numberList, symbolStar))
    {
        number.PositionRange = number.GetNumberRange();
        if (number.PositionRange.Contains(symbolStar.Position) || number.PositionRange.Contains(symbolStar.Position - 1) || number.PositionRange.Contains(symbolStar.Position + 1))
        {
            result *= number.Value;
            counter++;
        }
    }
    if (counter == 2)
    {
        return result;
    }
    else { return 0; }
}


bool CheckPosition(Symbol symbol, Number number)
{
    number.PositionRange = number.GetNumberRange();

    if (number.PositionRange.Contains(symbol.Position) || number.PositionRange.Contains(symbol.Position - 1) || number.PositionRange.Contains(symbol.Position + 1))
    {
        return true;
    }
    else { return false; }
}


List<Number> FindPartNumbers(List<Symbol> symbolList, List<Number> numberList)
{
    List<Number> partNumbers = new List<Number>();
    foreach (Number number in numberList)
    {
        foreach (Symbol symbol in SelectSymbolLines(symbolList, number))
        {
            if (CheckPosition(symbol, number))
            {
                partNumberList.Add(number);
            };
        }
    }
    return partNumberList;
}

void FindNumbers(string[] lines, List<Number> numberList)
{
    for (int i = 0; i < lines.Length; i++)
    {
        int count = 0;
        string line = lines[i];
        string numberStr = "";

        foreach (char c in line)
        {
            if (char.IsDigit(c) && count - 1 > 0 && !char.IsDigit(line[count - 1]) && !char.IsDigit(line[count + 1]))
            {
                Number number = new Number();
                number.Line = i;
                number.StartPosition = number.EndPosition = count;
                number.Value = c - '0';
                numberList.Add(number);
                count++;
                continue;
            }
            if ((char.IsDigit(c) && (count - 1 < 0)) || (char.IsDigit(c) && !char.IsDigit(line[count - 1])))
            {
                //erste Ziffer einer Zahl
                numberStr = "";
                Number number = new Number();
                number.Line = i;
                number.StartPosition = count;
                numberList.Add(number);
                numberStr += c;
                count++;
                continue;
            }
            else if ((char.IsDigit(c) && char.IsDigit(line[count - 1]) && (count + 1 > line.Length)) ||
                    (char.IsDigit(c) && char.IsDigit(line[count - 1]) && char.IsDigit(line[count + 1])))
            {
                //Ziffer innerhalb einer Zahl
                numberStr += c;
                count++;
                continue;
            }
            else if ((char.IsDigit(c) && (count + 1 > line.Length)) ||
                    (char.IsDigit(c) && !char.IsDigit(line[count + 1])))
            {
                //letzte Ziffer einer Zahl
                numberStr += c;
                numberList.Last<Number>().EndPosition = count;
                numberList.Last<Number>().Value = Convert.ToInt32(numberStr);
            }
            count++;
        }
    }
}

void FindSymbols(string[] lines, List<Symbol> symbolList)
{
    for (int i = 0; i < lines.Length; i++)
    {
        int count = 0;
        foreach (char c in lines[i])
        {
            if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) && c != '.')
            {
                Symbol symbol = new Symbol();
                symbol.Line = i;
                symbol.Value = c;
                symbol.Position = count;
                symbolList.Add(symbol);
            }
            count++;
        }
    }
}


//Klassen
public class Number
{
    public int Value { get; set; }
    public int Line { get; set; }
    public int StartPosition { get; set; }
    public int EndPosition { get; set; }
    public int[]? PositionRange { get; set; }


    public int[] GetNumberRange()
    {
        if (StartPosition == EndPosition)
        {
            PositionRange = new int[1];
            PositionRange[0] = StartPosition;
        }
        else
        {
            PositionRange = new int[EndPosition - StartPosition + 1];
            int count = 0;
            for (int i = StartPosition; i <= EndPosition; i++)
            {
                PositionRange[count] = i;
                count++;
            }
        }

        return PositionRange;
    }
}

public class Symbol
{
    public char Value { get; set; }
    public int Line { get; set; }
    public int Position { get; set; }
}