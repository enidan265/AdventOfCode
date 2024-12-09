
string input = File.ReadAllText("input_test.txt");

// 2333133121414131402
// 00...111...2...333.44.5555.6666.777.888899

string translated = string.Empty;

for(int i = 0; i < input.Length; i++)
{
    int counter = 0;
    if (i % 2 == 0)
    {
        int num = input[i] - '0';
        for(int j = 0; j < num; j++)
        {
            translated = translated + counter;
        }
        counter++;
    }
    else
    {
        int num = input[i] - '0';
        for (int j = 0; j < num; j++)
        {
            translated = translated + '.';
        }
    }
}

char[] blocks = translated.ToCharArray();

for(int i = blocks.Length -1; i >= 0; i--)
{
    if (blocks[i] != '.')
    {
        for(int j = 0; j < blocks.Length; j++)
        {
            if (blocks[j] == '.')
            {
                
            }
        }
    }
}



