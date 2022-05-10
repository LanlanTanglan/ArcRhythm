using System.Collections.Generic;
using System;
using UnityEngine;
using ArkRhythm;
using Util;
using Newtonsoft.Json.Linq;

/// <summary>
/// 铺面管理器
/// 逻辑:
///     载入铺面
///     载入干员
///     每帧刷新Note
/// 提供事件:
///     退出当前游戏--暂停铺面刷新，清除内存
/// 注册事件：
///     暂停游戏
/// </summary>
public class BMSManager : Singleton<BMSManager>
{
    public bool isStopGame = false;
    override protected void OnAwake()
    {
        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
        operatorObjs = new List<GameObject>();
        baseOperators = new List<BaseOperator>();
    }
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    public BMS bms;//铺面
    public bool isLoadBMS = false;//是否加载铺面
    public List<GameObject> operatorObjs;//干员obj存储
    public List<Operator> operators;//干员信息
    public List<BaseOperator> baseOperators;
    public int[] noteIdx = new int[4] { 0, 0, 0, 0 };//Note的idx

    public AsyncFileReader asyncFileReader;


    void Update()
    {
        //是否加载铺面
        if (isLoadBMS)
        {
            //游戏是否暂停
            if (!isStopGame)
            {
                LoadNotes();
            }

            //TODO   
        }
    }

    //停止加载铺面，并清理内存(意思是用户退出当前铺面了，没必要加载了)
    public void StopLoadBMS()
    {
        this.bms = null;

        this.noteIdx = new int[4] { 0, 0, 0, 0 };

        operatorObjs = new List<GameObject>();
        baseOperators = new List<BaseOperator>();
    }

    /// <summary>
    /// 加载铺面对象
    /// </summary>
    /// <param name="BMSurl"></param>
    public void LoadBMSClass(String BMSurl)
    {
        //读取铺面


        bms = DataUtil.ReaderDate<BMS>(BMSurl, null);

        isLoadBMS = true;

        //初始化这一局的游戏信息
        Singleton<GameInfoManager>.Instance.cGamePlayResult.ClearInfo().setTotalNoteNum(bms.BMSInfo.totalNoteNum);

        LoadBMS();
    }

    //老方法
    public void LoadBMSClass_2(String BMSurl)
    {
        JObject jo = JsonUtil.readJSON(BMSurl);
        bms = new BMS().SetParam(jo);

        isLoadBMS = true;

        //初始化这一局的游戏信息
        Singleton<GameInfoManager>.Instance.cGamePlayResult.ClearInfo().setTotalNoteNum(bms.BMSInfo.totalNoteNum);

        LoadBMS();
    }

    //异步方法
    public void LoadBMSClass_3(String BMSurl)
    {
        asyncFileReader = new AsyncFileReader(BMSurl);
    }

    public void TestLoad()
    {
        bms = asyncFileReader.bms;
        isLoadBMS = true;

        //初始化这一局的游戏信息
        Singleton<GameInfoManager>.Instance.cGamePlayResult.ClearInfo().setTotalNoteNum(bms.BMSInfo.totalNoteNum);

        LoadBMS();
    }

    /// <summary>
    /// 加载铺面
    /// </summary>
    private void LoadBMS()
    {
        if (bms != null)
        {
            LoadOperators();
        }
    }

    /// <summary>
    /// 加载干员
    /// </summary>
    public void LoadOperators()
    {
        //遍历载入干员预制体，并命名
        foreach (Operator o in bms.operSet)
        {
            string objn = Enum.GetName(typeof(OPERATOR), (int)o.operatorType);
            // GameObject opObj = Instantiate((GameObject)Resources.Load(ArcPath.prefebDirOfOperator + objn));
            GameObject opObj = Instantiate(Singleton<ABManager>.Instance.GetAssetBundle("operator").LoadAsset(objn, typeof(GameObject)) as GameObject);
            opObj.name = ArcStr.operatorName + "(" + objn + ")";
            //绑定Operator脚本
            BaseOperator bo = opObj.AddComponent<BaseOperator>();
            BaseOperatorController boc = opObj.AddComponent<BaseOperatorController>();
            //传入参数初始化
            bo.Init(o);
            boc.Init(o);
            operatorObjs.Add(opObj);
            baseOperators.Add(bo);
        }

    }

