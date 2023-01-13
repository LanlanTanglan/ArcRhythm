using System.Reflection;
using System;
using UnityEngine;
using ArkRhythm;
using TLUtil;
public partial class BaseTapNote : BaseNote
{
    public TapNote _tapNote;
    public GameObject _childNotePrefeb;
    public BaseOperator _targetOperator;
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
        _setNote();
    }

    public override void Update()
    {
        _stateMachine.Update();
    }

    public void Init(TapNote t)
    {
        this._tapNote = t;
        base._init(t);
    }

    //设置Note的相关属性
    public void _setNote()
    {
        //设置Note贴图
        GameObject o = (GameObject)Resources.Load("Prefab/Enemy/" + Enum.GetName(typeof(ENEMY), _tapNote.enemy));
        _childNotePrefeb = Instantiate(o);
        _childNotePrefeb.transform.parent = this.transform;
        _childNotePrefeb.transform.localPosition = Vector3.zero;

        //设置Note初始位置
        this._targetOperator = BMSManager.Instance._baseOperators[_tapNote.targetOperId];
        this.transform.parent = _targetOperator._attackRanges[_tapNote.attackId].transform;
        this.transform.localPosition = Vector3.zero;

        //设置Note距离Attack的范围
        this.transform.localPosition += ArkRhythmUtil.GetPosByDirection(_tapNote.direction, ArkRhythmUtil.GenerateNotePos(_tapNote, _targetOperator._operator)) / 100;
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
            base.Update();
            // Debug.Log(StateMachine.Subject);
            //更新位置
            if (GameClockManager.Instance.isGameBegin)
            {
                Subject.transform.localPosition += ArkRhythmUtil.GetPosByDirection(Subject._tapNote.direction, -Subject._targetOperator._operator.speed * Time.deltaTime);
            }
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
            float ct = GameClockManager.Instance.currentGamePalyTime;
            JUDGE_RESULT res = ArkRhythmUtil.GetJudgeResult(Subject._tapNote.endTime, ct, true);
            Debug.Log(res);
            Subject.DoJudgeAndDoAnime(Subject._targetOperator, res);
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
            Singleton<GameInfoManager>.Instance.cGamePlayResult.addMissNum().addMaxNum(false);
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
            float ct = GameClockManager.Instance.currentGamePalyTime;
            return ct - subject._tapNote.endTime >= ArcNum.prJudgeTime &&
                   ct - subject._tapNote.endTime <= ArcNum.neJudgeTime &&
                   Singleton<KeyboardInputManager>.Instance.LoadInputState(subject._targetOperator._operator.keyType, InputType.TAP);
        }
    }

    public class BaseTapNote_Condition_Alive2Dead : Condition<BaseTapNote>
    {
        public BaseTapNote_Condition_Alive2Dead(Enum conditionId) : base(conditionId)
        {

        }

        public override bool ConditionCheck(BaseTapNote subject)
        {
            float ct = GameClockManager.Instance.currentGamePalyTime;
            return ct - subject._tapNote.endTime > ArcNum.neJudgeTime;
        }
    }

}