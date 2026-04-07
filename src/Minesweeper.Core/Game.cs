using System.Diagnostics;
using Minesweeper.Core;
/*
public class Game
{
    private int _size;
    private int _seconds;
    private int _moves;
    private int _seed;
    private bool win;

    public int GameLoop() // 1 is loss, 0 is win
    {
        (int, int) sizeAndSeed = Menu.ProcessPrereqs();
        int size = sizeAndSeed.Item1;
        int seed = sizeAndSeed.Item2;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string currentTime = $"{stopwatch.Elapsed.TotalSeconds}";

        Menu.HomeScreen(seed, currentTime);

        Map map = new Map(size);
        map.GenMap(seed);

        while (true)
        {
            currentTime = $"{Math.Round(stopwatch.Elapsed.TotalSeconds, 2)} second(s)";
            Menu.HomeScreen(seed, currentTime);
            GenMap.Display(map.mapMask, map._mapSize);
            Menu.Display(map); 
        }
        // return 1;
    }
}
*/