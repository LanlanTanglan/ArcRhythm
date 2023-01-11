using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using TLUtil;


//基础干员控制器控制器
public class BaseOperatorController : MonoBehaviour
{
    #region 事件注册块
    public bool isStopGame = false;
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion

    public Animator _animator;
    public Operator _operator;
    void Awake()
    {
        //获取脚本
        _animator = GetComponent<Animator>();

        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }
    void Start()
    {

    }

    void Update()
    {
        if (!isStopGame)
        {
            if (Input.GetKeyDown(_operator.keyType))
            {
                DoAttack();
            }
        }
    }

    public void Init(Operator oper)
    {
        this._operator = oper;
    }

    public void DoAttack()
    {
        //如果Attack动画正在播放
        if (_animator.GetBool("isAttacking"))
            _animator.SetTrigger("attackAgain");
        else
            _animator.SetBool("isAttacking", true);
    }
}
