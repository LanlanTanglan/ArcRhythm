using System.Collections;
using System.Collections.Generic;
using ArkRhythm;
using UnityEngine;
using DG.Tweening;

public class Miss : BaseJudgePerfor
{
    // Start is called before the first frame update
    public override void Start()
    {
        this.transform.DOLocalMove(this.transform.localPosition + new Vector3(0, -0.84f, 0), 0.5f)
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

    public override void Init(ATTACK_RANGE_TYPE art)
    {
        base.Init(art);
    }
}
