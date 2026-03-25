// See https://aka.ms/new-console-template for more information


using System.Security.Cryptography.X509Certificates;
namespace Minesweeper.Core;
public static class Program
{
    public static void Main(string[] args)
    {
        // Get seed/set seed
        //Console.Write("Input seed, leave blank for random: ");
        //string? response = Console.ReadLine();
        int seed = 12345;
        //int.TryParse(response, out seed);
        if (seed == 0)
            seed = (int)DateTime.Now.Ticks;

        Map map = new Map(1);
        map.GenMap(seed);
        GenMap.Generate(map._map);
    }
}
