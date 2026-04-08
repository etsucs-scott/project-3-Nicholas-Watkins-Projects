
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace Minesweeper.Core;

public class CSVTest
{
    private int _size;
    private float _seconds;
    private int _moves;
    private int _seed;
    private string _timeStamp;
    private string saveFile = "../../save.csv";
    private List<string> saveInfo;
    private List<string> size8 = new List<string>();
    private List<string> size12 = new List<string>();
    private List<string> size16 = new List<string>();
    private List<string> highscores = new List<string>();
    private void CheckSaveFile() // Creates the save file if it doesn't exist
    {
        if (!File.Exists(saveFile))
        {
            File.Create(saveFile).Close();
            File.AppendAllText(saveFile, "size,seconds,moves,seed,timestamp\n");
        }
    }
    private void OrganizeSaves()
    {
        List<string> sortedSaves = Sorting.SelectionSort(size8);
        size8 = sortedSaves;
        sortedSaves = Sorting.SelectionSort(size12);
        size12 = sortedSaves;
        sortedSaves = Sorting.SelectionSort(size16);
        size16 = sortedSaves;

        size8 = size8.Take(5).ToList();
        size12 = size12.Take(5).ToList();
        size16 = size16.Take(5).ToList();

        highscores.AddRange(size8);
        highscores.AddRange(size12);
        highscores.AddRange(size16);
    }
    private void Retrieve()
    {
        saveInfo = File.ReadAllLines(saveFile).ToList();
        saveInfo.RemoveAt(0); // Make sure the header is not in list
        foreach (string item in saveInfo)
        {
            SortToSize(item);
        }
    }
    private void SortToSize(string save)
    {
        string[] items = save.Split(",");
        if (items[0] == "8")
            size8.Add(save);
        if (items[0] == "12")
            size12.Add(save);
        if (items[0] == "16")
            size16.Add(save);
    }
    public void Update(int size, float seconds, int moves, int seed)
    {
        _size = size;
        _seconds = seconds;
        _moves = moves;
        _seed = seed;
    }
    public void Save() // Saves the file with highscores
    {
        CheckSaveFile();
        Retrieve();

        _timeStamp = DateTime.Now.ToString("MM/dd/yy hh:mm tt");
        string newSave = $"{_size},{_seconds},{_moves},{_seed},{_timeStamp}";
        SortToSize(newSave);

        OrganizeSaves();
        File.WriteAllText(saveFile, "size,seconds,moves,seed,timestamp\n");

        File.AppendAllLines(saveFile, highscores);
    }
}
