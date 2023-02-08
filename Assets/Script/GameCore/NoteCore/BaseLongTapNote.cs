using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLTemplate;
using ArkRhythm;
using TLUtil;

public partial class BaseLongTapNote : BaseNote
{

    public LongTapNote _longTapNote;
    public FiniteStateMachine<BaseLongTapNote> _stateMachine;
    public JUDGE_RESULT _firstJudgeResult;
    public HPBar _hBar;
    public bool _stillAlive = true;//第一次判定后是否存活

    public float _loopTime = 0.25f;//循环判定时间
    public float _passTime = 0;

    public override void Awake()
    {
        _stateMachine = new FiniteStateMachine<BaseLongTapNote>(this);
        _stateMachine.AddTransition(BaseLongTapNote_State.Alive, BaseLongTapNote_State.FirstJudge, BaseLongTapNote_Condition.Alive2FirstJudge)
                     .AddTransition(BaseLongTapNote_State.Alive, BaseLongTapNote_State.Dead, BaseLongTapNote_Condition.Alive2Dead)
                     .AddTransition(BaseLongTapNote_State.FirstJudge, BaseLongTapNote_State.Judge, BaseLongTapNote_Condition.FirstJudge2Judge)
                     .AddTransition(BaseLongTapNote_State.FirstJudge, BaseLongTapNote_State.Dead, BaseLongTapNote_Condition.FirstJudge2Dead);
        _stateMachine.DefaultStateID = BaseLongTapNote_State.Alive;
        _stateMachine.Awake();
    }
    void Start()
    {
        SetNote();
    }

    public override void Update()
    {
        _stateMachine.Update();
    }

    /// <summary>
    /// 设置Note相关信息
    /// </summary>
    public override void SetNote()
    {
        //基础位置信息
        base.SetNote();
        //TODO 长条贴图
        SetHpBar();
    }

    public override void Init(Note n)
    {
        base.Init(n);
        _longTapNote = n as LongTapNote;
    }

    public void SetHpBar()
    {
        GameObject bar = Instantiate((GameObject)Resources.Load("Prefab/AttackRange/HPBar"), this.transform);
        _hBar = bar.AddComponent<HPBar>();
        _hBar.Init((int)(_longTapNote.duraTime / _loopTime));
    }
}

partial class BaseLongTapNote
{
    public enum BaseLongTapNote_State
    {
        Alive,
        FirstJudge,
        Judge,
        Dead
    }

    public enum BaseLongTapNote_Condition
    {
        Alive2FirstJudge,//第一次判定
        Alive2Dead,//从未判定
        FirstJudge2Dead,//Bad or 松手了
        FirstJudge2Judge,//去尾判
    }


}

partial class BaseLongTapNote
{
    public class BaseLongTapNote_State_Alive : State<BaseLongTapNote>
    {
        public BaseLongTapNote_State_Alive(IStateMachine<BaseLongTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }

        public override void Update()
        {
            //更新位置
            Subject.UpdateAlivePos();
        }
    }
    public class BaseLongTapNote_State_FirstJudge : State<BaseLongTapNote>
    {
        public BaseLongTapNote_State_FirstJudge(IStateMachine<BaseLongTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }

        public override void Enter()
        {
            //查看第一次判断是什么判定
            Subject._firstJudgeResult = Subject.DOFirstJudge(Subject._targetOperator);
        }

        public override void Update()
        {
            //判定成功此Update执行才有意义
            //每隔0.25获得是否拥有LongTapNote
            Subject._passTime += Time.deltaTime;
            if (Subject._passTime >= Subject._loopTime)
            {
                Subject._stillAlive = Singleton<KeyboardInputManager>.Instance.LoadInputState(Subject._targetOperator._operator.keyType, InputType.LONG_TAP);
                Debug.Log("此时的判断状态：" + Subject._stillAlive);
                if (Subject._stillAlive)
                {
                    Subject.DoJudgeAndDoAnime(Subject._targetOperator, Subject._firstJudgeResult);
                    Subject._hBar.SubHp();
                }
                Subject._passTime = 0;
            }

            //TODO 长条Prefeb的更新
        }
    }
    public class BaseLongTapNote_State_Judge : State<BaseLongTapNote>
    {
        public BaseLongTapNote_State_Judge(IStateMachine<BaseLongTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }

        public override void Enter()
        {
            Debug.Log("成功判定LongTap了,结束了！！");
            //道这个时候了，肯定是完美判定了
            Subject.DoJudgeAndDoAnime(Subject._targetOperator, JUDGE_RESULT.Perfect);

            //删除物体
            //解决Dotween未执行完的问题
            Subject._hBar.CompleteTween();
            Destroy(Subject.gameObject);
        }
    }
    public class BaseLongTapNote_State_Dead : State<BaseLongTapNote>
    {
        public BaseLongTapNote_State_Dead(IStateMachine<BaseLongTapNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }

        public override void Enter()
        {
            //死亡，统计分数，Miss
            Subject.DoJudgeAndDoAnime(Subject._targetOperator, JUDGE_RESULT.Miss);
            //销毁
            Destroy(Subject.gameObject);
        }
    }
}

partial class BaseLongTapNote
{
    public class BaseLongTapNote_Condition_Alive2FirstJudge : Condition<BaseLongTapNote>
    {
        public BaseLongTapNote_Condition_Alive2FirstJudge(Enum conditionId) : base(conditionId)
        {

        }

        public override bool ConditionCheck(BaseLongTapNote subject)
        {
            return subject.AliveJudge();
        }
    }
    public class BaseLongTapNote_Condition_Alive2Dead : Condition<BaseLongTapNote>
    {
        public BaseLongTapNote_Condition_Alive2Dead(Enum conditionId) : base(conditionId)
        {
        }

        public override bool ConditionCheck(BaseLongTapNote subject)
        {
            return subject.AliveMiss();
        }
    }

    public class BaseLongTapNote_Condition_FirstJudge2Dead : Condition<BaseLongTapNote>
    {
        public BaseLongTapNote_Condition_FirstJudge2Dead(Enum conditionId) : base(conditionId)
        {

        }

        public override bool ConditionCheck(BaseLongTapNote subject)
        {
            if (subject._firstJudgeResult == JUDGE_RESULT.Bad)
                return true;
            else
                return !subject._stillAlive;
        }
    }
    public class BaseLongTapNote_Condition_FirstJudge2Judge : Condition<BaseLongTapNote>
    {
        public BaseLongTapNote_Condition_FirstJudge2Judge(Enum conditionId) : base(conditionId)
        {

        }

        public override bool ConditionCheck(BaseLongTapNote subject)
        {
            float ct = GameClockManager.Instance.currentGamePalyTime;
            return ct > subject._longTapNote.endTime + subject._longTapNote.duraTime;
        }
    }

}