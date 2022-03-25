using System.Collections;
using System.Collections.Generic;
using ArcRhythm;
using UnityEngine;
using DG.Tweening;

public class Perfect : BaseJudgePerfor
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Init(ATTACK_RANGE_TYPE art, int idx)
    {
        base.Init(art, idx);
        this.transform.DOLocalMove(this.transform.localPosition + new Vector3(0, 0.64f, 0), 0.5f)
        .SetEase(Ease.OutElastic)
        .OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
