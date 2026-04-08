namespace Minesweeper.Core;

/// <summary>
/// Contains sorting algorithms 
/// </summary>
public static class Sorting
{
    /// <summary>
    /// A selection sorting algorithm to sort a string of data from a CSV based off the time 
    /// </summary>
    /// <param name="data">List to be sorted</param>
    /// <returns>the sorted list</returns>
    public static List<string> SelectionSort(List<string> data)
    {
        for (int i = 0; i < data.Count - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < data.Count; j++)
            {
                if (float.Parse(data[j].Split(",")[1]) < float.Parse(data[minIndex].Split(",")[1]))
                {
                    minIndex = j;
                }
            }
            (data[i], data[minIndex]) = (data[minIndex], data[i]);
        }
        return data;
    }

}