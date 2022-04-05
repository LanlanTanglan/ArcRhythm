using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ArcRhythm;
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
            FirstUpdatePos();
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
        rect.transform.localPosition = Vector3.zero;
        rect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //TODO 长度根据特定的关系式进行设置(这里规定变化速度为500像素每秒)
        rect.GetComponent<SpriteRenderer>().size.Set(1.28f, longTapNote.duraTime * 5);
    }

    public override void FirstUpdatePos()
    {
        base.FirstUpdatePos();
    }
}
