using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStatement : Statement
{
    private string[] possibleTags;
    private string[] possibleDirections;
    private int tag;
    private int direction;

    public MoveStatement(string[] tags, string[] directions)
    {
        possibleTags = tags;
        possibleDirections = directions;
        tag = 0;
        direction = 0;
    }

    public override GameObject[] GetCodeLineObjects()
    {
        GameObject[] moveObject = new GameObject[] { GameObject.Instantiate(Resources.Load<GameObject>("Move")) };
        moveObject[0].GetComponent<MoveHandler>().SetupDirectionDropdown(possibleDirections, direction, SetDirection);
        moveObject[0].GetComponent<MoveHandler>().SetupTagDropdown(possibleTags, tag, SetTag);
        return (moveObject);
    }

    public override Statement GetCopy()
    {
        return (new MoveStatement(possibleTags, possibleDirections));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().MoveEntity(possibleTags[tag], possibleDirections[direction]);
    }

    public void SetDirection(int index)
    {
        if(index < 0 || index >= possibleDirections.Length)
        {
            return;
        }
        direction = index;
    }

    public void SetTag(int index)
    {
        if (index < 0 || index >= possibleTags.Length)
        {
            return;
        }
        tag = index;
    }
}
