using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ArkRhythm;
using Util;

public class BaseTapNote : BaseNote
{
    public Note tapNote;
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
            // UpdateJudgeResult();

            //第一次判定
            if (!isFirstJudged)
            {
                //判定
                UpdateFirstJudge();
                //更新位置
                FirstUpdatePos();
            }
            //没有二次判定，所以直接销毁
            else
            {
                Destroy(this.gameObject);
            }
        }
    }


    public override void Init(Note n)
    {
        base.Init(n);
        tapNote = n as TapNote;
    }


    public override void FirstUpdatePos()
    {
        base.FirstUpdatePos();
    }

    /// <summary>
    /// 铺面判定相关
    /// </summary>
    public override void UpdateFirstJudge()
    {
        base.UpdateFirstJudge();
    }
}
