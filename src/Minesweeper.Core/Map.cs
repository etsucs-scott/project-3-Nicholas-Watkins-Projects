using System.Collections.Generic;
using System.Data;
namespace Minesweeper.Core;

public class Map
{
    public int _mapSize { get; private set; }
    private int _mapBombs;
    public List<string> map { get; private set; } = new List<string>();
    public List<string> mapMask { get; private set;} = new List<string>();
    private List<(int, int)> _hiddenSpaces = new List<(int, int)>();

    public Map(int mapType)
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
                throw new ArgumentException("mapType has to be 1, 2, or 3!");
        }
    }

    public void GenMap(int seed)
    {
        Random coordGen = new Random(seed);
        List<(int, int)> bombCoords = new List<(int, int)>();

        // Bomb generation for map (coords)
        for (int i = 0; i < _mapBombs; i++)
        {
            bool coordMatch = true;
            int x = -1;
            int y = -1;
            while (coordMatch)
            {
                x = coordGen.Next(0, _mapSize);
                y = coordGen.Next(0, _mapSize);
                if (!bombCoords.Contains((x, y)))
                    coordMatch = false;
            }
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
                    mapMask.Add(" # ");
                    bombCoords.Remove((x, y));
                }
                else
                {
                    map.Add(" . ");
                    mapMask.Add(" # ");
                    _hiddenSpaces.Add((x, y));
                }
                if (x == _mapSize - 1)
                {
                    map.Add("\n");
                    mapMask.Add("\n");
                }
            }
        }

        Dictionary<string, List<(int, int)>> bombCheck = new Dictionary<string, List<(int, int)>>();
        List<(int, int)> amountAndCoords = new List<(int, int)>();

        // Check positions on map for bomb amounts per tile
        for (int y = 0; y < _mapSize; y++)
        {
            for (int x = 0; x < _mapSize; x++)
            {
                amountAndCoords = new List<(int, int)>();
                bombCheck[" b "] = amountAndCoords;
                Dictionary<string, List<(int, int)>> result = CheckCoord(map, (x, y), _mapSize, bombCheck);
                bombCheck[" b "] = result[" b "]; 

                if (map[(_mapSize + 1) * y + x] != " b ")
                {
                    if (result[" b "].Count != 0)
                        map[(_mapSize + 1) * y + x] = $" {result[" b "].Count} ";
                }
            }
        }
    }

    public static Dictionary<string, List<(int, int)>> CheckCoord(List<string> map, (int, int) coords, int mapSize, Dictionary<string, List<(int, int)>> checks)
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
                        if (val == "all")
                        {
                            List<(int, int)> valCoords = checks[val];

                            valCoords.Add((x + coords.Item1, y + coords.Item2));
                            
                            checks[val] = valCoords;
                        }

                        if (map[(mapSize + 1) * (coords.Item2 + y) + (coords.Item1 + x)] == val) 
                        {
                            List<(int, int)> valCoords = checks[val];

                            valCoords.Add((x + coords.Item1, y + coords.Item2));
                            
                            checks[val] = valCoords;
                        }
                    }
                }
            }
        }
        return checks;
    }
/*
    public void Reveal((int, int) coord)
    {
        int listPosition = (_mapSize + 1) * coord.Item2 + coord.Item1;
        mapMask[listPosition] = map[listPosition];
        map[listPosition] = " ! "; // Used space
        
        Dictionary<string, Dictionary<int, List<(int, int)>>> emptyCheck = new Dictionary<string, Dictionary<int, List<(int, int)>>>();
        Dictionary<int, List<(int, int)>> amountAndCoords = new Dictionary<int, List<(int, int)>>();

        amountAndCoords[0] = new List<(int, int)>();
        emptyCheck[" . "] = amountAndCoords;

        emptyCheck = CheckCoord(this.map, coord, this._mapSize, emptyCheck);
        amountAndCoords = emptyCheck[" . "];
        int amountOfDots = amountAndCoords.Keys.ToList()[0];
        List<(int, int)> dotCoords = amountAndCoords[amountOfDots];
        dotCoords.Remove(coord);

        if (amountOfDots != 0)
        {
            foreach (var dotCoord in dotCoords)
            {
                Reveal(dotCoord);
            }
        }
    }
*/
    public bool Reveal((int, int) fCoord)
    {
        int listPosition = (_mapSize + 1) * fCoord.Item2 + fCoord.Item1;
        string coordSymbol = map[listPosition];
        string maskCoordSymbol = mapMask[listPosition];

        if (maskCoordSymbol == " f ") { }
        else if (coordSymbol != " . ")
        {
            mapMask[listPosition] = map[listPosition];
            _hiddenSpaces.Remove(fCoord);
            if (coordSymbol == " b ")
            {
                return true; // is Blown up
            }
        }
        else
        {
            List<(int,int)> coordsProccessed = new List<(int, int)>();
            Queue<(int, int)> coordProcessing = new Queue<(int, int)>();
            coordProcessing.Enqueue(fCoord);

            while (true)
            {
                if (coordProcessing.Count == 0)
                    break;
                if(coordsProccessed.Contains(coordProcessing.Peek()))
                {
                    coordProcessing.Dequeue();
                }
                else
                {
                    (int, int) coord = coordProcessing.Dequeue();
                    int mapPos = (_mapSize + 1) * coord.Item2 + coord.Item1;
                    string coordMapSymbol = map[mapPos];

                    if (coordMapSymbol == " . ")
                    {
                        Dictionary<string, List<(int, int)>> checkedCoords = new Dictionary<string, List<(int, int)>>();
                        checkedCoords["all"] = new List<(int, int)>();
                        checkedCoords = CheckCoord(map, coord, _mapSize, checkedCoords);

                        foreach (string key in checkedCoords.Keys.ToList())
                        {
                            foreach ((int, int) coordinates in checkedCoords[key])
                            {
                                coordProcessing.Enqueue(coordinates);
                                _hiddenSpaces.Remove(coordinates);
                            }
                        } 
                        mapMask[mapPos] = map[mapPos];
                        coordsProccessed.Add(coord);
                    }
                    else
                    {
                        mapMask[mapPos] = map[mapPos];
                        coordsProccessed.Add(coord);
                    }
                }
            }
        }
        return false; // False is not blown up
    }

    public void Replace((int, int) coord, string symbol)
    {
        int listPosition = (_mapSize + 1) * coord.Item2 + coord.Item1;
        if (mapMask[listPosition] == " . ") {}
        else if (mapMask[listPosition] == " f ")
            mapMask[listPosition] = " # ";
        else if (mapMask[listPosition] == " # ")
            mapMask[listPosition] = symbol;
    }

    public bool CheckedHiddenEmpty()
    {
        if (_hiddenSpaces.Count == 0)
            return true;
        else
            return false;
    }

    public void SetHiddenEmpty()
    {
        _hiddenSpaces = new List<(int, int)>();
    }
}