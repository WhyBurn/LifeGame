using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEachStatement : Statement
{
    private string[] possibleTags;
    private int entityTag;
    private string variableName;
    private Statement loopStatement;

    public ForEachStatement(string[] tags, string variable, Statement loop)
    {
        possibleTags = tags;
        entityTag = 0;
        variableName = variable;
        loopStatement = loop;
    }

    public override Statement GetCopy()
    {
        return (new ForEachStatement(possibleTags, variableName, loopStatement.GetCopy()));
    }

    public override void Run()
    {
        GameControllerObject.GetGCO().RunForEachLoop(possibleTags[entityTag], variableName, loopStatement);
    }

    public override GameObject[] GetCodeLineObjects()
    {
        GameObject[] loopObjects = loopStatement.GetCodeLineObjects();
        GameObject[] forEachObject = new GameObject[loopObjects.Length + 1];
        forEachObject[0] = GameObject.Instantiate(Resources.Load<GameObject>("Foreach"));
        forEachObject[0].GetComponent<ForEachHandler>().SetupTagDropdown(possibleTags, entityTag, SetTag);
        forEachObject[0].GetComponent<ForEachHandler>().SetupText(variableName);
        for(int i = 0; i < loopObjects.Length; ++i)
        {
            forEachObject[i + 1] = loopObjects[i];
        }
        return (forEachObject);
    }

    public void SetTag(int index)
    {
        if(index < 0 || index >= possibleTags.Length)
        {
            return;
        }
        entityTag = index;
    }
}
