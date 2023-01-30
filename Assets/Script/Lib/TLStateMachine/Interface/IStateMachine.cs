using System;
public interface IStateMachine<T>
{
    /// <summary>
    /// 状态主体
    /// </summary>
    T Subject { get; }

    /// <summary>
    /// 默认状态
    /// </summary>
    Enum DefaultStateID { get; set; }

    /// <summary>
    /// 添加状态（建议直接使用添加转换关系方法AddTransition()）
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    IStateMachine<T> AddState(Enum state);

    /// <summary>
    /// 添加状态转换关系
    /// </summary>
    /// <param name="fromState">来源状态ID</param>
    /// <param name="toState">目标状态ID</param>
    /// <param name="condition">条件ID</param>
    /// <returns>this</returns>
    IStateMachine<T> AddTransition(Enum fromState, Enum toState, Enum condition);

    /// <summary>
    /// 变更状态
    /// </summary>
    /// <param name="targetState">目标状态ID</param>
    void ChangeState(Enum targetState);

    /// <summary>
    /// 打开复合状态的子状态机
    /// </summary>
    /// <param name="stateID">复合状态ID</param>
    /// <returns>子状态机</returns>
    IStateMachine<T> Open(Enum stateID);
}