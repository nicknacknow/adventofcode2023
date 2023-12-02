string[] lines = File.ReadAllLines(@"../../../input.txt");

Dictionary<string, int> digits = new Dictionary<string, int>
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

bool isStringThere(string main, string sub, int pos)
{
    for (int i = 0; (i < sub.Length); i++)
    {
        if (pos + i > main.Length - 1) return false;
        if (main[pos + i] != sub[i]) return false;
    }

    return true;
}

int function_name(string s, int pos)
{
    foreach (KeyValuePair<string, int> pair in digits)
    {
        if (isStringThere(s, pair.Key, pos)) return pair.Value;
    }

    return -1;
}

int getFirstDigit(string s)
{
    for (int i = 0; i < s.Length; i++)
    {
        char c = s[i];

        int name_check = function_name(s, i);
        if (name_check != -1) return name_check;

        if (char.IsDigit(c)) return int.Parse(c.ToString());
    }

    return -1;
}

int getLastDigit(string s)
{
    for (int i = s.Length - 1; i >= 0; i--)
    {
        char c = s[i];

        int name_check = function_name(s, i);
        if (name_check != -1) return name_check;

        if (char.IsDigit(c)) return int.Parse(c.ToString());
    }

    return -1;
}

int test = function_name("two1nine", 4);
Console.WriteLine(test);

int sum = 0;
foreach (string s in lines)
{
    int first_digit = getFirstDigit(s);
    int last_digit = getLastDigit(s);

    sum += (first_digit * 10 + last_digit);
}

Console.WriteLine(sum);