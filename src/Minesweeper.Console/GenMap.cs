/// <summary>
/// 
/// </summary>
internal static class GenMap
{
    public static void Display(List<(bool, string)> map)
    {
        foreach ((bool, string) displayString in map)
        {
            string s = displayString.Item2;
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

            if (displayString.Item1)
            {
                Console.Write(s);
            }
            else
            {
                Console.ResetColor();
                //Console.Write(" # ");
                Console.Write(s);
            }

            Console.ResetColor();
        }
        Console.WriteLine();
    }
}