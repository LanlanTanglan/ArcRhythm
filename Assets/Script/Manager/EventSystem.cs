using UnityEngine;
using ArcM2;
using Util;


/// <summary>
/// 游戏进程事件管理
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
}