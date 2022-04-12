using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcRhythm;
using Util;

public class BaseNote : MonoBehaviour
{
    public Note note;
    public bool isStopGame = false;
    public bool isFirstJudged = false;
    public BaseOperator targetBaseOperator;
    public NoteState noteState = NoteState.FirstJudging;
    public JUDGE_RESULT firstTapResult;//用于描述LongTap第一次判定的完美度，随后长按一直是此完美度

    // Start is called before the first frame update
    public void Awake()
    {
        this.OnAwake();
    }
    public virtual void OnAwake()
    {
        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }
    #region 事件注册块
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion
    void Start()
    {

    }

    public virtual void Init(Note n)
    {
        //初始化信息
        this.note = n;
        this.targetBaseOperator = Singleton<BMSManager>.Instance.baseOperators[n.targetOperId];
        //将其绑定在父物体上
        this.transform.SetParent(Singleton<BMSManager>.Instance.operatorObjs[n.targetOperId].transform);
        //设置在正确的攻击范围上
        this.transform.localPosition = ArcRhythmUtil.GetNoteOffset(targetBaseOperator.o.attackRange, note.attackId);
        //设置note生成位置
        this.transform.localPosition += ArcRhythmUtil.GetPosByDirection(note.direction, ArcRhythmUtil.GenerateNotePos(note, targetBaseOperator.o)) / 100;
        //设置角度
        this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    /// <summary>
    /// 更新Note的位置
    /// </summary>
    public virtual void FirstUpdatePos()
    {
        //注意负号
        this.transform.localPosition += ArcRhythmUtil.GetPosByDirection(note.direction, -targetBaseOperator.o.speed * Time.deltaTime);
    }

    /// <summary>
    /// 第二次的修改位置
    /// </summary>
    public virtual void SecondUpdatePos()
    {

    }

    /// <summary>
    /// 第一次判定
    /// </summary>
    public virtual void UpdateFirstJudge()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;

        //开始判定的时间范围
        if (ct - note.endTime >= ArcNum.prJudgeTime && ct - note.endTime <= ArcNum.neJudgeTime)
        {
            //时刻监听输入器中是否有需要的输入状态
            //如果监听到了就在下一帧转到 判定完美度判断
            if (Singleton<KeyboardInputManager>.Instance.LoadInputState(targetBaseOperator.o.keyType, InputType.TAP))
            {
                isFirstJudged = true;
                noteState = NoteState.SecondJudging;
                firstTapResult = ArcRhythmUtil.GetJudgeResult(note.endTime, ct, true);
                targetBaseOperator.DoJudgeAnim(note, firstTapResult);

                //TODO 冗余
                if (firstTapResult == JUDGE_RESULT.Perfect)
                {
                    Singleton<GameInfoManager>.Instance.cGamePlayResult.addPerfectNum().addMaxNum(true);
                }
                if (firstTapResult == JUDGE_RESULT.Good)
                {
                    Singleton<GameInfoManager>.Instance.cGamePlayResult.addGoodNum().addMaxNum(true);
                }
                if (firstTapResult == JUDGE_RESULT.Bad)
                {
                    Singleton<GameInfoManager>.Instance.cGamePlayResult.addBadNum().addMaxNum(false);
                }

                return;
            }
        }
        //MISS
        //超出了判定时间了, 代表着miss
        else if (ct - note.endTime > ArcNum.neJudgeTime)
        {
            isFirstJudged = true;
            noteState = NoteState.Miss;
            firstTapResult = JUDGE_RESULT.Miss;
            targetBaseOperator.DoJudgeAnim(note, JUDGE_RESULT.Miss);

            //设置Miss分数
            Singleton<GameInfoManager>.Instance.cGamePlayResult.addMissNum().addMaxNum(false);
        }
    }

    /// <summary>
    /// 第二次即以后判定
    /// </summary>
    public virtual void UpdateSecondJudge()
    {

    }

    public enum NoteState
    {
        FirstJudging = 1,
        SecondJudging = 2,
        Miss = 3,
    }
}
