using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLUI;

public class FixNavBar : TLBaseUI
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    //返回按钮
    public void back_Btn()
    {
        TLUIPanelManager.Instance.Leave1Panel();
    }
}
