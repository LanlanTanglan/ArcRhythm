using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ArkRhythm;
using System;
using DG.Tweening;
using Spine.Unity;

public class BaseOperator : MonoBehaviour
{
    public Operator _operator;
    public SkeletonMecanim _skeletonMecanim;
    public MeshRenderer _meshRenderer;

    public virtual void Awake()
    {
        SpineManager.Instance.Init();
        //初始化
        _skeletonMecanim = GetComponent<SkeletonMecanim>();
        _meshRenderer = GetComponent<MeshRenderer>();




        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
        this.transform.localPosition = Vector3.zero;
        //设置Obj初始状态
        //TODO //设置Alpha值

        //TODO Note信息统计

        //设置Obj的攻击范围
        _setAttackRange();
        //设置朝向
        _setDirection(_operator.direction);
    }
    bool a = false;
    public bool b = false;
    public virtual void Update()
    {
        if (!isStopGame)
        {
            UpdateOperterAnim();
        }
        if (a != b)
        {
            if (a)
            {
                _meshRenderer.material = SpineManager.Instance.GetMaterial("myrtle");
                _skeletonMecanim.skeletonDataAsset = SpineManager.Instance.GetSkeletonDataAsset("myrtle");
                _skeletonMecanim.Initialize(true);
            }
            else
            {
                _meshRenderer.material = SpineManager.Instance.GetMaterial("myrtle_b");
                _skeletonMecanim.skeletonDataAsset = SpineManager.Instance.GetSkeletonDataAsset("myrtle_b");
                _skeletonMecanim.Initialize(true);
            }
            a = b;
        }
    }

    public int _oaIdx = 0;//判定线动画指针

    public SpriteRenderer spriteRenderer;

    #region 事件注册块
    public bool isStopGame = false;
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion

    //初始化变量以及相关动画
    public void _init(Operator o)
    {
        this._operator = o;
    }

    public void _setDirection(DIRECTION d)
    {
        if (d == DIRECTION.LEFT)
        {
            this.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    //设置攻击距离(判定区域的范围)
    public void _setAttackRange()
    {
        List<Vector2> vector2s = Util.ArcRhythmUtil.GetAttackRange(this._operator.attackRange);
        UnityEngine.Object to = Resources.Load("Prefab/AttackRange/JudgePoint");
        foreach (Vector2 v in vector2s)
        {
            GameObject pObj = Instantiate((GameObject)to);
            pObj.transform.SetParent(this.transform);
            pObj.transform.localScale = new Vector3(1, 1, 1);
            pObj.transform.localPosition = new Vector3(v.x, 1f + v.y, 0);
        }
    }


    //更新判定线动画
    private void UpdateOperterAnim()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;

        while (_oaIdx < _operator.animCommands.Count && ct >= _operator.animCommands[_oaIdx].beginTime)
        {
            //动画基于SpriteRender
            //TODO 暂时不考虑透明度变化问题
            if (_operator.animCommands[_oaIdx].animCommandType == ANIM_COMMAND.OP_CA)
            {

                // //TODO 一级子节点变化
                // //TODO 二级子节点变化

            }
            //使用Transform
            else
            {
                _operator.animCommands[_oaIdx].GetTween(this.transform).Play();
            }
            //下一条命令
            _oaIdx++;
        }
    }

    /// <summary>
    /// 生成判定动画
    /// </summary>
    /// <param name="note"></param>
    public void CreateJudgeAnim(Note note, JUDGE_RESULT jr)
    {
        //根据枚举初始化一个物体
        GameObject pObj = Instantiate((GameObject)Resources.Load("Prefab/Judge/" + Enum.GetName(typeof(JUDGE_RESULT), jr)));
        pObj.transform.SetParent(this.transform);
        //让这个物体绑定对应的脚本
        BaseJudgePerfor bjp = null;
        if (jr == JUDGE_RESULT.Perfect)
        {
            bjp = pObj.AddComponent<Perfect>();
        }
        else if (jr == JUDGE_RESULT.Good)
        {
            bjp = pObj.AddComponent<Good>();
        }
        else if (jr == JUDGE_RESULT.Bad)
        {
            bjp = pObj.AddComponent<Bad>();
        }
        else if (jr == JUDGE_RESULT.Miss)
        {
            bjp = pObj.AddComponent<Miss>();
        }

        bjp.Init(_operator.attackRange, note.attackId);
    }

    /// <summary>
    /// 处理判定结果，TODO 修改为两个函数
    /// </summary>
    public void DoJudgeAnim(Note note, JUDGE_RESULT jr)
    {
        //根据枚举初始化一个物体
        string jrn = Enum.GetName(typeof(JUDGE_RESULT), jr);
        // Debug.Log("判断名称: " + jrn.ToString());
        // GameObject pObj = Instantiate((GameObject)Resources.Load("Prefab/Judge/" + jrn));
        // pObj.transform.SetParent(this.transform);

        CreateJudgeAnim(note, jr);
    }
}