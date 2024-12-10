

string input = File.ReadAllText("input.txt");
long result = 0;

List<int> translatedBlocks = TranslateBlocks(input);

List<int> movedBlocks = MoveBlocks(translatedBlocks);

result = GenerateCheckSum(movedBlocks);

Console.WriteLine(result);



List<int> TranslateBlocks(string input)
{
    int counter = 0;
    List<int> translated = new List<int>();

    for (int i = 0; i < input.Length; i++)
    {
        if (i % 2 == 0)
        {
            int num = input[i] - '0';
            for (int j = 0; j < num; j++)
            {
                translated.Add(counter);
            }
            counter++;
        }
        else
        {
            int num = input[i] - '0';
            for (int j = 0; j < num; j++)
            {
                translated.Add(-1);
            }
        }
    }
    return translated;
}


List<int> MoveBlocks(List<int> translatedBlocks)
{
    int writePointer = 0;

    for (int readPointer = translatedBlocks.Count - 1; readPointer >= 0; readPointer--)
    {
        writePointer = translatedBlocks.IndexOf(-1);
        if (writePointer == -1) { break; }

        if (translatedBlocks[readPointer] != -1 && writePointer < readPointer)
        {
            translatedBlocks[writePointer] = translatedBlocks[readPointer];
            translatedBlocks[readPointer] = 0;
        }
        else if (translatedBlocks[readPointer] == -1)
        {
            translatedBlocks[readPointer] = 0;
        }
    }

    return translatedBlocks;
}

long GenerateCheckSum(List<int> movedBlocks)
{
    for (int i = 0; i < movedBlocks.Count; i++)
    {
        result += movedBlocks[i] * i;
    }
    return result;
}