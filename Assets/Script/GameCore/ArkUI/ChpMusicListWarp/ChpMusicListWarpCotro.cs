using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using TMPro;

/// <summary>
/// 章节内部明细表现
/// </summary>
public class ChpMusicListWarpCotro : MonoBehaviour
{
    public string currentMusic;//当前的歌曲
    public Chapter currentChapter;//当前的章节
    public STAFF_LEVEL currentLevel;//当前等级
    public MusicListScrollCotro musicListSC;
    public Transform warpT;//音乐列表的Warp


    public bool mutex;

    void Awake()
    {
        mutex = false;

        musicListSC = this.transform.GetComponentInChildren<MusicListScrollCotro>();
        warpT = musicListSC.transform.Find("Viewport/Content/Warp");

    }
    void Start()
    {
        ChangeLevel(STAFF_LEVEL.EZ);
        Init("act1_cylg");
    }

    void Update()
    {
        if (mutex)
        {
            mutex = false;
        }
    }
    public void Init(string chapterName)
    {
        //加载章节
        currentChapter = Singleton<GameInfoManager>.Instance.GetChapter(chapterName);

        //加载章节的Music
        currentChapter.LoadAllMusic();

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
    /// 清除所有的
    /// </summary>
    public void Clear()
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
        //设置Scroll的长度
        musicListSC.SetItemNum(currentChapter.musics.Count);

        //渲染所有的Music放入列表
        //初始-70, 偏移 -200
        Debug.Log("这个章节的音乐数量" + currentChapter.musics.Count);
        for (int i = 0; i < currentChapter.musics.Count; i++)
        {
            //TODO 使用AB包
            GameObject mInfo = Instantiate((GameObject)Resources.Load("Prefab/UI/chapter/MusicInfoWarp"));
            MusicInfoWarpCotro m = mInfo.AddComponent<MusicInfoWarpCotro>();

            m.Init(currentChapter.musics[currentChapter.musicNames[i]]);
            m.transform.SetParent(warpT);
            m.transform.localPosition = new Vector3(551, -71 - 200 * (i - 1), 0);
            m.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    /// <summary>
    /// 修改当前列表等级
    /// </summary>
    /// <param name="sl"></param>
    public void ChangeLevel(STAFF_LEVEL sl)
    {
        currentLevel = sl;
        Singleton<EventManager>.Instance.TriggerEvent("changeLevel", new EventManager.EventParam().SetInt((int)sl));
    }
}
