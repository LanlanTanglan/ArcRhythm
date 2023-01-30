using System;
using System.Reflection;

public class ConditionFactory
{
    public static Condition<T> GetCondition<T>(Enum conditionID)
    {
        Type conditionType = Enum2TypeFactory.GetType(conditionID);

        ConstructorInfo ctor = conditionType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0];

        return (Condition<T>)ctor.Invoke(new object[] { conditionID });
    }
}