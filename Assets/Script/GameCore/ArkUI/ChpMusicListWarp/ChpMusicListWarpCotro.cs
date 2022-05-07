using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using TMPro;
using DG.Tweening;


/// <summary>
/// 章节内部明细表现
/// </summary>
public class ChpMusicListWarpCotro : MonoBehaviour
{
    public Music currentMusic;//当前的歌曲
    public Chapter currentChapter;//当前的章节
    public STAFF_LEVEL currentLevel;//当前等级
    public Transform warpT;//音乐列表的Warp

    public Transform leftWarpG;
    public Transform rightWarpG;


    public MusicListScrollCotro musicListSC;
    public SpriteRenderer backgroundSR;
    public SpriteRenderer musicInfoCoverSR;
    public LevelBarCotro levelBarCotro;


    public bool mutex;

    void Awake()
    {
        mutex = false;

        musicListSC = this.transform.GetComponentInChildren<MusicListScrollCotro>();
        warpT = musicListSC.transform.Find("Viewport/Content/Warp");
        backgroundSR = transform.Find("Background").GetComponent<SpriteRenderer>();
        musicInfoCoverSR = transform.Find("MusicInfoWarp/MusicCoverMask/MusicCover").GetComponent<SpriteRenderer>();
        levelBarCotro = transform.GetComponentInChildren<LevelBarCotro>();

        leftWarpG = transform.GetChild(1);
        rightWarpG = transform.GetChild(2);

    }
    void Start()
    {
        ChangeLevelList(STAFF_LEVEL.EZ);
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

        //设置当前的音乐为列表第一个音乐
        currentMusic = currentChapter.musicSet.musics[0];
        Refresh();
    }

    /// <summary>
    /// 刷新
    /// </summary>
    public void Refresh()
    {
        //修改背景的图片
        ChangeBackground();

        //修改右侧信息
        //--修改右侧Cover图片
        ChangeMusicInfoCover();

        //--修改右侧下方levelBar信息
        ChangeLevelBar();
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
    public void ChangeLevelList(STAFF_LEVEL sl)
    {
        currentLevel = sl;
        Singleton<EventManager>.Instance.TriggerEvent("changeLevel", new EventManager.EventParam().SetInt((int)sl));
    }

    /// <summary>
    /// 修改Levelbar
    /// </summary>
    public void ChangeLevelBar()
    {
        //设置有多少个等级可选
        levelBarCotro.SetLevelCount(currentMusic.level);

        //设置背景条的长度
        levelBarCotro.ChangeLevelWarpNum(currentMusic.level.Count);

        //设置当前难度
        if ((int)currentLevel > currentMusic.level.Count)
        {
            levelBarCotro.hoverLevelCotro.ChangeLevel((STAFF_LEVEL)currentMusic.level.Count, currentMusic.level[currentMusic.level.Count - 1]);
            return;
        }
        levelBarCotro.hoverLevelCotro.ChangeLevel(currentLevel, currentMusic.level[(int)currentLevel - 1]);

    }

    /// <summary>
    /// 修改背景
    /// </summary>
    public void ChangeBackground()
    {
        AssetBundle backdrop = Singleton<ABManager>.Instance.GetAssetBundle("backdrop");
        Sprite s = backdrop.LoadAsset<Sprite>(currentMusic.background + "_gs");
        backgroundSR.sprite = s;

        //TODO 调整大小

    }

    /// <summary>
    /// 修改InfoCover
    /// </summary>
    public void ChangeMusicInfoCover()
    {
        AssetBundle backdrop = Singleton<ABManager>.Instance.GetAssetBundle("backdrop");
        Sprite s = backdrop.LoadAsset<Sprite>(currentMusic.background);
        musicInfoCoverSR.sprite = s;
    }

    public void PlayAnim()
    {
        leftWarpG.DOLocalMoveX(-15, 1f).AsyncWaitForStart();
        rightWarpG.DOLocalMoveX(15f, 1f).AsyncWaitForStart();
    }
}
