/// <summary>
/// 
/// </summary>
internal static class GenMap
{
    public static void Display(List<string> map, int mapSize)
    {
        foreach (int i in Enumerable.Range(0, mapSize))
        {
            Console.Write($" {i} ");
        }
        Console.WriteLine();

        int yAxis = 0;
        int xAxis = 0;
        foreach (string s in map)
        {
            if (s == "\n") { }
            else if (s.ToArray()[1] == '1')
                Console.ForegroundColor = ConsoleColor.Blue;
            else if (s.ToArray()[1] == '2')
                Console.ForegroundColor = ConsoleColor.Green;
            else if (s.ToArray()[1] == '3')
                Console.ForegroundColor = ConsoleColor.Red;
            else if (s.ToArray()[1] == '4')
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            else if (s.ToArray()[1] == '5')
                Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (s.ToArray()[1] == '6')
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            else if (s.ToArray()[1] == '7')
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            if (yAxis % 2 == 0) // Makes a grid like pattern to see coords better
            {
                //Console.BackgroundColor = ConsoleColor.DarkGray;
            }
            xAxis++;

            if (s == "\n")
            {
                Console.Write($" {yAxis} ");
                yAxis++; // Get it?
                xAxis = 0;
            }

            Console.Write(s);

            Console.ResetColor();
        }
        Console.WriteLine();
    }
}