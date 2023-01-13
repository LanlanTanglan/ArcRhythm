using System.Collections.Generic;
using System;
using UnityEngine;
using ArkRhythm;
using TLUtil;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

public class BMSManager : Singleton<BMSManager>
{

    //铺面
    public BMS _bms;

    //干员脚本
    public List<BaseOperator> _baseOperators;

    //Note的idx
    public int[] _noteIdx = new int[4] { 0, 0, 0, 0 };//Tap、LongTap

    /// <summary>
    /// 加载铺面
    /// </summary>
    public void LoadBMS(String BMSUrl)
    {
        JObject jo = JsonUtil.readJSON(BMSUrl);
        _bms = new BMS().SetParam(jo);

        _baseOperators = new List<BaseOperator>();
    }

    /// <summary>
    /// 加载干员
    /// </summary>
    public void LoadOperator()
    {
        foreach (Operator o in _bms.operSet)
        {
            UnityEngine.Debug.Log("添加了！！！！！！！");
            GameObject obj = new GameObject("operator");
            obj.SetActive(false);
            BaseOperator bo = obj.AddComponent<BaseOperator>();
            bo.Init(o);
            _baseOperators.Add(bo);
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