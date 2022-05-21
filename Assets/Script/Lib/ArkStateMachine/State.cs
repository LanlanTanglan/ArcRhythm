using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 状态类
/// </summary>
/// <typeparam name="T"></typeparam>
public class State<T>
{
    /// <summary>
    /// 连接次态
    /// </summary>
    public Dictionary<Condition<T>, State<T>> nextStates = new Dictionary<Condition<T>, State<T>>();

    public State<T> AddCondition(Condition<T> c, State<T> s)
    {
        if (!nextStates.ContainsKey(c))
            nextStates.Add(c, s);
        return this;
    }

    /// <summary>
    /// 依次检查下个状态是否可以转换
    /// </summary>
    /// <returns></returns>
    public State<T> CheckNextState(T mainObj)
    {
        foreach (var map in nextStates)
        {
            if (map.Key.CheckCondition(mainObj))
            {
                return map.Value;
            }
        }
        return null;
    }
}