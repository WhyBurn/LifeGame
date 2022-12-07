using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public enum RuleType { onMove = 0, onInteract = 1};

    private Statement[] statements;
    private RuleType ruleType;

    public Rule(RuleType t, Statement[] s)
    {
        statements = s;
        ruleType = t;
    }

    public RuleType Type
    {
        get { return (ruleType); }
    }

    public virtual Rule GetCopy()
    {
        Statement[] sCopy = new Statement[statements.Length];
        for(int i = 0; i < statements.Length; ++i)
        {
            sCopy[i] = statements[i].GetCopy();
        }
        Rule copy = new Rule(ruleType, sCopy);
        return (copy);
    }

    public void Run()
    {
        foreach(Statement statement in statements)
        {
            statement.Run();
        }
    }

    public GameObject[] GetCodeObjects()
    {
        List<GameObject> codeObjects = new List<GameObject>();
        for(int i = 0; i < statements.Length; ++i)
        {
            GameObject[] objects = statements[i].GetCodeLineObjects();
            for(int o = 0; o < objects.Length; ++o)
            {
                codeObjects.Add(objects[o]);
            }
        }
        GameObject[] codeObjectsArray = new GameObject[codeObjects.Count + 1];
        codeObjectsArray[0] = GameObject.Instantiate(Resources.Load<GameObject>(ruleType.ToString()));
        for(int i = 0; i < codeObjects.Count; ++i)
        {
            codeObjectsArray[i + 1] = codeObjects[i];
        }
        return (codeObjectsArray);
    }
}
