using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArcFMS
{
    /// <summary>
    /// 状态机接口
    /// </summary>
    public interface IStateMachine<T>
    {
        /// <summary>
        /// 实体
        /// </summary>
        /// <value></value>
        T subject{get;}

        /// <summary>
        /// 默认状态
        /// </summary>
        /// <value></value>
        Enum defaultState{get;}

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        IStateMachine<T> AddState(Enum state);

        /// <summary>
        /// 添加转换关系
        /// </summary>
        /// <param name="formarState"></param>
        /// <param name="latterState"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        IStateMachine<T> AddTransition(Enum formarState, Enum latterState, Enum condition);


    }
}