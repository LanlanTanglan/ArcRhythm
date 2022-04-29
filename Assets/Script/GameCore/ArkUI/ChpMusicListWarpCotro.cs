using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;

/// <summary>
/// 章节内部明细表现
/// </summary>
public class ChpMusicListWarp : MonoBehaviour
{
    public string currentMusic;//当前的歌曲
    public Chapter currentChapter;//当前的章节
    public delegate void ChangeMusic(string mn);
    public event ChangeMusic ChangeMusicInfo;


    void Awake()
    {

    }
    void Start()
    {

    }

    void Update()
    {

    }
    public void Init(string chapterName)
    {
        //加载章节
        currentChapter = Singleton<GameInfoManager>.Instance.GetChapter(chapterName);

        //将章节的相关信息渲染到屏幕上
        RenderWarp();
    }
    /// <summary>
    /// 刷新
    /// </summary>
    public void Refresh()
    {

    }

    /// <summary>
    /// 根据数据渲染屏幕
    /// </summary>
    public void RenderWarp()
    {
        RenderMusicList();
    }

    public void RenderMusicList()
    {
        //首先加载所有的Music
        currentChapter.LoadAllMusic();
        //渲染所有的Music放入列表
        //间隔160；从-130开始
        //TODO 使用AB包
        
    }
}
