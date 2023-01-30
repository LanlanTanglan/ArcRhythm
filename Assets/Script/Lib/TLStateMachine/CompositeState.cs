using System;
using UnityEngine;
public abstract class CompositeState<T> : State<T>, IStateMachine<T>
{
    protected CompositeState(IStateMachine<T> stateMachine, Enum stateID) : base(stateMachine, stateID)
    {
        _innerStateMachine = new FiniteStateMachine<T>(Subject);
    }

    //内置状态机
    private readonly FiniteStateMachine<T> _innerStateMachine;
    public Enum DefaultStateID
    {
        get => _innerStateMachine.DefaultStateID;
        set => _innerStateMachine.DefaultStateID = value;
    }
    public override void Awake()
    {
        base.Awake();
        _innerStateMachine.Awake();
    }
    public override void Enter()
    {
        base.Enter();
        _innerStateMachine.Start();
    }
    public override void Update()
    {
        base.Update();
        _innerStateMachine.Update();
    }
    public override void Leave()
    {
        base.Leave();
        _innerStateMachine.ChangeState(null);
    }
    public IStateMachine<T> AddState(Enum state)
    {
        _innerStateMachine.AddState(state);
        return this;
    }
    public IStateMachine<T> AddTransition(Enum fromState, Enum toState, Enum condition)
    {
        _innerStateMachine.AddTransition(fromState, toState, condition);
        return this;
    }

    public void ChangeState(Enum targetState)
    {
        _innerStateMachine.ChangeState(targetState);
    }

    public IStateMachine<T> Open(Enum stateID)
    {
        return _innerStateMachine.Open(stateID);
    }
}