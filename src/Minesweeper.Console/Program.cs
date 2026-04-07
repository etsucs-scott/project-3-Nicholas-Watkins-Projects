// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System;
namespace Minesweeper.Core;

public static class Program
{
    internal static void Main(string[] args)
    {
        while (true)
        {
            System.Console.Clear();
            (int, int) sizeAndSeed = Menu.ProcessPrereqs();
            int size = sizeAndSeed.Item1;
            int seed = sizeAndSeed.Item2;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string currentTime = $"{stopwatch.Elapsed.TotalSeconds}";

            Menu.HomeScreen(seed, currentTime);

            Map map = new Map(size);
            map.GenMap(seed);

            int moves = 0;
            bool notWin = true;

            while (notWin)
            {
                currentTime = $"{Math.Round(stopwatch.Elapsed.TotalSeconds, 2)} second(s)";
                Menu.HomeScreen(seed, currentTime);
                GenMap.Display(map.mapMask, map._mapSize);
                bool isBlownUp = Menu.Display(map);
                if (isBlownUp)
                    break;
                moves += 1;
                notWin = !map.CheckedHiddenEmpty(); // If not empty == not win, if empty is win
            }
            if (!notWin)
            {
                CSVTest csvTest = new CSVTest();
                csvTest.Update(map._mapSize, (float)Math.Round(stopwatch.Elapsed.TotalSeconds, 2), moves, seed);
                csvTest.Save();
                Console.WriteLine("You won! Please press enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
