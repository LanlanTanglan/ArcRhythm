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
}