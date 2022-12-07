using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAtStatement : Statement
{
    private string[] possibleTags;
    private int[] possibleXs;
    private int[] possibleYs;
    private int tag;
    private int x;
    private int y;

    public PlaceAtStatement(string[] tags, int[] xs, int[] ys)
    {
        possibleTags = tags;
        possibleXs = xs;
        possibleYs = ys;
        tag = 0;
        x = 0;
        y = 0;
    }

    public override Statement GetCopy()
    {
        return (new PlaceAtStatement(possibleTags, possibleXs, possibleYs));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().PlaceAt(possibleTags[tag], possibleXs[x], possibleYs[y]);
    }

    public override GameObject[] GetCodeLineObjects()
    {
        GameObject[] placeObject = new GameObject[] { GameObject.Instantiate(Resources.Load<GameObject>("PlaceAt")) };
        placeObject[0].GetComponent<PlaceAtHandler>().SetupTagDropdown(possibleTags, tag, SetTag);
        placeObject[0].GetComponent<PlaceAtHandler>().SetupXDropdown(possibleXs, tag, SetTag);
        placeObject[0].GetComponent<PlaceAtHandler>().SetupYDropdown(possibleYs, tag, SetTag);
        return (placeObject);
    }

    public void SetTag(int index)
    {
        if (index < 0 || index >= possibleTags.Length)
        {
            return;
        }
        tag = index;
    }

    public void SetX(int index)
    {
        if (index < 0 || index >= possibleXs.Length)
        {
            return;
        }
        x = index;
    }

    public void SetY(int index)
    {
        if (index < 0 || index >= possibleYs.Length)
        {
            return;
        }
        y = index;
    }
}
