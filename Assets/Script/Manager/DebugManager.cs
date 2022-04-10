using UnityEngine;
using ArcRhythm;
using Util;
using UnityEngine.UI;

public class DebugManager : Singleton<DebugManager>
{
    public bool isShowDebug = false;
    public bool mustex = false;

    public GameObject debugPanel;
    public CanvasGroup cg;

    public Text debugText;


    void Start()
    {
        //获取DebugPanel
        debugPanel = GameObject.Find("Canvas/DebugInfoPanel");
        cg = debugPanel.GetComponent<CanvasGroup>();
        Debug.Log("debug: " + debugPanel.name);
        debugText = GameObject.Find("Canvas/DebugInfoPanel/Info").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isShowDebug = !isShowDebug;
            //显示
            if (isShowDebug)
            {
                CanvasGroup cg = debugPanel.transform.GetComponent<CanvasGroup>();
                cg.alpha = 1;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
            else
            {
                CanvasGroup cg = debugPanel.transform.GetComponent<CanvasGroup>();
                cg.alpha = 0;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        }
        if (isShowDebug)
        {
            debugText.text = "";
            debugText.text += "全局游戏时间: " + Singleton<GameClockManager>.Instance.currentGamePalyTime + "\n";
            debugText.text += "当前游戏执行时间: " + Singleton<GameClockManager>.Instance.currentGamePalyTime + "\n";
            debugText.text += "当前得分: " + Singleton<GameInfoManager>.Instance.cGamePlayResult.score + "\n";
            debugText.text += "最大连击: " + Singleton<GameInfoManager>.Instance.cGamePlayResult.maxCombo + "\n";
        }
    }
    
    public void Init()
    {

    }
}