using System.Collections.Generic;
using System;
using UnityEngine;
using ArkRhythm;
using Util;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

public class BMSManager : Singleton<BMSManager>
{

    //铺面
    public BMS _bms;


    /// <summary>
    /// 加载铺面
    /// </summary>
    public void LoadBMS(String BMSUrl)
    {
        JObject jo = JsonUtil.readJSON(BMSUrl);
        _bms = new BMS().SetParam(jo);
    }

    /// <summary>
    /// 加载干员
    /// </summary>
    public void LoadOperator()
    {
        foreach (Operator o in _bms.operSet)
        {
            GameObject obj = new GameObject("operator");
            obj.SetActive(false);
            BaseOperator bo = obj.AddComponent<BaseOperator>();
            bo.Init(o);
            obj.SetActive(true);
        }
    }

    /// <summary>
    /// 加载Note
    /// </summary>
    public void LoadNote()
    {

    }

    public bool isStopGame = false;
    override protected void OnAwake()
    {
        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;

    }
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }


}