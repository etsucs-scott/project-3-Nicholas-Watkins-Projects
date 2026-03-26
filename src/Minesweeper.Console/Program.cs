// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Minesweeper.Console;
namespace Minesweeper.Core;
public static class Program
{
    internal static void Main(string[] args)
    {
        System.Console.Clear();
        (int, int) sizeAndSeed = Menu.ProcessPrereqs();
        int size = sizeAndSeed.Item1;
        int seed = sizeAndSeed.Item2;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string currentTime = $"{stopwatch.Elapsed.TotalSeconds}";
        Menu.HomeScreen(seed, currentTime);

        Map map = new Map();
        map.SetSize(size);
        map.GenMap(seed);

        bool loss = false;

        while (true)
        {
            currentTime = $"{Math.Round(stopwatch.Elapsed.TotalSeconds, 2)} second(s)";
            Menu.HomeScreen(seed, currentTime);
            GenMap.Display(map._map);
            (int, int, int) menOut = Menu.Display();
            
            if (menOut.Item3 == 0)
            {
                loss = map.reveal((menOut.Item1, menOut.Item2));
                if (loss)
                {
                    System.Console.WriteLine("You lost the game");
                }
            }
            if (menOut.Item3 == -2 || loss)
            {
                break;
            }

        }
    }
}
