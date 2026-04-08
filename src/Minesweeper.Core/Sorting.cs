namespace Minesweeper.Core;

public static class Sorting
{
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