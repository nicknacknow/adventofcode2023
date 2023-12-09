string[] lines = File.ReadAllLines(@"../../../input.txt");
int LINE_LENGTH = lines[0].Length;
int LINE_HEIGHT = lines.Length;


char get2dpos(int x, int y)
{
    return lines[y][x];
}

int[][] directions = new int[][]
        {
            new int[] { -1, -1 },
            new int[] { -1, 0 },
            new int[] { -1, 1 },
            new int[] { 0, 1 },
            new int[] { 1, 1 },
            new int[] { 1, 0 },
            new int[] { 1, -1 },
            new int[] { 0, -1 }
        };


List<KeyValuePair<char, int[]>> getSurroundingChars(int x, int y)
{
    List<KeyValuePair<char, int[]>> ret = new List<KeyValuePair<char, int[]>>();

    

    foreach (int[] direction in directions)
    {
        int cx = x + direction[0];
        int cy = y + direction[1];

        if ((cx >= 0 && cx < LINE_LENGTH) && (cy >= 0 && cy < LINE_HEIGHT))
        {
            char c = get2dpos(cx, cy);
            if (c != '.') ret.Add(new KeyValuePair<char, int[]>(c, new int[] { cx, cy }));
        }
    }

    return ret;
}

// get all instances of special character:
// then we check surrounding digits

bool isSpecial(char c)
{
    return !(char.IsDigit(c) || c == '.');
}


List<int[]> special_list = new List<int[]>();

special_list.Add(new int[] { 1,2 });

for (int y = 0; y < lines.Length; y++)
{
    string line = lines[y];
    for (int x = 0; x < line.Length; x++)
    {
        char c = line[x];

        if (isSpecial(c)) special_list.Add(new int[] { x, y });
    }
}


int[] findStartOfNumber(int x, int y)
{
    int[] vec = new int[2];

    int _x = x - 1;
    bool done = false;
    while (!done && _x > 0)
    {
        char curr = get2dpos(_x, y);
        if (curr == '.')
        {
            done = true;
            vec[0] = _x + 1;
            vec[1] = y;
        }
        else _x--;
    }

    return vec;
}

int FindStartOfNumber(string input, int index)
{
    // Check if the index is within bounds
    if (index < 0 || index >= input.Length)
    {
        return -1;
    }

    // Iterate backward from the given index to find the start of the number
    for (int i = index - 1; i >= 0; i--)
    {
        char currentChar = input[i];

        if (currentChar == '.')
        {
            // Found the '.' before the number
            return i + 1; // Start of the number
        }

        if (!char.IsDigit(currentChar))
        {
            // Stop if a non-digit character is encountered before finding the '.'
            break;
        }
    }

    return 0; // '.' not found before the number
}


int getFullNumber(int x, int y)
{
    Stack<int> stack = new Stack<int>();

    char curr = get2dpos(x++, y);
    while (char.IsDigit(curr) && x < LINE_LENGTH)
    {
        stack.Push(int.Parse(curr.ToString()));
        curr = get2dpos(x++, y);
    }

    int sum = 0;
    for (int i = 0; stack.Count > 0; i++)
    {
        sum += (stack.Pop() * (int)Math.Pow(10, i));
    }

    return sum;
}

bool IsIn(List<int[]> list, int[] vec)
{
    // Check if any array in the list is equal to vec
    foreach (var item in list)
    {
        if (ArraysAreEqual(item, vec))
        {
            return true;
        }
    }

    return false;
}

bool ArraysAreEqual(int[] array1, int[] array2)
{
    // Check if the arrays have the same length
    if (array1.Length != array2.Length)
    {
        return false;
    }

    // Compare the elements of the arrays
    for (int i = 0; i < array1.Length; i++)
    {
        if (array1[i] != array2[i])
        {
            return false;
        }
    }

    return true;
}

bool isIn(List<int[]> list, int[] vec)
{
    for (int i = 0; i < list.Count; i++)
    {
        int[] vs = list[i];
        bool test = true;
        for (int x = 0; x < vs.Length; x++)
        {
            if (vs[x] != vec[i]) test = false;
        }
        if (test) return true;
    }
    return false;
}

int total_sum = 0;
List<int[]> startOfNumbers = new List<int[]>();
foreach (int[] ee in special_list)
{
    List<KeyValuePair<char, int[]>> surr = getSurroundingChars(ee[0], ee[1]);

    foreach (KeyValuePair<char, int[]> c in surr)
    {
        int x = c.Value[0];
        int y = c.Value[1];

        int test = FindStartOfNumber(lines[y], x);
        //Console.WriteLine($"AHHHHHHHHHHHH {test} {x} {y}");

        int[] vec = new int[] { test,y };
        if (!startOfNumbers.Any(p => p.SequenceEqual(vec)))
        {
            startOfNumbers.Add(new int[] { test, y });
           // Console.WriteLine($"{c.Key} :  {x} {y}  {vec[0]} {vec[1]} number : {getFullNumber(test, y)}");
        }

    }
}

 foreach (int[] n in startOfNumbers)
{
    int full_number = getFullNumber(n[0], n[1]);
    //Console.WriteLine(full_number);
    total_sum += full_number;
}


    Console.WriteLine(total_sum);





















/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */


int CalculatePartNumberSum(string schematic)
{
    string[] lines = schematic.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    int sum = 0;

    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            char currentChar = lines[i][j];

            if (IsSymbol(currentChar))
            {
                sum += GetAdjacentPartNumbers(lines, i, j);
            }
        }
    }

    return sum;
}

bool IsSymbol(char c)
{
    return !(char.IsDigit(c) || c == '.');
}

int GetAdjacentPartNumbers(string[] lines, int row, int col)
{
    int sum = 0;

    for (int i = row - 1; i <= row + 1; i++)
    {
        for (int j = col - 1; j <= col + 1; j++)
        {
            if (i >= 0 && i < lines.Length && j >= 0 && j < lines[i].Length && char.IsDigit(lines[i][j]))
            {
                sum += int.Parse(lines[i][j].ToString());
            }
        }
    }

    return sum;
}

string engineSchematic = @"
            467..114..
            ...*......
            ..35..633.
            ......#...
            617*......
            .....+.58.
            ..592.....
            ......755.
            ...$.*....
            .664.598..
        ";

int sum = CalculatePartNumberSum(engineSchematic);
Console.WriteLine($"The sum of part numbers is: {sum}");