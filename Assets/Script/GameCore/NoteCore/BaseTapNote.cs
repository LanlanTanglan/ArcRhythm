using System.Reflection;
using System;
using UnityEngine;
using ArkRhythm;
using TLUtil;
public partial class BaseTapNote : BaseNote
{
    public Note _tapNote;
    // public GameObject _childNotePrefeb;
    // public BaseOperator _targetOperator;
    public FiniteStateMachine<BaseTapNote> _stateMachine;

    public override void Awake()
    {
        //设置状态机
        _stateMachine = new FiniteStateMachine<BaseTapNote>(this);
        _stateMachine.AddTransition(BaseTapNote_State.Alive, BaseTapNote_State.Dead, BaseTapNote_Condition.Alive2Dead)
                     .AddTransition(BaseTapNote_State.Alive, BaseTapNote_State.Judge, BaseTapNote_Condition.Alive2Judge);
        _stateMachine.DefaultStateID = BaseTapNote_State.Alive;
        _stateMachine.Awake();
    }

    public void Start()
    {
        SetNote();
    }

    public override void Update()
    {
        _stateMachine.Update();
    }

    public override void Init(Note t)
    {
        this._tapNote = t;
        // Debug.LogWarning($"这里是{_tapNote.enemy}");
        base.Init(t);
    }

    //设置Note的相关属性
    public override void SetNote()
    {
        //设置基础属性
        base.SetNote();
    }

    //更新位置
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
        }
        public override void Update()
        {
            Subject.UpdateAlivePos();
        }
    }

    //Note产生了判定，可能会出现Bad，Good Perfect
    public class BaseTapNote_State_Judge : State<BaseTapNote>
    {
        public BaseTapNote_State_Judge(IStateMachine<BaseTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }
        public override void Enter()
        {
            Subject.DOFirstJudge(Subject._targetOperator);
            Destroy(Subject.gameObject);
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
            //死亡，统计分数，Miss
            Subject.DoJudgeAndDoAnime(Subject._targetOperator, JUDGE_RESULT.Miss);
            //销毁
            Destroy(Subject.gameObject);
        }
        public override void Update()
        {

        }
    }

}

//条件
partial class BaseTapNote
{
    public class BaseTapNote_Condition_Alive2Judge : Condition<BaseTapNote>
    {
        public BaseTapNote_Condition_Alive2Judge(Enum conditionId) : base(conditionId)
        {
        }

        public override bool ConditionCheck(BaseTapNote subject)
        {
            return subject.AliveJudge();
        }
    }

    public class BaseTapNote_Condition_Alive2Dead : Condition<BaseTapNote>
    {
        public BaseTapNote_Condition_Alive2Dead(Enum conditionId) : base(conditionId)
        {

        }

        public override bool ConditionCheck(BaseTapNote subject)
        {
            return false;
        }
    }

}