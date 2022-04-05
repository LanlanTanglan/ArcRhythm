using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ArcRhythm;
using Util;

public class BaseTapNote : BaseNote
{
    // public Note note;
    // public bool isStopGame = false;
    // public bool isJudged = false;
    // public BaseOperator targetBaseOperator;
    //public float currentSpeed;
    void Start()
    {

    }
    void Update()
    {
        if (!isStopGame)
        {
            //TODO 判定提示动画(就是类似于一个小圈圈)
            UpdateJudgeResult();
            //判定
            UpdateJudge();

            //更新位置
            FirstUpdatePos();
        }
    }


    public override void Init(Note n)
    {
        base.Init(n);
    }


    public override void FirstUpdatePos()
    {
        base.FirstUpdatePos();
    }

    /// <summary>
    /// 铺面判定相关
    /// </summary>
    private void UpdateJudge()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;

        //开始判定的时间范围
        if (ct - note.endTime >= ArcNum.prJudgeTime && ct - note.endTime <= ArcNum.neJudgeTime)
        {
            //时刻监听输入器中是否有需要的输入状态
            //如果监听到了就在下一帧转到 判定完美度判断
            if (Singleton<KeyboardInputManager>.Instance.LoadInputState(targetBaseOperator.o.keyType, InputType.TAP))
            {
                isJudged = true;
                return;
            }
        }
        //MISS
        //超出了判定时间了, 代表着miss
        else if (ct - note.endTime > ArcNum.neJudgeTime)
        {
            Debug.Log("Miss" + (ct - note.endTime) + this.transform.localPosition);
            targetBaseOperator.CreateJudgeAnim(note, JUDGE_RESULT.Miss);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 处理判定结果，TODO并播放动画
    /// </summary>
    private void UpdateJudgeResult()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;
        if (isJudged)
        {
            float c = ct - note.endTime;
            //Bad
            if (c >= ArcNum.prJudgeTime && c < ArcNum.prJudgeTime + ArcNum.badJudgeTime)
            {
                Debug.Log("Bad" + c + this.transform.localPosition);
                targetBaseOperator.CreateJudgeAnim(note, JUDGE_RESULT.Bad);
                Destroy(this.gameObject);
            }

            //Good
            if ((c >= -2 * ArcNum.perJudgeTime && c < -ArcNum.perJudgeTime) || (ct > ArcNum.perJudgeTime && ct <= 2 * ArcNum.perJudgeTime))
            {
                Debug.Log("Good" + c + this.transform.localPosition);
                Singleton<AudioManager>.Instance.AudioInstantiate("Note/Dead/tap");
                targetBaseOperator.CreateJudgeAnim(note, JUDGE_RESULT.Good);
                Destroy(this.gameObject);
            }

            //Perfect
            if ((c >= -ArcNum.perJudgeTime && c <= 0) || (ct >= 0 && ct <= ArcNum.perJudgeTime))
            {
                Debug.Log("Perfect" + c + this.transform.localPosition);
                Singleton<AudioManager>.Instance.AudioInstantiate("Note/Dead/tap");
                targetBaseOperator.CreateJudgeAnim(note, JUDGE_RESULT.Perfect);
                Destroy(this.gameObject);
            }
        }
    }
}
