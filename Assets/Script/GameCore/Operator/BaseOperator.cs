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
    public SpriteRenderer spriteRenderer;

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
        //设置Obj的初始位置
        this.transform.localPosition = Vector3.zero;
        //设置Alpha值
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        Color color = this.spriteRenderer.color;
        color.a = 1;
        this.spriteRenderer.color = color;
        
        //TODO Note信息统计

        //TODO 设置Obj的攻击范围

    }


    //更新判定线动画
    private void UpdateOperterAnim()
    {
        float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;

        while (oaIdx < o.animCommands.Count && ct >= o.animCommands[oaIdx].beginTime)
        {
            //动画基于SpriteRender
            if (o.animCommands[oaIdx].animCommandType == ANIM_COMMAND.OP_CA)
            {
                SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
                o.animCommands[oaIdx].GetTween(sp).Play();
                //TODO 一级子节点变化
                //TODO 二级子节点变化

            }
            //使用Transform
            else
            {
                o.animCommands[oaIdx].GetTween(this.transform).Play();
            }
            //下一条命令
            oaIdx++;
        }
    }

    /// <summary>
    /// 生成判定动画
    /// </summary>
    /// <param name="note"></param>
    public void CreateJudgeAnim(Note note)
    {

    }
}
