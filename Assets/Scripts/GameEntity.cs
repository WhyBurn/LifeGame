using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity
{
    private Vector2Int position;
    private string tag;
    private Data.Direction facing;
    private Sprite sprite;
    private EntityObject displayObject;

    public GameEntity(string t, Sprite s)
    {
        tag = t;
        sprite = s;
        facing = Data.Direction.up;
        position = new Vector2Int();
    }
    
    public virtual GameEntity GetCopy()
    {
        GameEntity copy = new GameEntity(tag, sprite);
        return (copy);
    }

    public int XPos
    {
        get { return (position.x); }
        set 
        { 
            position.x = value;
            if(displayObject != null)
            {
                displayObject.MoveTo(position, facing);
            }
        }
    }
    public int YPos
    {
        get { return (position.y); }
        set 
        { 
            position.y = value;
            if (displayObject != null)
            {
                displayObject.MoveTo(position, facing);
            }
        }
    }
    public Vector2Int Position
    {
        get { return (position); }
        set 
        { 
            position = value;
            if (displayObject != null)
            {
                displayObject.MoveTo(position, facing);
            }
        }
    }
    public Data.Direction Facing
    {
        get { return (facing); }
        set { facing = value; }
    }
    public Sprite Sprite
    {
        get { return (sprite); }
    }
    public string Tag
    {
        get { return (tag); }
    }

    public void SetDisplayObject(EntityObject d)
    {
        displayObject = d;
    }

    public virtual void Interact() {}
}
