using TLUI;
using UnityEngine;

public class MainPanel : TLPanel
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }

    public void CreateModPage_btn()
    {
        TLUIPanelManager.Instance.OpenPanel("CreateModPage");
        TLUIPanelManager.Instance.ShowPanel("CreateModPage");
    }
}