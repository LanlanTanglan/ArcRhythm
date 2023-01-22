using UnityEngine;
using ArkTemplate;
using ArkRhythm;

/// <summary>
/// 游戏时钟管理器
/// 管理游戏进程的时钟，与铺面延迟有着相似的关系
/// </summary>
public class GameClockManager : Singleton<GameClockManager>
{
    public bool isStopGame = false;
    public float globalTime = 0;//游戏全局时间
    public float currentGamePalyTime = ArcNum.defaultBeginTime;//当前游戏时间
    public bool isGameBeginGlobal = false;
    public bool isGameBegin = false;

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

    void FixedUpdate()
    {

    }

    void Update()
    {
        if (isGameBeginGlobal)
        {
            globalTime += Time.deltaTime;
        }
        if (isGameBegin && !isStopGame)
        {
            currentGamePalyTime += Time.deltaTime;
        }
    }

    public void GameBegin()
    {
        isGameBeginGlobal = true;
    }

    public void CurrentGameBegin()
    {
        currentGamePalyTime = -5;
        isGameBegin = true;
    }
}