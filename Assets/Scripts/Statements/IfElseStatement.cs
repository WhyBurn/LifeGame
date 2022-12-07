using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfElseStatement : Statement
{
    private BoolStatement check;
    private Statement nextStatement;
    private Statement alternateStatement;

    public IfElseStatement(BoolStatement c, Statement next, Statement alt)
    {
        check = c;
        nextStatement = next;
        alternateStatement = alt;
    }

    public override Statement GetCopy()
    {
        Statement alt = null;
        if(alternateStatement != null)
        {
            alt = alternateStatement.GetCopy();
        }
        return (new IfElseStatement(check.GetCopy(), nextStatement.GetCopy(), alt)); ;
    }

    public override void Run()
    {
        if(check.IsTrue())
        {
            nextStatement.Run();
        }
        else if(alternateStatement != null)
        {
            alternateStatement.Run();
        }
    }

    public override GameObject[] GetCodeLineObjects()
    {
        GameObject[] nextObjects = nextStatement.GetCodeLineObjects();
        GameObject[] alternateObjects = new GameObject[0];
        if(alternateStatement != null)
        {
            GameObject[] a = alternateStatement.GetCodeLineObjects();
            alternateObjects = new GameObject[a.Length + 1];
            alternateObjects[0] = GameObject.Instantiate(Resources.Load<GameObject>("Else"));
            for(int i = 0; i < a.Length; ++i)
            {
                alternateObjects[i + 1] = a[i];
            }
        }
        GameObject[] codeObjects = new GameObject[nextObjects.Length + alternateObjects.Length + 1];
        codeObjects[0] = GameObject.Instantiate(Resources.Load<GameObject>("If"));
        GameObject boolObject = check.GetBoolStatementObjects();
        boolObject.transform.SetParent(codeObjects[0].transform);
        boolObject.transform.localPosition = new Vector3(30, 0, 0);
        for(int i = 0; i < nextObjects.Length; ++i)
        {
            codeObjects[i + 1] = nextObjects[i];
        }
        for(int i = 0; i < alternateObjects.Length; ++i)
        {
            codeObjects[i + 1 + nextObjects.Length] = alternateObjects[i];
        }
        return (codeObjects);
    }
}
