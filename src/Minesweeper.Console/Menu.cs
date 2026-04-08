using System.Drawing;
using Minesweeper.Core;

public static class Menu
{
    public static int Display(Map map) // bool -> isblownUp
    {
        Console.Write("> ");
        string? response = Console.ReadLine();

        string[] responsePieces = response.Split(" ");
        if (responsePieces[0] == "r")
        {
            int x;
            int y;
            int.TryParse(responsePieces[1], out x);
            int.TryParse(responsePieces[2], out y);
            bool blownUp = map.Reveal((x, y));
            if (blownUp)
            {
                Console.WriteLine("You have hit a bomb! Please hit enter to continue...");
                Console.ReadLine();
                return 1; // Bomb hit
            }
        }
        if (responsePieces[0] == "f")
        {
            int x;
            int y;
            int.TryParse(responsePieces[1], out x);
            int.TryParse(responsePieces[2], out y);
            map.Replace((x, y), " f ");
        }
        if (responsePieces[0] == "q")
            return -1; // Quit code
        if (responsePieces[0] == "win")
            return -2; // Insta win/set reveal board empty

        return 0; // Fine
    }
    public static int GetSeed()
    {
        System.Console.Write("\nEnter a seed or leave blank random (time based)\n?: ");
        int seed;
        string? response;
        response = System.Console.ReadLine();
        int.TryParse(response, out seed);

        if (seed == 0)
        {
            seed = (int)DateTime.Now.Ticks;
            seed = 12345; // Can easily edit to change default seed | Set to 12345 for testing 
        }

        return seed;
    }
    public static int GetSize()
    {
        System.Console.Write("Enter a map size\n1: 8x8 10 mines\t\t2: 12x12 25 mines\t\t3: 16x16 40 mines\n?:  ");
        int size;
        string? response = System.Console.ReadLine();
        int.TryParse(response, out size);
        if (size == 1 || size == 2 || size == 3)
            return size;
        else
            return 0;
    }
    public static void HomeScreen(int seed, string time)
    {
        System.Console.Clear();
        System.Console.WriteLine("\nMINESWEEPER\n\tBy Nick W");
        System.Console.WriteLine("\nCommands: r row col | f row col | q\n");
        System.Console.WriteLine($"Seed: {seed} | Time: {time}");
    }
    public static (int, int) ProcessPrereqs() // Get size and seed from play and handle errors
    {
        int size;
        while (true)
        {
            size = GetSize();
            if (size != 0)
                break;
            else
            {
                System.Console.WriteLine("\nPlease enter a correct input (1, 2, 3)...");
            }
        }
        int seed = GetSeed();
        return (size, seed);
    }
}