using System.Collections.Generic;
using System.Data;
namespace Minesweeper.Core;

/// <summary>
/// A map object that holds the map and mask and the ability to change
/// </summary> 
public class Map
{
    public int _mapSize { get; private set; }
    private int _mapBombs;
    public List<string> map { get; private set; } = new List<string>();
    public List<string> mapMask { get; private set;} = new List<string>();
    private List<(int, int)> _hiddenSpaces = new List<(int, int)>();

    /// <summary>
    /// Constructor function for map
    /// </summary>
    /// <param name="mapType">Numbers 1 - 3</param>
    /// <exception cref="ArgumentException">Will cause an exception if not 1-3</exception>
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

    /// <summary>
    /// Generates the Map and MapMask variable
    /// </summary>
    /// <param name="seed">The seed is any integer </param>
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

    /// <summary>
    /// Checks the coordinates around a coord in a 3x3 area and returns what it found based on the dictionary string passed in
    /// </summary>
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
    
    /// <summary>
    /// Replaces the coordinate in mapMask with the coordinate in the map and updates hidden spaces
    /// </summary>
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

    /// <summary>
    /// Replaces a coordinate on the mapMask with a symbol
    /// </summary>
    /// <param name="coord">a (x, y) coordinate pair</param>
    /// <param name="symbol">a string symbol like " b "</param>
    public void Replace((int, int) coord, string symbol)
    {
        int listPosition = (_mapSize + 1) * coord.Item2 + coord.Item1;
        if (mapMask[listPosition] == " . ") {}
        else if (mapMask[listPosition] == " f ")
            mapMask[listPosition] = " # ";
        else if (mapMask[listPosition] == " # ")
            mapMask[listPosition] = symbol;
    }

    /// <summary>
    /// Checks the count of _hiddenSpaces
    /// </summary>
    /// <returns>True if empty, false is not</returns>
    public bool CheckedHiddenEmpty()
    {
        if (_hiddenSpaces.Count == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Sets the _hiddenSpaces to nothing
    /// </summary>
    public void SetHiddenEmpty()
    {
        _hiddenSpaces = new List<(int, int)>();
    }
}