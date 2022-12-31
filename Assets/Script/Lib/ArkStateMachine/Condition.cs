using System;
using UnityEngine;

public abstract class Condition<T>
{
    public Condition(Enum conditionId) { }
    //条件判断
    public abstract bool ConditionCheck(T subject);
}