using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : GameEntity
{
    private Rule[] rules;

    public Computer(string t, Sprite s, Rule[] r) : base(t, s)
    {
        rules = r;
    }

    public int NumRules
    {
        get { return (rules.Length); }
    }

    public Rule GetRule(int index)
    {
        if(index < 0 || index >= rules.Length)
        {
            return (null);
        }
        return (rules[index]);
    }

    public override GameEntity GetCopy()
    {
        Rule[] rCopy = new Rule[rules.Length];
        for(int i = 0; i < rules.Length; ++i)
        {
            rCopy[i] = rules[i].GetCopy();
        }
        Computer copy = new Computer(Tag, Sprite, rCopy);
        return (copy);
    }
}
