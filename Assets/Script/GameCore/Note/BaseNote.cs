using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcRhythm;
using Util;

public class BaseNote : MonoBehaviour
{
    public Note note;
    public bool isStopGame = false;
    public bool isJudged = false;
    public BaseOperator targetBaseOperator;
    // Start is called before the first frame update
    public void Awake()
    {
        this.OnAwake();
    }
    public virtual void OnAwake()
    {
        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }
    #region 事件注册块
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion
    void Start()
    {

    }

    public virtual void Init(Note n)
    {
        //初始化信息
        this.note = n;
        this.targetBaseOperator = Singleton<BMSManager>.Instance.baseOperators[n.targetOperId];
        //将其绑定在父物体上
        this.transform.SetParent(Singleton<BMSManager>.Instance.operatorObjs[n.targetOperId].transform);
        //设置在正确的攻击范围上
        this.transform.localPosition = ArcMUtil.GetNoteOffset(targetBaseOperator.o.attackRange, note.attackId);
        //设置note生成位置
        this.transform.localPosition += ArcMUtil.GetPosByDirection(note.direction, ArcMUtil.GenerateNotePos(note, targetBaseOperator.o)) / 100;
        //设置角度
        this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    /// <summary>
    /// 更新Note的位置
    /// </summary>
    public virtual void FirstUpdatePos()
    {
        //注意负号
        this.transform.localPosition += ArcMUtil.GetPosByDirection(note.direction, -targetBaseOperator.o.speed * Time.deltaTime);
    }

    /// <summary>
    /// 第二次的修改位置
    /// </summary>
    public virtual void SecondUpdatePos()
    {

    }

    /// <summary>
    /// 第一次判定
    /// </summary>
    public virtual void FirstJudge()
    {

    }

    /// <summary>
    /// 第二次即以后判定
    /// </summary>
    public virtual void SecondJudge()
    {

    }
}
