using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public enum RuleType { onMove = 0, onInteract = 1};

    private Statement[] statements;
    private RuleType ruleType;

    public Rule(Statement[] s, RuleType t)
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
        Rule copy = new Rule(sCopy, ruleType);
        return (copy);
    }
}
