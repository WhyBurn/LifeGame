using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftStatement : Statement
{
    private string[] possibleTags;
    private string[] possibleDirections;
    private int tag;
    private int direction;

    public ShiftStatement(string[] tags, string[] directions)
    {
        possibleTags = tags;
        possibleDirections = directions;
        tag = 0;
        direction = 0;
    }

    public override Statement GetCopy()
    {
        return (new ShiftStatement(possibleTags, possibleDirections));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().ShiftEntity(possibleTags[tag], possibleDirections[direction]);
    }

    public override GameObject[] GetCodeLineObjects()
    {
        GameObject[] moveObject = new GameObject[] { GameObject.Instantiate(Resources.Load<GameObject>("Shift")) };
        moveObject[0].GetComponent<MoveHandler>().SetupDirectionDropdown(possibleDirections, direction, SetDirection);
        moveObject[0].GetComponent<MoveHandler>().SetupTagDropdown(possibleTags, tag, SetTag);
        return (moveObject);
    }

    public void SetDirection(int index)
    {
        if (index < 0 || index >= possibleDirections.Length)
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
