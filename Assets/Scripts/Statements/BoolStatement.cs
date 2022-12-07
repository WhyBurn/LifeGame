using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoolStatement
{
    public abstract BoolStatement GetCopy();
    public abstract bool IsTrue();
    public abstract GameObject GetBoolStatementObjects();
}
