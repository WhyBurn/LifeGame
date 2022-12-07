using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftStatement : Statement
{
    private string[] possibleTags;
    private string[] possibleDirections;
    private string tag;
    private string direction;

    public ShiftStatement(string[] tags, string[] directions)
    {
        possibleTags = tags;
        possibleDirections = directions;
        tag = tags[0];
        direction = directions[0];
    }

    public override Statement GetCopy()
    {
        return (new ShiftStatement(possibleTags, possibleDirections));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().ShiftEntity(tag, direction);
    }
}
