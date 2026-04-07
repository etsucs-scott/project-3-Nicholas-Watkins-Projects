
using static System.Runtime.InteropServices.JavaScript.JSType;
public class CSVTest
{
    private int _size;
    private float _seconds;
    private int _moves;
    private int _seed;
    private string _timeStamp;
    private string saveFile = "../../save.csv";

    public void Save()
    {
        _timeStamp = DateTime.Now.ToString("MM/dd/yy hh:mm tt");
        File.AppendAllText(saveFile, $"{_size},{_seconds},{_moves},{_seed},{_timeStamp}\n");
    }
    public void Retrieve(int saveFilePosition)
    {
        string[] saves = File.ReadAllLines(saveFile);
        string[] vars = saves[saveFilePosition].Split(",");
        _size = int.Parse(vars[0]);
        _seconds = float.Parse(vars[1]);
        _moves = int.Parse(vars[2]);
        _seed = int.Parse(vars[3]);
        _timeStamp = vars[4];
    }
    public void Show()
    {
        Console.WriteLine($"Size\tSeconds\tMoves\tSeed\tTimestamp\n{_size}\t{_seconds}\t{_moves}\t{_seed}\t{_timeStamp}");
    }
    public void UpdateTerm()
    {
        Console.Write("Size: ");
        _size = int.Parse(Console.ReadLine());
        Console.Write("Secs: ");
        _seconds = float.Parse(Console.ReadLine());
        Console.Write("Moves: ");
        _moves = int.Parse(Console.ReadLine());
        Console.Write("Seed: ");
        _seed = int.Parse(Console.ReadLine());
    }
    public void Update(int size, float seconds, int moves, int seed)
    {
        _size = size;
        _seconds = seconds;
        _moves = moves;
        _seed = seed;
    }
}