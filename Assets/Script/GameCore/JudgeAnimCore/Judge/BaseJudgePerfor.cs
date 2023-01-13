using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using TLUtil;
using DG.Tweening;

public class BaseJudgePerfor : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        this.transform.DOLocalMove(this.transform.localPosition + new Vector3(0, 0.84f, 0), 0.5f)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    Destroy(this.gameObject);
                });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Init(ATTACK_RANGE_TYPE art)
    {
        //回到原点
        this.transform.localPosition = new Vector3(0, 0, -3);
        //解除与父物体的绑定关系
        this.transform.SetParent(null);
        //无角度
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
