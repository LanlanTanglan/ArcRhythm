using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : IState<T>
{

    public IStateMachine<T> StateMachine { get; }
    public Enum StateID { get; set;}
    private Dictionary<Condition<T>, Enum> conditionMap = new Dictionary<Condition<T>, Enum>();
    public T Subject { get; }

    public State(IStateMachine<T> stateMachine, Enum stateID)
    {
        this.StateMachine = stateMachine;
        this.StateID = stateID;
        this.Subject = stateMachine.Subject;
    }

    public IState<T> AddCondition(Condition<T> conditionID, Enum stateID)
    {
        if (conditionMap.ContainsKey(conditionID))
        {
            Debug.LogWarning($"条件{conditionID}被重复添加，一个条件只能对应一种次态！");
            return this;
        }


        conditionMap.Add(conditionID, stateID);
        return this;
    }

    public virtual void Awake() { }
    public virtual void Enter() { }

    public virtual void Leave() { }

    //遍历条件，看是否成立
    public Enum TransitionCheck()
    {
        foreach (var map in conditionMap)
        {
            if (map.Key.ConditionCheck(Subject))
            {
                return map.Value;
            }
        }
        return null;
    }

    public virtual void Update() { 
        
    }
}