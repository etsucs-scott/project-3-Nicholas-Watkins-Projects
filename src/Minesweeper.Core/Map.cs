namespace Minesweeper.Core;

public class Map
{
    private int _mapSize;
    private int _mapBombs;
    public List<string> _map { get; private set; } = new List<string>(); 

    public Map(int mapType)
    {
        ChangeSize(mapType);
    }

    public int ChangeSize(int mapType) // Returns 1 if 1,2,3 not chosen
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
                    _map.Add(" b ");
                    bombCoords.Remove((x, y));
                }
                else
                {
                    _map.Add(" . ");
                }
                if (x == _mapSize - 1)
                {
                    _map.Add("\n");
                }
            }
        }

        // Check positions on map for bomb amounts per tile
        for (int y = 0; y < _mapSize; y++)
        {
            for (int x = 0; x < _mapSize; x++)
            {
                int posSym = CheckCoord(_map, (x, y), _mapSize);
                if (_map[(_mapSize + 1) * y + x] != " b ")
                {
                    if (posSym != 0)
                        _map[(_mapSize + 1) * y + x] = $" {posSym} ";
                }
            }
        }
    }

    public int CheckCoord(List<string> map, (int, int) coords, int mapSize)
    {
        int bombAmount = 0;
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
                    if (map[(mapSize + 1) * (coords.Item2 + y) + (coords.Item1 + x)] == " b ")
                        bombAmount += 1;
                }
            }
        }
        return bombAmount;
    }

}