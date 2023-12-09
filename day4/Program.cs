string[] lines = File.ReadAllLines(@"../../../input.txt");


List<int> getNumbersFromString(string str)
{
    List<int> numbers = new List<int>();

    foreach(string s in str.Split(' '))
    {
        int result = -1;
        if (int.TryParse(s, out result))
            numbers.Add(result);
    }

    return numbers;
}

int getNumberOfSharedNumbers(List<int> a, List<int> b)
{
    int count = 0;

    foreach (int n in a)
    { 
        foreach (int n2 in b)
        {
            if (n2 == n)
            {
                count++;
                break;
            }
        }
    }

    return count;
}


//int sum = 0;

List<int> vs = Enumerable.Repeat(1, lines.Length).ToList();

int index = 0;
foreach (string line in lines)
{
    string numbers = line.Split(':')[1];
    string[] temp = numbers.Split('|');

    string winning_numbers = temp[0];
    string card_numbers = temp[1];


    List<int> winning_nums = getNumbersFromString(winning_numbers);
    List<int> card_nums = getNumbersFromString(card_numbers);

    int count = getNumberOfSharedNumbers(winning_nums, card_nums);

    for (int x = 0; x < vs[index]; x++) {
        for (int i = 0; i < count; i++)
        {
            vs[index + 1 + i]++;
        }
    }

   // Console.WriteLine(count);
    //sum += (int)Math.Pow(2, count - 1);

    index++;
}

Console.WriteLine(Enumerable.Sum(vs));