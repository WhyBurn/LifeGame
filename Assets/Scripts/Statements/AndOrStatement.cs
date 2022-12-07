using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndOrStatement : BoolStatement
{
    public enum AndOrType { and = 0, or = 1 };

    private AndOrType[] possibleTypes;
    private int type;
    private BoolStatement leftStatement;
    private BoolStatement rightStatement;

    public AndOrStatement(AndOrType[] t, BoolStatement left, BoolStatement right)
    {
        possibleTypes = t;
        type = 0;
        leftStatement = left;
        rightStatement = right;
    }

    public override BoolStatement GetCopy()
    {
        return (new AndOrStatement(possibleTypes, leftStatement.GetCopy(), rightStatement.GetCopy()));
    }

    public override bool IsTrue()
    {
        if(possibleTypes[type] == AndOrType.and)
        {
            return (leftStatement.IsTrue() && rightStatement.IsTrue());
        }
        else
        {
            return (leftStatement.IsTrue() || rightStatement.IsTrue());
        }
    }

    public override GameObject GetBoolStatementObjects()
    {
        GameObject leftObject = leftStatement.GetBoolStatementObjects();
        GameObject rightObject = rightStatement.GetBoolStatementObjects();
        GameObject andOrObject = GameObject.Instantiate(Resources.Load<GameObject>("AndOr"));
        andOrObject.GetComponent<AndOrHandler>().SetupTypeDropdown(getOptions(), type, SetType);
        andOrObject.GetComponent<AndOrHandler>().SetupTransforms(leftObject, rightObject);
        return (andOrObject);
    }

    private string[] getOptions()
    {
        string[] options = new string[possibleTypes.Length];
        for(int i = 0; i < possibleTypes.Length; ++i)
        {
            options[i] = possibleTypes[i].ToString();
        }
        return (options);
    }

    public void SetType(int index)
    {
        if(index < 0 || index >= possibleTypes.Length)
        {
            return;
        }
        type = index;
    }
}
