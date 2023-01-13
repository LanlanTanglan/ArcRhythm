using System.Threading;
using System;
using UnityEngine;
using ArkRhythm;
public class BaseNote : MonoBehaviour
{
    Note _note;
    public virtual void Awake()
    {

    }

    public virtual void Update()
    {

    }

    public void _init(Note n)
    {
        this._note = n;
    }

    //是否被判定
    public bool IsJudge()
    {
        return true;
    }

    //是否失误
    public bool IsDead()
    {
        return true;
    }

    //设置物体位置
    public void SetPosition()
    {

    }

    //统计并且播放动画
    public virtual void DoJudgeAndDoAnime(BaseOperator bo, JUDGE_RESULT res)
    {
        // //TODO 判定动画（整合在一个函数里面）
        GameObject pObj = Instantiate((GameObject)Resources.Load("Prefab/Judge/" + Enum.GetName(typeof(JUDGE_RESULT), res)));
        pObj.transform.SetParent(bo.transform);
        BaseJudgePerfor bjp = null;
        if (res == JUDGE_RESULT.Perfect)
        {
            Singleton<GameInfoManager>.Instance.cGamePlayResult.addPerfectNum().addMaxNum(true);
            bjp = pObj.AddComponent<Perfect>();
        }
        if (res == JUDGE_RESULT.Good)
        {
            Singleton<GameInfoManager>.Instance.cGamePlayResult.addGoodNum().addMaxNum(true);
            bjp = pObj.AddComponent<Good>();
        }
        if (res == JUDGE_RESULT.Bad)
        {
            Singleton<GameInfoManager>.Instance.cGamePlayResult.addBadNum().addMaxNum(false);
            bjp = pObj.AddComponent<Bad>();
        }
        bjp.Init(bo._operator.attackRange);
    }
}