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
}
