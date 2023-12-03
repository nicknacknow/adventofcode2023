string[] lines = File.ReadAllLines(@"../../../input.txt");
int LINE_LENGTH = 140;
int LINE_HEIGHT = 140;

char get2dpos(int x, int y)
{
    return lines[y][x];
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


List<char> getSurroundingChars(int x, int y)
{
    List<char> ret = new List<char>();

    foreach (int[] direction in directions)
    {
        int cx = x + direction[0];
        int cy = y + direction[1];

        if ((cx >= 0 && cx < LINE_LENGTH) && (cy >= 0 && cy < LINE_HEIGHT))
        {
            char c = get2dpos(cx, cy);
            if (c != '.') ret.Add(c);
        }
    }
    
    return ret;
}

bool isConnected(int x, int y)
{
    foreach (char c in getSurroundingChars(x, y))
        if (!char.IsDigit(c)) return true;

    return false;
}

int total_sum = 0;
for (int y = 0; y < lines.Length; y++)
{
    string line = lines[y];
    int start_x = -1;
    bool counted = false;

    for (int x = 0; x < line.Length; x++)
    {
        //Console.WriteLine($"{x} {y}");
        char current = get2dpos(x, y);
        if (current == '.')
        {
            start_x = -1;
            counted = false;
        }
        else if (char.IsDigit(current) && !counted)
        {
            if (start_x == -1) start_x = x;

            if (isConnected(x, y))
            {
                int full_number = getFullNumber(start_x, y);
                Console.WriteLine(full_number);
                total_sum += full_number;
                counted = true;
                start_x = -1;
            }

        }

    }
}


Console.WriteLine(total_sum);