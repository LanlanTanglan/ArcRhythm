using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ArcRhythm;
using Util;

public class BasePushNote : MonoBehaviour
{
    public Note note;
    public bool isStopGame = false;

    void Awake()
    {
        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }
    void Start()
    {

    }

    void Update()
    {

    }

    #region 事件注册块
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion

    public void Init(Note n)
    {
        this.note = n;
    }
}
