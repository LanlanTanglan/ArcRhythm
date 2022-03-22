using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcFMS
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IState<T>
    {
        /// <summary>
        /// 状态id
        /// </summary>
        /// <value></value>
        Enum state { get; }

        /// <summary>
        /// 主体(谁是这个状态的属于者)
        /// </summary>
        /// <value></value>
        T subject { get; }

        /// <summary>
        /// 所属于的状态机
        /// </summary>
        IStateMachine<T> stateMachine { get; }

        /// <summary>
        /// 进入状态
        /// </summary>
        void Enter();

        /// <summary>
        /// 处于这个状态
        /// </summary>
        void Update();

        /// <summary>
        /// 离开这个状态
        /// </summary>
        void Out();

        /// <summary>
        /// 是否有跳转
        /// </summary>
        /// <returns></returns>
        Enum TransitionCheck();
    }
}