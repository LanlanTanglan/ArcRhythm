using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLUI;
using TLTemplate;

public class BMSCManager : Singleton<BMSCManager>
{
    private string _root = "BMSCreator/UI/Panel/";
    public string _rootBMS = "";
    public bool isTest = true;

    void Awake()
    {

    }

    void Start()
    {
        if (isTest)
        {
            TLUIPanelManager.Instance.LoadPanelPrefab(_root, "EnterPanel");
            TLUIPanelManager.Instance.LoadPanelPrefab(_root, "MainPanel");
            TLUIPanelManager.Instance.LoadPanelPrefab(_root+"CreateModPage/", "CreateModPage");
            TLUIPanelManager.Instance.OpenPanel("EnterPanel");
            TLUIPanelManager.Instance.ShowPanel("EnterPanel");
        }
    }
}
