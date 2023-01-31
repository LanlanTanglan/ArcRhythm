using System.Collections;
using System.Collections.Generic;
using ArkRhythm;
using UnityEngine;
using DG.Tweening;

public class Perfect : BaseJudgePerfor
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //载入一个例子Prefab
        GameObject myPratical = Instantiate((GameObject)Resources.Load("Prefab/Particle/CFX_MagicPoof"), this.transform);
        myPratical.transform.localPosition = new Vector3(0, 0, 0);
        myPratical.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        myPratical.gameObject.AddComponent<CFX_AutoDestructShuriken>();
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
