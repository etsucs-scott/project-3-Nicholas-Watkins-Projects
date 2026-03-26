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

        while (true)
        {
            currentTime = $"{Math.Round(stopwatch.Elapsed.TotalSeconds, 2)} second(s)";
            Menu.HomeScreen(seed, currentTime);
            GenMap.Display(map.map);
            Menu.Display(); 
        }
    }
}
