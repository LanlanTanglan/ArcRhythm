using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcFMS
{
    public class ArcStateMachine<T> : IStateMachine<T>
    {
        public T subject { get; private set; }
        public Enum defaultState { get; set; }

        public IStateMachine<T> childStateMachine;

        public IState<T> currentState;

        public IStateMachine<T> AddState(Enum state)
        {
            



            return this;
        }

        public IStateMachine<T> AddTransition(Enum formarState, Enum latterState, Enum condition)
        {
            return this;
        }

        public void Update()
        {
            //更新当前状态
            
            
        }

    }
}