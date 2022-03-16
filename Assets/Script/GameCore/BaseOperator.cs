using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;
using ArcRhythm;


/// <summary>
/// Operator行为控制脚本
/// </summary>
public class BaseOperator : MonoBehaviour
{

    public Operator o;
    public int oaIdx = 0;//判定线动画指针
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
        if (!isStopGame)
        {
            UpdateOperterAnim();
        }
    }

    #region 事件注册块
    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    #endregion

    //初始化变量以及相关动画
    public void Init(Operator o)
    {
        this.o = o;
    }


    //更新判定线动画
    private void UpdateOperterAnim()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;

        while (ct >= o.animCommands[oaIdx].beginTime)
        {
            //动画基于SpriteRender
            if (o.animCommands[oaIdx].animCommandType == ANIM_COMMAND.OP_CA)
            {
                SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
                o.animCommands[oaIdx].GetTween(sp).Play();
            }
            //使用Transform
            else
            {
                o.animCommands[oaIdx].GetTween(this.transform).Play();
            }
        }
        //向下移动
        oaIdx++;
    }


}
