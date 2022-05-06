using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// 音乐列表
/// </summary>
public class MusicInfoWarpCotro : MonoBehaviour, IPointerClickHandler
{
    public ChpMusicListWarpCotro cmlwC;

    public int currentLevel;
    public Music musicInfo;
    public TMP_Text musicNameTMP;
    public TMP_Text authorTMP;
    public TMP_Text levelCountTMP;
    private Action<EventManager.EventParam> changeLevel;//修改等级

    void Awake()
    {
        cmlwC = transform.GetComponentInParent<ChpMusicListWarpCotro>();
        musicNameTMP = transform.Find("Info/MusicName").GetComponent<TMP_Text>();
        authorTMP = transform.Find("Info/Author").GetComponent<TMP_Text>();
        levelCountTMP = transform.Find("Level/count").GetComponent<TMP_Text>();



        //注册事件
        changeLevel = new Action<EventManager.EventParam>(ChangeLevel);
        Singleton<EventManager>.Instance.StartListening("changeLevel", changeLevel);
    }

    void Start()
    {
        ChangeLevel(new EventManager.EventParam().SetInt((int)STAFF_LEVEL.EZ));
        //设置内容
        musicNameTMP.text = musicInfo.musicName;
        authorTMP.text = musicInfo.author;
        levelCountTMP.text = musicInfo.level[(int)currentLevel - 1] + "";
    }
    void Update()
    {

    }

    public void Init(Music m)
    {
        musicInfo = m;
    }

    public void ChangeLevel(EventManager.EventParam eventParam)
    {
        //当前等级
        currentLevel = eventParam.i;

        //将当前的等级数字修改为正确的等级
        levelCountTMP.text = musicInfo.level[(int)currentLevel - 1] + "";
    }

    /// <summary>
    /// 当被点击
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //当前选中的歌曲与点击相同，则无需修改相关信息
        if (musicInfo.musicName == cmlwC.currentMusic.musicName)
            return;

        //修改信息
        cmlwC.currentMusic = musicInfo;
        // 刷新
        cmlwC.Refresh();
    }   

}
