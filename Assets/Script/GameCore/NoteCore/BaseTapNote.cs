using System;
using UnityEngine;
using ArkRhythm;
public partial class BaseTapNote : BaseNote
{
    TapNote _tapNote;
    public override void Awake()
    {

    }


    public override void Update()
    {

    }

    public void _init(TapNote t)
    {
        this._tapNote = t;
        base._init(t);
    }
}

//状态条件枚举
partial class BaseTapNote
{
    public enum BaseTapNote_State
    {
        Alive,
        Dead,
        Judge,
    }
    public enum BaseTapNote_Condition
    {
        Alive2Dead,
        Alive2Judge
    }
}

//状态
partial class BaseTapNote
{
    public class BaseTapNote_State_Alive : State<BaseTapNote>
    {
        public BaseTapNote_State_Alive(IStateMachine<BaseTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }

        public override void Enter()
        {
            base.Enter();
            //进入相关
        }
        public override void Update()
        {
            base.Update();
            //更新位置
            
        }
    }
    //Note没有任何操作，简称MISS
    public class BaseTapNote_State_Dead : State<BaseTapNote>
    {
        public BaseTapNote_State_Dead(IStateMachine<BaseTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            base.Update();
        }
    }
    //Note产生了判定
    public class BaseTapNote_State_Judge : State<BaseTapNote>
    {
        public BaseTapNote_State_Judge(IStateMachine<BaseTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {
        }
        public override void Enter()
        {
            base.Enter();
        }
    }
}

//条件
partial class BaseTapNote
{
    public class BaseTapNote_Condition_Alive2Dead : Condition<BaseTapNote>
    {
        public BaseTapNote_Condition_Alive2Dead(Enum conditionId) : base(conditionId)
        {

        }

        public override bool ConditionCheck(BaseTapNote subject)
        {
            return false;
            //什么时候出现Miss
        }
    }
    public class BaseTapNote_Condition_Alive2Judge : Condition<BaseTapNote>
    {
        public BaseTapNote_Condition_Alive2Judge(Enum conditionId) : base(conditionId)
        {
        }

        public override bool ConditionCheck(BaseTapNote subject)
        {
            return false;
            //什么时候出现Judge
        }
    }
}