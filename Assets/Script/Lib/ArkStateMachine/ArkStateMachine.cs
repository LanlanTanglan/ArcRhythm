using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArkStateMachine<T>
{
    /// <summary>
    /// 状态机所附加对象
    /// </summary>
    public T mainObj;

    /// <summary>
    /// 当前状态
    /// </summary>
    public State<T> currentState;

    /// <summary>
    /// 当前状态机所拥有的所有状态
    /// </summary>
    public List<State<T>> states;

    /// <summary>
    /// 添加状态
    /// </summary>
    public ArkStateMachine<T> AddState(State<T> state)
    {
        if (!states.Contains(state))
        {
            states.Add(state);
        }
        return this;
    }

    

}