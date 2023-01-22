using System.Threading;
using System;
using UnityEngine;
using ArkRhythm;
using TLUtil;
using ArkTemplate;

public class BaseNote : MonoBehaviour
{
    Note _note;
    public GameObject _noteImagePrefeb;
    public BaseOperator _targetOperator;
    public virtual void Awake()
    {

    }

    public virtual void Update()
    {

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

    public virtual void Init(Note n)
    {
        _note = n;
    }

    /// <summary>
    /// 设置Note的最基本的状态
    /// </summary>
    public virtual void SetNote()
    {
        //设置Note贴图
        GameObject o = (GameObject)Resources.Load("Prefab/Enemy/" + Enum.GetName(typeof(ENEMY), _note.enemy));
        _noteImagePrefeb = Instantiate(o);
        _noteImagePrefeb.transform.parent = this.transform;
        _noteImagePrefeb.transform.localPosition = Vector3.zero;

        //设置Note初始位置
        this._targetOperator = BMSManager.Instance._baseOperators[_note.targetOperId];
        this.transform.parent = _targetOperator._attackRanges[_note.attackId].transform;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        //设置Note距离Attack的范围
        this.transform.localPosition += ArkRhythmUtil.GetPosByDirection(_note.direction, ArkRhythmUtil.GenerateNotePos(_note, _targetOperator._operator)) / 100;
    }

    /// <summary>
    /// 更新初始下落的位置
    /// </summary>
    public virtual void UpdateAlivePos()
    {
        if (GameClockManager.Instance.isGameBegin)
        {
            this.transform.localPosition += ArkRhythmUtil.GetPosByDirection(_note.direction, -_targetOperator._operator.speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 从始至终没有判定
    /// </summary>
    /// <returns></returns>
    public virtual bool AliveMiss()
    {
        float ct = GameClockManager.Instance.currentGamePalyTime;
        return ct - _note.endTime > ArcNum.neJudgeTime;
    }

    /// <summary>
    /// 第一次通用判定
    /// </summary>
    /// <returns></returns>
    public virtual bool AliveJudge()
    {
        float ct = GameClockManager.Instance.currentGamePalyTime;
        return ct - _note.endTime >= ArcNum.prJudgeTime &&
               ct - _note.endTime <= ArcNum.neJudgeTime &&
               Singleton<KeyboardInputManager>.Instance.LoadInputState(_targetOperator._operator.keyType, InputType.TAP);
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
        if (res == JUDGE_RESULT.Miss)
        {
            Singleton<GameInfoManager>.Instance.cGamePlayResult.addMissNum().addMaxNum(false);
            bjp = pObj.AddComponent<Miss>();
        }
        bjp.Init(bo._operator.attackRange);
    }
}