namespace Minesweeper.Core;

public class Seed
{
    private int _seed;
    private Random _random;
    public void SetSeed(int seed)
    {
        _seed = seed;
        _random = new Random(seed); 
    }
}