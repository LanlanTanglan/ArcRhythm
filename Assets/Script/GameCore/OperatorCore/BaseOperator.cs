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
    public BaseOperatorController _baseOperatorController;
    public SkeletonMecanim _skeletonMecanim;
    public MeshRenderer _meshRenderer;
    public Animator _animator;
    public GameObject _childOperator;//干员Prefab
    public List<GameObject> _attackRanges = new List<GameObject>();
    public int _acIdx = 0;//判定线动画指针
    public int _setSpeedIdx = 0;//设置速度指针

    public virtual void Awake()
    {

        //初始化子物体
        _setOperator();

        //初始化字段
        _skeletonMecanim = _childOperator.GetComponent<SkeletonMecanim>();
        _meshRenderer = _childOperator.GetComponent<MeshRenderer>();
        _animator = _childOperator.GetComponent<Animator>();

        //添加脚本
        _baseOperatorController = _childOperator.AddComponent<BaseOperatorController>();
        _baseOperatorController.Init(_operator);



        //初始化位置（TODO 预先加载起初先放在很远的一个地方）
        this.transform.localPosition = new Vector3(1920, 0, 0);

        //TEST
        // OpSetPos a = new OpSetPos();
        // List<float> l = new List<float>();
        // l.Add(500 / ArcNum.pixelPreUnit);
        // l.Add(0 / ArcNum.pixelPreUnit);
        // l.Add(0);
        // a.beginTime = 0f;
        // a.newPos = l;
        // a.animCommandType = ANIM_COMMAND.OP_SetPos;
        // _operator.animCommands.Add(a);

        // OpSetDirect d = new OpSetDirect();
        // d.d1 = DIRECTION.UP;
        // d.d2 = DIRECTION.RIGHT;
        // d.d3 = DIRECTION.UP;
        // d.animCommandType = ANIM_COMMAND.OP_SerDirect;
        // d.beginTime = 5f;
        // _operator.animCommands.Add(d);

        //添加事件响应
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (!isStopGame)
        {
            _updateOperterAnim();
            _updateSpeed();
        }
    }

    #region 事件注册块
    public bool isStopGame = false;
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion

    //初始化变量以及相关动画
    public void Init(Operator o)
    {
        this._operator = o;
    }

    //增加干员的Spine
    public void _setOperator()
    {
        Debug.Log("我在这");
        Debug.Log(_operator);
        //实例化
        GameObject o = (GameObject)Resources.Load("Prefab/Operator/" + Enum.GetName(typeof(OPERATOR), _operator.operatorType));
        _childOperator = Instantiate(o);
        _childOperator.transform.parent = this.transform;
        _childOperator.transform.localPosition = Vector3.zero;

        //设置攻击范围
        _setAttackRange();
    }

    /// <summary>
    /// 若是左右朝向的话，仅需要翻转（默认正方向为朝右）
    /// 若是上朝向的话，则需要使用背面素材
    /// TODO 逻辑待修改
    /// </summary>
    /// <param name="d"></param>
    public void SetDirection(DIRECTION d)
    {
        //朝前，使用背面素材
        if (d == DIRECTION.UP)
        {
            _meshRenderer.material = ABManager.Instance.GetMaterial("myrtle_b");
            _skeletonMecanim.skeletonDataAsset = ABManager.Instance.GetSkeletonDataAsset("myrtle_b");
            _skeletonMecanim.Initialize(true);
            ChangeAttackRangeDirecton(DIRECTION.UP);
        }
        //使用正面素材
        else if (d == DIRECTION.DOWN)
        {
            _meshRenderer.material = ABManager.Instance.GetMaterial("myrtle");
            _skeletonMecanim.skeletonDataAsset = ABManager.Instance.GetSkeletonDataAsset("myrtle");
            _skeletonMecanim.Initialize(true);
            ChangeAttackRangeDirecton(DIRECTION.DOWN);
        }
        else if (d == DIRECTION.LEFT)
        {
            _childOperator.transform.DOLocalRotate(new Vector3(0, 180, 0), 0.25f).SetEase(Ease.OutCubic).Play();
            ChangeAttackRangeDirecton(DIRECTION.LEFT);
        }
        else if (d == DIRECTION.RIGHT)
        {
            _childOperator.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.OutCubic).Play();
            ChangeAttackRangeDirecton(DIRECTION.RIGHT);
        }

    }

    //设置攻击距离(判定区域的范围)
    public void _setAttackRange()
    {
        List<Vector2> vector2s = TLUtil.ArkRhythmUtil.GetAttackRange(this._operator.attackRange);
        UnityEngine.Object to = Resources.Load("Prefab/AttackRange/JudgePoint");
        foreach (Vector2 v in vector2s)
        {
            GameObject pObj = Instantiate((GameObject)to);
            _attackRanges.Add(pObj);
            pObj.transform.SetParent(this.transform);
            pObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pObj.transform.localPosition = Vector3.zero;
        }
        ChangeAttackRangeDirecton(DIRECTION.LEFT);
    }

    /// <summary>
    /// 以动画的方式修改方向
    /// 
    /// </summary>
    /// <param name="d"></param>
    public void ChangeAttackRangeDirecton(DIRECTION d)
    {
        List<Vector2> v2 = TLUtil.ArkRhythmUtil.GetAttackRange(this._operator.attackRange);
        v2 = TLUtil.ArkRhythmUtil.ChangeArrackRangeDirection(d, v2);
        //设置移动动画
        int i = 0;
        foreach (GameObject g in _attackRanges)
        {
            g.transform.DOLocalMove(new Vector3(v2[i].x / 2, (1f + v2[i].y) / 2, 0), 1f).SetEase(Ease.OutCubic).Play();
            i++;
        }
    }

    //TODO 更新判定线动画
    private void _updateOperterAnim()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;

        //更新动画
        while (_acIdx < _operator.animCommands.Count && ct >= _operator.animCommands[_acIdx].beginTime)
        {
            switch (_operator.animCommands[_acIdx].animCommandType)
            {
                //暂时不实现
                case ANIM_COMMAND.OP_DoAlpha:
                    break;
                case ANIM_COMMAND.OP_DoMove:
                    _operator.animCommands[_acIdx].GetTween(this.transform).Play();
                    break;
                case ANIM_COMMAND.OP_DoRotate:
                    _operator.animCommands[_acIdx].GetTween(this.transform).Play();
                    break;
                //设置位置：播放进场动画
                case ANIM_COMMAND.OP_SetPos:
                    // Debug.Log("执行SetPos");
                    this.transform.localPosition = _operator.animCommands[_acIdx].GetPos();
                    _animator.Play("Start");
                    break;
                case ANIM_COMMAND.OP_SetSpeed:
                    break;
                case ANIM_COMMAND.OP_SerDirect:
                    AnimCommand od = _operator.animCommands[_acIdx];
                    SetDirection(od.directions[0]);
                    SetDirection(od.directions[1]);
                    SetDirection(od.directions[2]);
                    break;
            }
            //下一条命令
            _acIdx++;
        }
    }
    /// <summary>
    /// 更新判定线速度
    /// </summary>
    public void _updateSpeed()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;
        while (_setSpeedIdx < _operator.opsvList.Count && ct >= _operator.opsvList[_setSpeedIdx].beginTime)
        {
            _operator.speed = _operator.opsvList[_setSpeedIdx].newSpeed;
            _setSpeedIdx++;
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

        bjp.Init(_operator.attackRange);
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