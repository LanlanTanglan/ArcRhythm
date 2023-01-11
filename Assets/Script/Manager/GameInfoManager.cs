using UnityEngine;
using ArkRhythm;
using TLUtil;
using System.Collections;
using System.Collections.Generic;
public class GameInfoManager : Singleton<GameInfoManager>
{
    //当前的游玩铺面的信息汇总对象
    public GamePlayResult cGamePlayResult = new GamePlayResult();
    //TODO 改成GameInfo大类
    public ChapterData chapterData = new ChapterData();

    /// <summary>
    /// 获取章节
    /// </summary>
    /// <param name="s"></param>
    public Chapter GetChapter(string s)
    {
        if (!chapterData.chapters.ContainsKey(s))
        {
            chapterData.LoadChapter(s);
        }
        
        return chapterData.chapters[s];
    }
}