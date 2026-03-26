using System.Collections.Generic;
namespace Minesweeper.Core;

public class Map
{
    private int _mapSize;
    private int _mapBombs;
    public List<string> map { get; private set; } = new List<string>(); 

    public int SetSize(int mapType) // Returns 1 if 1,2,3 not chosen
    {
        switch (mapType)
        {
            case 1:
                _mapSize = 8;
                _mapBombs = 10;
                break;
            case 2:
                _mapSize = 12;
                _mapBombs = 25;
                break;
            case 3:
                _mapSize = 16;
                _mapBombs = 40;
                break;
            default:
                return 1;
        }
        return 0;
    }

    public void GenMap(int seed)
    {
        Random coordGen = new Random(seed);
        List<(int, int)> bombCoords = new List<(int, int)>();

        // Bomb generation for map (coords)
        for (int i = 0; i < _mapBombs; i++)
        {
            int x = coordGen.Next(0, _mapSize - 1);
            int y = coordGen.Next(0, _mapSize - 1);

            bombCoords.Add((x, y));
        }

        // Gen the map using bomb positions
        for (int y = 0; y < _mapSize; y++)
        {
            for (int x = 0; x < _mapSize; x++)
            {
                if (bombCoords.Contains((x, y)))
                {
                    map.Add(" b ");
                    bombCoords.Remove((x, y));
                }
                else
                {
                    map.Add(" . ");
                }
                if (x == _mapSize - 1)
                {
                    map.Add("\n");
                }
            }
        }

        Dictionary<string, Dictionary<int, List<(int, int)>>> bombCheck = new Dictionary<string, Dictionary<int, List<(int, int)>>>();
        Dictionary<int, List<(int, int)>> amountAndCoords = new Dictionary<int, List<(int, int)>>();

        // Check positions on map for bomb amounts per tile
        for (int y = 0; y < _mapSize; y++)
        {
            for (int x = 0; x < _mapSize; x++)
            {
                amountAndCoords[0] = new List<(int, int)>();
                bombCheck[" b "] = amountAndCoords;
                Dictionary<string, Dictionary<int, List<(int, int)>>> result = CheckCoord(map, (x, y), _mapSize, bombCheck);
                bombCheck[" b "] = result[" b "]; 

                if (map[(_mapSize + 1) * y + x] != " b ")
                {
                    if (result[" b "].Keys.ToList()[0] != 0)
                        map[(_mapSize + 1) * y + x] = $" {result[" b "].Keys.ToList()[0]} ";
                }
            }
        }
    }

    public static Dictionary<string, Dictionary<int, List<(int, int)>>> CheckCoord(List<string> map, (int, int) coords, int mapSize, Dictionary<string, Dictionary<int, List<(int, int)>>> checks)
    {
        int skipX = 2;
        int skipY = 2;

        // Check if need to skip coord based on boundary
        if (coords.Item1 == 0)
            skipX = -1;
        if (coords.Item2 == 0)
            skipY = -1;
        if (coords.Item1 == mapSize - 1)
            skipX = 1;
        if (coords.Item2 == mapSize - 1)
            skipY = 1;

        // Check coord 
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                bool checkCoord = true;

                if (x == skipX)
                    checkCoord = false;
                if (y == skipY)
                    checkCoord = false;
                if ((x + coords.Item1, y + coords.Item2) == coords)
                    checkCoord = false;

                if (checkCoord)
                {
                    foreach (string val in checks.Keys)
                    {
                        if (map[(mapSize + 1) * (coords.Item2 + y) + (coords.Item1 + x)] == val) 
                        {
                            Dictionary<int, List<(int, int)>> amountAndCoords = checks[val];
                            List<int> amount = checks[val].Keys.ToList();
                            List<(int, int)> aCoords = checks[val][amount[0]];

                            amount[0] += 1;
                            aCoords.Add((x, y));

                            Dictionary<int, List<(int,int)>> newAmountAndCoords = new Dictionary<int, List<(int, int)>>();
                            newAmountAndCoords[amount[0]] = aCoords;
                            
                            checks[val] = newAmountAndCoords;
                        }
                    }
                }
            }
        }
        return checks;
    }

}