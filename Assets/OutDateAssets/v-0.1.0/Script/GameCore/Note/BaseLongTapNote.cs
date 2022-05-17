using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ArkRhythm;
using Util;
/// <summary>
/// 长按Note行为脚本
/// 
/// 思路
///     生成：
///         第一个Note生成规律与TapNote相同
///         Hold的长度只需要保证能在a时间内缩短至（高度）为0即可
///     长按长方形：
///         生成Hold的长度
///         Hold块第一次检测到判定时，长度开始缩减
/// </summary>
public class BaseLongTapNote : BaseNote
{
    public bool isFirstJudge = false;//长按判定的第一次是否判定成功
    public GameObject rect;
    public LongTapNote longTapNote;
    public bool isSecondPos = false;
    public float passTime;
    public float loopTime = 0.2f;
    public bool mutex1 = false;

    public override void OnAwake()
    {
        base.OnAwake();
    }
    void Start()
    {

    }

    void Update()
    {
        if (!isStopGame)
        {
            passTime += Time.deltaTime;
            //第一次未判定时的刷新
            if (!isSecondPos)
            {
                FirstUpdatePos();
            }
            if (noteState == NoteState.FirstJudging)
            {
                UpdateFirstJudge();
            }
            if (noteState == NoteState.SecondJudging)
            {
                UpdateSecondJudge();
            }
            //将Rect的透明度降低
            if (noteState == NoteState.Miss)
            {
                Debug.Log("已经Miss");
                if (!mutex1)
                {
                    SpriteRenderer s = rect.GetComponent<SpriteRenderer>();
                    Color c = new Color(s.color.r, s.color.g, s.color.b, 125 / 255f);
                    s.color = c;
                    mutex1 = true;
                }
                float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;
                if (ct >= longTapNote.endTime + longTapNote.duraTime)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="n"></param>
    public override void Init(Note n)
    {
        longTapNote = n as LongTapNote;
        base.Init(n);
        Debug.Log("Rect实例化");
        //实例化一个Rect
        rect = Instantiate((GameObject)Resources.Load("Prefab/Judge/Rect"));
        rect.transform.SetParent(this.transform);
        //设置位置，以及长度(就是rect的高度)
        rect.transform.localPosition = new Vector3(0, 0, 1);
        rect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //TODO 长度根据特定的关系式进行设置(这里规定变化速度为500像素每秒)
        rect.GetComponent<SpriteRenderer>().size = new Vector2(1.28f, longTapNote.duraTime * 2);
    }

    public override void FirstUpdatePos()
    {
        //如果超过了endTime就停止刷新其位置
        //开始缩小Hold长度
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;
        if (ct >= longTapNote.endTime)
        {
            isSecondPos = true;
            //为Rect添加脚本
            LongTapRect l = rect.AddComponent<LongTapRect>();
            l.Init(longTapNote.duraTime, 2f);
        }
        base.FirstUpdatePos();
    }

    public override void UpdateFirstJudge()
    {
        base.UpdateFirstJudge();
    }

    public override void UpdateSecondJudge()
    {
        //如果时间大于等于了结束时间加长按时间，则说明这个LongTap是完美判定的
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;
        if (ct >= longTapNote.endTime + longTapNote.duraTime)
        {
            targetBaseOperator.CreateJudgeAnim(longTapNote, firstTapResult);
            Destroy(this.gameObject);
        }
        //未结束,,需持续判定
        //TODO 逻辑需要完善
        if (passTime >= loopTime)
        {

            if (ct <= longTapNote.endTime + longTapNote.duraTime && Singleton<KeyboardInputManager>.Instance.LoadInputState(targetBaseOperator.o.keyType, InputType.LONG_TAP))
            {
                Debug.Log("正在长按");
                targetBaseOperator.CreateJudgeAnim(longTapNote, firstTapResult);
            }
            else
            {
                noteState = NoteState.Miss;
                Debug.Log("失败了/结束了");
            }
            passTime = 0;
        }
    }
}
