using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameSpaceStatement : BoolStatement
{
    private string[] possibleLeftTags;
    private string[] possibleRightTags;
    private string leftTag;
    private string rightTag;

    public SameSpaceStatement(string[] left, string[] right)
    {
        possibleLeftTags = left;
        possibleRightTags = right;
        leftTag = left[0];
        rightTag = right[0];
    }

    public override BoolStatement GetCopy()
    {
        return (new SameSpaceStatement(possibleLeftTags, possibleRightTags));
    }

    public override bool IsTrue()
    {
        return (GameControllerObject.GetGCO().SameSpace(leftTag, rightTag));
    }
}
