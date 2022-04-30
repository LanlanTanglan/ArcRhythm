using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;


/// <summary>
/// 音乐列表
/// </summary>
public class MusicInfoWarpCotro : MonoBehaviour
{
    public int currentLevel;
    public List<int> level;//铺面等级
    public Music musicInfo;
    public Transform musicNameT;
    public Transform authorT;
    public Transform levelCountT;
    private Action<EventManager.EventParam> changeLevel;
    void Awake()
    {
        musicNameT = transform.Find("Info/MusicName");
        authorT = transform.Find("Info/Author");
        levelCountT = transform.Find("Level/count");

        //注册事件
        changeLevel = new Action<EventManager.EventParam>(ChangeLevel);
        Singleton<EventManager>.Instance.StartListening("changeLevel", changeLevel);
    }
    void Start()
    {

    }
    void Update()
    {

    }

    public void Init()
    {

    }

    public void ChangeLevel(EventManager.EventParam eventParam)
    {
        
    }
}
