using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndOrStatement : BoolStatement
{
    public enum AndOrType { and = 0, or = 1 };

    private AndOrType[] possibleTypes;
    private AndOrType type;
    private BoolStatement leftStatement;
    private BoolStatement rightStatement;

    public AndOrStatement(AndOrType[] t, BoolStatement left, BoolStatement right)
    {
        possibleTypes = t;
        type = t[0];
        leftStatement = left;
        rightStatement = right;
    }

    public override BoolStatement GetCopy()
    {
        return (new AndOrStatement(possibleTypes, leftStatement.GetCopy(), rightStatement.GetCopy()));
    }

    public override bool IsTrue()
    {
        if(type == AndOrType.and)
        {
            return (leftStatement.IsTrue() && rightStatement.IsTrue());
        }
        else
        {
            return (leftStatement.IsTrue() || rightStatement.IsTrue());
        }
    }
}
