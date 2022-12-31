using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

[Serializable]
public class FiniteStateMachine<T> : IStateMachine<T>
{
    public T Subject { get; set; }

    public Enum DefaultStateID { get; set; }
    private List<State<T>> _states = new List<State<T>>();
    private State<T> _currentState;

    private State<T> _GetState(Enum stateID)
    {
        return _states.Find(state => state.StateID.ToString() == stateID.ToString());
    }

    public FiniteStateMachine(T subject)
    {
        this.Subject = subject;
    }

    public IStateMachine<T> AddState(Enum stateID)
    {
        Debug.Log(stateID);
        if (_states.Find(state => state.StateID.ToString() == stateID.ToString())!=null)
            Debug.LogWarning($"尝试重复添加状态{stateID}");
        else
        {
            //工厂生成状态实例
            var addedState = (State<T>)StateFactory.GetState<T>(this, stateID);
            _states.Add(addedState);
        }
        return this;
    }

    public IStateMachine<T> AddTransition(Enum fromState, Enum toState, Enum condition)
    {
        var fState = _GetState(fromState);
        var tState = _GetState(toState);
        if (fState == null)
        {
            AddState(fromState);
            fState = _GetState(fromState);
        }
        if (tState == null)
        {
            AddState(toState);
            tState = _GetState(toState);
        }
        //工厂生成条件实例
        var addCondition = (Condition<T>)ConditionFactory.GetCondition<T>(condition);
        fState.AddCondition(addCondition, toState);
        return this;
    }

    public void ChangeState(Enum ts)
    {
        State<T> targetState = _GetState(ts);
        //触发现态切出动作
        _currentState.Leave();
        //触发次态切入动作
        targetState.Enter();
        //将次态设置为现态
        _currentState = targetState;
    }

    public IStateMachine<T> Open(Enum stateID)
    {
        var state = _GetState(stateID);
        try
        {
            var compState = (CompositeState<T>)state;

            return compState;
        }
        catch (Exception e)
        {
            Debug.LogError($"获取复合状态错误, ID: {stateID}");
            Debug.LogError($"错误类型：{e}");
            throw;
        }
    }

    public void Awake()
    {
        foreach (var state in _states)
        {
            //遍历状态列表初始化
            state.Awake();
        }
        _currentState = _GetState(DefaultStateID);
    }

    public void Start()
    {

    }

    public void Update()
    {
        // Debug.Log(_currentState == null ? "NULL" : "HAVE");
        Enum s = _currentState.TransitionCheck();
        if (s != null)
        {
            ChangeState(s);
        }
        _currentState.Update();
    }

    public void Leave()
    {

    }
}