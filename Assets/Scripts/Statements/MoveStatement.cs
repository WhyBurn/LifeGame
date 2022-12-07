using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStatement : Statement
{
    private string[] possibleTags;
    private string[] possibleDirections;
    private string tag;
    private string direction;

    public MoveStatement(string[] tags, string[] directions)
    {
        possibleTags = tags;
        possibleDirections = directions;
        tag = tags[0];
        direction = directions[0];
    }

    public override Statement GetCopy()
    {
        return (new MoveStatement(possibleTags, possibleDirections));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().MoveEntity(tag, direction);
    }
}
