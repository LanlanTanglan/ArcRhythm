using UnityEngine;
using TLUtil;
using TLTemplate;
using ArkRhythm;


/// <summary>
/// 游戏进程事件管理
/// TODO 使用事件池作为事件接收中心
/// </summary>
public class GameProcessManager : Singleton<GameProcessManager>
{
    //游戏暂停接口
    public delegate void StopGameHandel(bool key);
    //游戏暂停接口事件
    public event StopGameHandel StopGameEvent;
    
    
    /// <summary>
    /// 是否暂停游戏
    /// </summary>
    /// <param name="key">是否</param>
    public void StopGame(bool key)
    {
        this.StopGameEvent(key);
    }

    public bool click = false;
    public bool mutex = false;
    void Update()
    {
        if(click)
        {
            if(!mutex)
            {
                StopGame(true);
                mutex = true;
            }
        }
        else
        {
            if(mutex)
            {
                StopGame(false);
                mutex = false;
            }
        }
    }

    //铺面事件
    public delegate void BMSHandel();

    //铺面加载完成
    public event BMSHandel BMSLoaded;
}