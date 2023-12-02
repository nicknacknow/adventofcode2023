string[] lines = File.ReadAllLines(@"../../../input.txt");

string test = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";

int getGameID(string s)
{
    return int.Parse(s.Split(":")[0].Split(" ")[1]);
}

string[] getSets(string s)
{
    string gameID = getGameID(s).ToString();

    string substring = s.Substring(gameID.Length + 6);

    return substring.Split(";");
}

string[] colours = { "red", "green", "blue" };

bool isStringThere(string m, string s)
{
    return m.Replace(s, "") != m;
}

Dictionary<string, int> getSetDetails(string set)
{
    Dictionary<string, int> ret = new Dictionary<string, int>
    {
        { "red", 0 },
        { "green", 0 },
        { "blue", 0 }
    };

    foreach (string s in set.Split(","))
    {
        foreach (string colour in colours)
        {
            if (isStringThere(s, colour))
            {
                int colour_num = int.Parse(s.Replace(colour, ""));
                ret[colour] = colour_num;
                //ret.Add(colour, colour_num);
                break;
            }
        }
    }

    return ret;
}



bool isValidGame(string game, int max_red = 12, int max_green = 13, int max_blue = 14)
{
    string[] game_sets = getSets(game);

    foreach (string set in game_sets)
    {
        Dictionary<string, int> set_data = getSetDetails(set);
        if (set_data["red"] > max_red) return false;
        if (set_data["green"] > max_green) return false;
        if (set_data["blue"] > max_blue) return false;
    }

    return true;
}


/*Console.WriteLine(getGameID(test));

foreach (string s in getSets(test))
{
    Console.WriteLine(s.Trim());

    foreach (string x in s.Split(","))
    {
        Console.WriteLine(x.Trim());
    }
}

Dictionary<string, int> details = getSetDetails("1 red, 2 green, 6 blue");

foreach (KeyValuePair<string, int> kvp in details)
{
    Console.WriteLine($"{kvp.Key}   {kvp.Value}");
}*/



int sum = 0;
foreach (string line in lines)
{
    int game_id = getGameID(line);
    
    if (isValidGame(line))
    {
        sum += game_id;
    }
}

Console.WriteLine(sum);