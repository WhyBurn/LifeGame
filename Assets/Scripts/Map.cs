using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private Vector2Int bounds;
    private EntityStartPos[] entityStarts;

    public Map(Vector2Int b, EntityStartPos[] e)
    {
        bounds = b;
        entityStarts = e;
    }

    public Vector2Int Bounds
    {
        get { return (bounds); }
    }
    public int X
    {
        get { return (bounds.x); }
    }
    public int Y
    {
        get { return (bounds.y); }
    }

    public int NumEntities
    {
        get { return (entityStarts.Length); }
    }

    public EntityStartPos GetEntityStart(int index)
    {
        if(index < 0 || index >= NumEntities)
        {
            return (null);
        }
        return (entityStarts[index]);
    }
}
