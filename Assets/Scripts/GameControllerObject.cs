using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerObject
{
    private static GameControllerObject gco;

    public static GameControllerObject GetGCO()
    {
        if(gco == null)
        {
            gco = new GameControllerObject();
        }
        return (gco);
    }

    private Map currentMap;
    private DisplayHandler displayer;
    private GameEntity[] entities;

    public void SetDisplayerReference(DisplayHandler d)
    {
        displayer = d;
    }

    public void PlayerDirectionInput(Data.Direction direction)
    {
        for(int i = 0; i < entities.Length; ++i)
        {

        }
    }

    public void PlayerInteractInput()
    {

    }
}
