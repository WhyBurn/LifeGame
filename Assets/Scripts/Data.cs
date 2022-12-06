using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public enum Direction { up = 0, right = 1, down = 2, left = 3 };

    public static int displayScale = 32;
    private static Map[] maps;

    public static int NumMaps
    {
        get { return (maps.Length); }
    }

    public static Map GetMap(int index)
    {
        if (index < 0 || index >= NumMaps)
        {
            return (null);
        }
        return (maps[index]);
    }

    public static void LoadData()
    {
        LoadMaps();
    }

    private static void LoadMaps()
    {

    }
}
