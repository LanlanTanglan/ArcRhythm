using UnityEngine;
using ArcRhythm;
using Util;



public class KeyboardInputManager : Singleton<KeyboardInputManager>
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

    void Update()
    {
        if (!isStopGame)
        {
            
        }
    }

    //初始化
    public void Init()
    {

    }
}


public class AndroidInputManager : Singleton<AndroidInputManager>
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

    public void Init()
    {

    }

}

public class IOSInputManager : Singleton<IOSInputManager>
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
    public void Init()
    {

    }
}