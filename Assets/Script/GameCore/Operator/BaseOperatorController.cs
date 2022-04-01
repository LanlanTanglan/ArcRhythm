using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcRhythm;
using Util;


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

    public Animator animator;
    public Operator o;
    void Awake()
    {
        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }
    void Start()
    {
        //获取Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isStopGame)
        {
            if (Input.GetKeyDown(o.keyType))
            {
                DoAttack();
            }
        }
    }

    public void Init(Operator oper)
    {
        this.o = oper;
    }

    public void DoAttack()
    {
        //如果Attack动画正在播放
        if (animator.GetBool("isAttacking"))
            animator.SetTrigger("attackAgain");
        else
            animator.SetBool("isAttacking", true);
    }
}
