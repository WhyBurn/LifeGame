using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStartPos
{
    private GameEntity entity;
    private Vector2Int startPos;

    public EntityStartPos(GameEntity e, Vector2Int p)
    {
        entity = e;
        startPos = p;
    }

    public GameEntity Entity
    {
        get { return (entity); }
    }
    public Vector2Int StartPos
    {
        get { return (startPos); }
    }
}
