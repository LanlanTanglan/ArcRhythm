using UnityEngine;
using ArcM2;
using Util;



public class PCInputManager : Singleton<PCInputManager>
{
    public bool isStopGame = false;

    override protected void OnAwake()
    {
        //注册事件
        Singleton<GameProcessManager>.Instance.StopGameEvent += StopGame;
    }

    //暂停游戏
    private void StopGame(bool key)
    {
        this.isStopGame = key;
    }

    //初始化
    public void Init()
    {
        
    }
}


public class AndroidInputManager : Singleton<AndroidInputManager>
{
    override protected void OnAwake()
    {
    }

    public void Init()
    {
        
    }
}

public class IOSInputManager : Singleton<IOSInputManager>
{
    override protected void OnAwake()
    {
    }
    public void Init()
    {
        
    }
}