
Console.WriteLine(
    File.ReadAllLines("input.txt").Sum(line => Convert.ToInt32($"{line.First(chr => Char.IsDigit(chr))}{line.Last(chr => Char.IsDigit(chr))}")));

