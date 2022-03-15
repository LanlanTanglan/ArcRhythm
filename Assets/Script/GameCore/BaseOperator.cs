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
    void Start()
    {

    }

    void Update()
    {

    }


    //初始化变量以及相关动画
    public void Init(Operator o)
    {
        this.o = o;

        //初始化判定线动画
        InitOperatorAnim();
    }

    //初始化判定线(干员)动画
    private void InitOperatorAnim()
    {
        //创建一个Sequence
        Sequence sq = DOTween.Sequence();
        //循环遍历动画命令
        foreach (AnimCommand ac in o.animCommands)
        {
            sq.Append(ac.GetTween(this.transform));
        }
    }

}
