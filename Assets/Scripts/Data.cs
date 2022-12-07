using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Data
{
    public enum Direction { up = 0, right = 1, down = 2, left = 3 };

    public static float displayScale = .33f;
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
        StreamReader reader = new StreamReader("Maps.csv");
        string line = reader.ReadLine();
        List<Map> m = new List<Map>();
        while((line = reader.ReadLine()) != null)
        {
            string[] row = line.Split(",");
            m.Add(new Map(new Vector2Int(int.Parse(row[1]), int.Parse(row[2])), new EntityStartPos[] { new EntityStartPos(new Player(Resources.Load<Sprite>("Player")), new Vector2Int()) }));
        }
        maps = new Map[m.Count];
        for(int i = 0; i < maps.Length; ++i)
        {
            maps[i] = m[i];
        }
    }
}
