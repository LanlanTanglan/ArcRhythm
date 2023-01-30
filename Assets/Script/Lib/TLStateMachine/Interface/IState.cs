using System;
public interface IState<T>
{
    /// <summary>
    /// 状态名
    /// </summary>
    /// <value></value>
    Enum StateID { get; }

    /// <summary>
    /// 状态主体
    /// </summary>
    /// <value></value>
    T Subject { get; }

    IStateMachine<T> StateMachine { get; }

    /// <summary>
    /// 进入时
    /// </summary>
    void Enter();

    /// <summary>
    /// 更新时
    /// </summary>
    void Update();

    /// <summary>
    /// 离开时
    /// </summary>
    void Leave();

    /// <summary>
    /// 添加条件
    /// </summary>
    /// <param name="conditionID">条件ID</param>
    /// <param name="stateID">状态ID</param>
    /// <returns></returns>
    IState<T> AddCondition(Condition<T> conditionID, Enum stateID);

    /// <summary>
    /// 状态检测
    /// </summary>
    /// <returns></returns>
    Enum TransitionCheck();
}