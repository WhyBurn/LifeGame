using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAtStatement : Statement
{
    private string[] possibleTags;
    private int[] possibleXs;
    private int[] possibleYs;
    private string tag;
    private int x;
    private int y;

    public PlaceAtStatement(string[] tags, int[] xs, int[] ys)
    {
        possibleTags = tags;
        possibleXs = xs;
        possibleYs = ys;
        tag = tags[0];
        x = xs[0];
        y = ys[0];
    }

    public override Statement GetCopy()
    {
        return (new PlaceAtStatement(possibleTags, possibleXs, possibleYs));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().PlaceAt(tag, x, y);
    }
}
