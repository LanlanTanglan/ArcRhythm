using System.Collections.Generic;
using UnityEngine;
using ArcRhythm;
using Util;


public class KeyboardInputManager : Singleton<KeyboardInputManager>
{
    public bool isStopGame = false;

    public List<BaseInput> currentInputState;
    override protected void OnAwake()
    {
        currentInputState = new List<BaseInput>();
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
            InputStateUpdate();
            InputListener();
        }
    }

    //监听常用按键
    public void InputListener()
    {
        //A
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Debug.Log("A键按下");
            AddInputState(KeyCode.A, InputType.TAP, 2);
            AddInputState(KeyCode.A, InputType.LONG_TAP, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Debug.Log("A键持续ing~~");
            AddInputState(KeyCode.A, InputType.LONG_TAP, 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            // Debug.Log("A键抬起");
        }
    }

    //更新按键状态栈
    public void InputStateUpdate()
    {
        for (int i = currentInputState.Count - 1; i >= 0; i--)
        {
            //超出存活时间则清除
            if (currentInputState[i].TTLUpdate() <= 0)
                currentInputState.Remove(currentInputState[i]);
        }
    }

    //占有某个按键状态
    public bool LoadInputState(KeyCode k, InputType it)
    {
        foreach (BaseInput b in currentInputState)
        {
            if (b.IsRightInputType(it) && b.IsRightKeyType(k))
            {
                b.Lock();
                return true;
            }
        }
        return false;
    }

    //添加输入状态
    public void AddInputState(KeyCode kc, InputType it, int ttl)
    {
        if (it == InputType.TAP)
        {
            Debug.Log("添加一个");
            BaseInput bi = new KeyboardInput().SetInpuKey(kc, kc).SetTTL(ttl).SetInputType(it);
            this.currentInputState.Add(bi);
            return;
        }
        else if (it == InputType.LONG_TAP)
        {
            //判断这个按键的长按是否存在
            foreach (BaseInput b in currentInputState)
            {
                if (b.IsRightInputType(InputType.LONG_TAP) && b.IsRightKeyType(kc))
                {
                    b.ProlongTTL();
                    return;
                }
            }
            BaseInput bi = new KeyboardInput().SetInpuKey(kc, kc).SetTTL(ttl).SetInputType(it);
            this.currentInputState.Add(bi);
            return;
        }
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