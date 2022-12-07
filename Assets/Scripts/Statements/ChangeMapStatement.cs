using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMapStatement : Statement
{
    private int[] possibleMapIndexs;
    private int mapIndex;

    public ChangeMapStatement(int[] mapIndexs)
    {
        possibleMapIndexs = mapIndexs;
        mapIndex = 0;
    }

    public override Statement GetCopy()
    {
        return (new ChangeMapStatement(possibleMapIndexs));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().LoadMap(possibleMapIndexs[mapIndex]);
    }

    public override GameObject[] GetCodeLineObjects()
    {
        GameObject[] mapObject = new GameObject[] { GameObject.Instantiate(Resources.Load<GameObject>("ChangeMap")) };
        mapObject[0].GetComponent<ChangeMapHandler>().SetupMapIndexDropdown(possibleMapIndexs, mapIndex, SetMapIndex);
        return (mapObject);
    }

    public void SetMapIndex(int index)
    {
        if(index < 0 || index >= possibleMapIndexs.Length)
        {
            return;
        }
        mapIndex = index;
    }
}