    /// <summary>
    /// 加载Note
    /// TODO 使用yield实现
    /// </summary>
    public void LoadNotes()
    {
        LoadTapNote();
        LoadLongTapLoad();
        LoadPushNote();
        LoadPullNote();
    }

    /// <summary>
    /// 加载TapNote
    /// </summary>
    public void LoadTapNote()
    {
        float ct = GameClockManager.Instance.currentGamePalyTime;
        //加载TapNote
        while (bms.noteSet.tapNotes != null && noteIdx[0] < bms.noteSet.tapNotes.Count && bms.noteSet.tapNotes[noteIdx[0]].endTime - ct <= 5f)
        {
            Note n = bms.noteSet.tapNotes[noteIdx[0]];
            GameObject nObj = Instantiate((GameObject)Resources.Load(ArcPath.prefebDirOfEnemy + Enum.GetName(typeof(ENEMY), (int)n.enemy)));
            //绑定Operator脚本
            BaseTapNote bn = nObj.AddComponent<BaseTapNote>();
            //传入参数初始化
            bn.Init(n);
            noteIdx[0]++;
        }
    }

    /// <summary>
    /// 加载LongtapNote
    /// </summary>
    public void LoadLongTapLoad()
    {
        float ct = GameClockManager.Instance.currentGamePalyTime;
        //加载TapNote
        while (bms.noteSet.longTapNotes != null && noteIdx[1] < bms.noteSet.longTapNotes.Count && bms.noteSet.longTapNotes[noteIdx[1]].endTime - ct <= 5f)
        {
            Note n = bms.noteSet.longTapNotes[noteIdx[1]];
            GameObject nObj = Instantiate((GameObject)Resources.Load(ArcPath.prefebDirOfEnemy + Enum.GetName(typeof(ENEMY), (int)n.enemy)));
            //绑定Operator脚本
            BaseLongTapNote bn = nObj.AddComponent<BaseLongTapNote>();
            //传入参数初始化
            bn.Init(n);
            noteIdx[1]++;
        }
    }

    /// <summary>
    /// 加载PushNote
    /// </summary>
    public void LoadPushNote()
    {
        float ct = GameClockManager.Instance.currentGamePalyTime;
        //加载TapNote
        while (bms.noteSet.pushNotes != null && noteIdx[2] < bms.noteSet.pushNotes.Count && bms.noteSet.pushNotes[noteIdx[2]].endTime - ct <= 5f)
        {
            Note n = bms.noteSet.pushNotes[noteIdx[2]];
            GameObject nObj = Instantiate((GameObject)Resources.Load(ArcPath.prefebDirOfEnemy + Enum.GetName(typeof(ENEMY), (int)n.enemy)));
            //绑定Operator脚本
            BasePushNote bn = nObj.AddComponent<BasePushNote>();
            //传入参数初始化
            bn.Init(n);
            noteIdx[2]++;
        }
    }

    /// <summary>
    /// 加载PullNote
    /// </summary>
    public void LoadPullNote()
    {
        float ct = GameClockManager.Instance.currentGamePalyTime;
        //加载TapNote
        while (bms.noteSet.pullNotes != null && noteIdx[3] < bms.noteSet.pullNotes.Count && bms.noteSet.pullNotes[noteIdx[0]].endTime - ct <= 5f)
        {
            Note n = bms.noteSet.pullNotes[noteIdx[3]];
            GameObject nObj = Instantiate((GameObject)Resources.Load(ArcPath.prefebDirOfEnemy + Enum.GetName(typeof(ENEMY), (int)n.enemy)));
            //绑定Operator脚本
            BasePullNote bn = nObj.AddComponent<BasePullNote>();
            //传入参数初始化
            bn.Init(n);
            noteIdx[3]++;
        }
    }
}