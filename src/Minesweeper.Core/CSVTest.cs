
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
public class CSVTest
{
    private int _size;
    private float _seconds;
    private int _moves;
    private int _seed;
    private string _timeStamp;
    private string saveFile = "../../save.csv";
    private List<string> saveInfo;
    private List<string> size8;
    private List<string> size12;
    private List<string> size16;









    public void Save()
    {
        _timeStamp = DateTime.Now.ToString("MM/dd/yy hh:mm tt");
        File.AppendAllText(saveFile, $"{_size},{_seconds},{_moves},{_seed},{_timeStamp}\n");
    }



    public void Retrieve()
    {
        saveInfo = File.ReadAllLines(saveFile).ToList();
        saveInfo.RemoveAt(0); // Make sure the header is not in list
    }








    public void Update(int size, float seconds, int moves, int seed)
    {
        _size = size;
        _seconds = seconds;
        _moves = moves;
        _seed = seed;
    }
}