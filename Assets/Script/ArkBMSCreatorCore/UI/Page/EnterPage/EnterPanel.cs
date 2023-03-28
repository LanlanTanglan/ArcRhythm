using UnityEngine;
using TLUI;
using DG.Tweening;

public class EnterPanel : TLPanel
{
    public bool isKeyAPressed = false;
    

    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.A) && !isKeyAPressed)
        {
            isKeyAPressed = true;
            //跳转至下一个页面
            TLUIPanelManager.Instance.OpenPanel("MainPanel");
            TLUIPanelManager.Instance.ShowPanel("MainPanel");
        }
    }
}