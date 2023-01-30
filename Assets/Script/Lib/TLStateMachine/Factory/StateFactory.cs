using System;
using System.Reflection;

public class StateFactory
{
    public static State<T> GetState<T>(object stateMachine, Enum stateID)
    {
        Type stateType = Enum2TypeFactory.GetType(stateID);

        ConstructorInfo ctor = stateType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0];

        return (State<T>)ctor.Invoke(new object[] { stateMachine, stateID });
    }
}