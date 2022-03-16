using UnityEngine;

public class GameClockManager : Singleton<GameClockManager>
{
    public bool isStopGame = false;
    public float globalTime = 0;//游戏全局时间
    public float currentGamePalyTime = -5f;//当前游戏时间
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