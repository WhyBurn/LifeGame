using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SameSpaceStatement : BoolStatement
{
    private string[] possibleLeftTags;
    private string[] possibleRightTags;
    private int leftTag;
    private int rightTag;

    public SameSpaceStatement(string[] left, string[] right)
    {
        possibleLeftTags = left;
        possibleRightTags = right;
        leftTag = 0;
        rightTag = 0;
    }

    public override BoolStatement GetCopy()
    {
        return (new SameSpaceStatement(possibleLeftTags, possibleRightTags));
    }

    public override bool IsTrue()
    {
        return (GameControllerObject.GetGCO().SameSpace(possibleLeftTags[leftTag], possibleRightTags[rightTag]));
    }

    public override GameObject GetBoolStatementObjects()
    {
        GameObject sameSpaceObject = GameObject.Instantiate(Resources.Load<GameObject>("SameSpace"));
        sameSpaceObject.GetComponent<SameSpaceHandler>().SetupLeftDropdown(possibleLeftTags, leftTag, SetLeft);
        sameSpaceObject.GetComponent<SameSpaceHandler>().SetupRightDropdown(possibleRightTags, rightTag, SetRight);
        return (sameSpaceObject);
    }

    public void SetLeft(int index)
    {
        if(index < 0 || index >= possibleLeftTags.Length)
        {
            return;
        }
        leftTag = index;
    }
    public void SetRight(int index)
    {
        if (index < 0 || index >= possibleRightTags.Length)
        {
            return;
        }
        rightTag = index;
    }
}
