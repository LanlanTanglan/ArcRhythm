using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcRhythm;
using Util;
using DG.Tweening;

public class BaseJudgePerfor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Init(ATTACK_RANGE_TYPE art, int idx)
    {
        //回到原点
        this.transform.localPosition = Vector3.zero;
        //根据攻击范围以及idx确定这些效果的位置
        this.transform.localPosition = ArcMUtil.GetNoteOffset(art, idx);
        
        //解除与父物体的绑定关系
        this.transform.SetParent(null);
        //无角度
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
