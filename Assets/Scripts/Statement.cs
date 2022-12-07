using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Statement
{
    public abstract Statement GetCopy();
    public abstract void Run();
    public abstract GameObject[] GetCodeLineObjects();
}
