using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEachStatement : Statement
{
    private string[] possibleTags;
    private string entityTag;
    private string variableName;
    private Statement loopStatement;

    public ForEachStatement(string[] tags, string variable, Statement loop)
    {
        possibleTags = tags;
        entityTag = tags[0];
        variableName = variable;
        loopStatement = loop;
    }

    public override Statement GetCopy()
    {
        return (new ForEachStatement(possibleTags, variableName, loopStatement.GetCopy()));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().RunForEachLoop(entityTag, variableName, loopStatement);
    }
}
