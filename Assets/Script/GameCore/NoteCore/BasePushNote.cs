using System.Threading.Tasks;
using System.Reflection;
using System;
using UnityEngine;
using ArkRhythm;
using TLUtil;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TLTemplate;

public partial class BasePushNote : BaseNote
{
    public PushNote _pushNote;
    public FiniteStateMachine<BasePushNote> _stateMachine;

    public void Start()
    {
        SetNote();
    }

    public override void Awake()
    {
        _stateMachine = new FiniteStateMachine<BasePushNote>(this);
        _stateMachine.AddTransition(BasePushNote_State.Alive, BasePushNote_State.Bounce, BasePushNote_Condition.Alive2Bounce);
        _stateMachine.AddTransition(BasePushNote_State.Alive, BasePushNote_State.Dead, BasePushNote_Condition.Alive2Dead);
        _stateMachine.DefaultStateID = BasePushNote_State.Alive;
        _stateMachine.Awake();
    }

    public override void Update()
    {
        _stateMachine.Update();
    }

    public override void SetNote()
    {
        base.SetNote();
    }

    public override void Init(Note n)
    {
        _pushNote = n as PushNote;
        base.Init(n);
    }
}

partial class BasePushNote
{
    public enum BasePushNote_State
    {
        Alive,
        Bounce,
        Dead
    }
    public enum BasePushNote_Condition
    {
        Alive2Dead,
        Alive2Bounce,
        Bounce2Dead
    }
}

partial class BasePushNote
{
    public class BasePushNote_State_Alive : State<BasePushNote>
    {
        public BasePushNote_State_Alive(IStateMachine<BasePushNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }

        public override void Update()
        {
            Subject.UpdateAlivePos();
        }
    }

    public class BasePushNote_State_Bounce : State<BasePushNote>
    {
        public BasePushNote_State_Bounce(IStateMachine<BasePushNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
        {

        }

        public override void Enter()
        {
            //宽松判定，进入此状态且判断成功则是Perfect否则就是超时判定失败
            ArkRhythmUtil.GetJudgeResult(Subject._pushNote.endTime, GameClockManager.Instance.currentGamePalyTime, true);
            Subject.DoJudgeAndDoAnime(Subject._targetOperator, JUDGE_RESULT.Perfect);
            //TODO 多目标来回弹跳
            // Debug.Log("干员方向" + Subject._targetOperator._currentDirect[2]);
            //获取增量
            Vector3 inc = ArkRhythmUtil.GetPosByDirection(Subject._targetOperator._currentDirect[2], Subject._pushNote.pushDistance);


            //第一次判定动画显示，往干员朝的方向弹走
            Subject.transform.DOLocalMove
                (
                    -inc / ArcNum.pixelPreUnit,
                    0.5f
                ).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    Destroy(Subject.gameObject);
                });
        }

        public override void Update()
        {

        }
    }

    public class BasePushNote_State_Dead : State<BasePushNote>
    {
        public BasePushNote_State_Dead(IStateMachine<BasePushNote> stateMachine, Enum stateID) : base(stateMachine, stateID)
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

partial class BasePushNote
{
    public class BasePushNote_Condition_Alive2Dead : Condition<BasePushNote>
    {
        public BasePushNote_Condition_Alive2Dead(Enum conditionId) : base(conditionId)
        {
        }

        public override bool ConditionCheck(BasePushNote subject)
        {
            return subject.AliveMiss();
        }
    }

    public class BasePushNote_Condition_Alive2Bounce : Condition<BasePushNote>
    {
        float waitTime = 0.5f;//等待时间
        bool isFirstJudge = false;//是否第一个按钮判定
        public BasePushNote_Condition_Alive2Bounce(Enum conditionId) : base(conditionId)
        {

        }

        //宽松判定，即不可能出现Bad
        public override bool ConditionCheck(BasePushNote subject)
        {
            //第一个按键判定
            if (!isFirstJudge)
            {
                if (subject.AliveJudge())
                    isFirstJudge = true;
            }
            //开始等待时间
            else
            {
                //第二次判定(给你0.2s的时间去按)
                if (waitTime > 0)
                {
                    if (Singleton<KeyboardInputManager>.Instance.LoadInputState(subject._pushNote.secondKey, InputType.TAP))
                        return true;
                }
                waitTime -= Time.deltaTime;
            }
            return false;
        }
    }
}
