using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Util;
using UnityEngine;

namespace ArkRhythm
{
    [System.Serializable]
    public class GamePlayResult
    {
        [JsonIgnore]
        public string bmsName;//铺面名称
        public int totalNoteNum;//这个铺面的总共的物量 TODO 考虑长按条的物量(算为1)
        public float accuracyRate;//准确度
        public int score;//分数
        public int goodNum;
        public int badNum;
        public int perfectNum;
        public int missNum;
        public int maxCombo;//最大连击数
        public int resultLevel;//结果等级,D,C,B,A,S,V,(至臻--主题，危机合约等徽章改成金黄色)

        [JsonIgnore]
        public int currentMaxCombo;

        public GamePlayResult addGoodNum()
        {
            this.goodNum++;
            return this;
        }
        public GamePlayResult addPerfectNum()
        {
            this.perfectNum++;
            return this;
        }
        public GamePlayResult addBadNum()
        {
            this.badNum++;
            return this;
        }
        public GamePlayResult addMissNum()
        {
            this.missNum++;
            return this;
        }

        public GamePlayResult addMaxNum(bool m)
        {
            if (m) currentMaxCombo++;
            else currentMaxCombo = 0;
            if (currentMaxCombo > maxCombo) maxCombo = currentMaxCombo;

            RefreshResult();
            return this;
        }

        //更新结果
        public void RefreshResult()
        {
            this.score = (int)((9 * this.perfectNum + 5.85f * this.goodNum + this.maxCombo) / this.totalNoteNum * 100000);
        }

        public GamePlayResult setTotalNoteNum(int t)
        {
            this.totalNoteNum = t;
            return this;
        }

        public GamePlayResult()
        {
            this.badNum = 0;
            this.goodNum = 0;
            this.perfectNum = 0;
            this.missNum = 0;
            this.maxCombo = 0;
            this.currentMaxCombo = 0;
        }

        public GamePlayResult ClearInfo()
        {
            this.badNum = 0;
            this.goodNum = 0;
            this.perfectNum = 0;
            this.missNum = 0;
            this.maxCombo = 0;
            this.currentMaxCombo = 0;

            return this;
        }
    }

    [System.Serializable]
    /// <summary>
    /// 游戏信息类,保存的是整个游戏的信息
    /// </summary>
    public class GameSaveData
    {
        public ChapterData chapterData;
        public GameSaveData()
        {

        }
    }

    [System.Serializable]
    /// <summary>
    /// 用户信息--大类
    /// </summary>
    public class UserData
    {

    }

    [System.Serializable]
    /// <summary>
    /// 章节信息--大类
    /// </summary>
    public class ChapterData
    {
        public List<string> chapterNames;
        [JsonIgnore]
        public Dictionary<string, Chapter> chapters = new Dictionary<string, Chapter>();
        public ChapterData()
        {

        }

        //加载所有的铺面
        public void LoadAllChapter()
        {
            foreach (string c in chapterNames)
            {
                if (!chapters.ContainsKey(c))
                {
                    chapters.Add(c, DataUtil.ReaderDate<Chapter>(Application.streamingAssetsPath + "/GameSaveData/Chapters/", c + ".json"));
                }
            }
        }
        public void LoadChapter(string c)
        {
            if (!chapters.ContainsKey(c))
            {
                chapters.Add(c, DataUtil.ReaderDate<Chapter>(Application.streamingAssetsPath + "/GameSaveData/Chapters/", c + ".json"));
            }
        }
    }


    [System.Serializable]
    /// <summary>
    /// 章节
    /// </summary>
    public class Chapter
    {
        public string chapterName;//章节名
        public List<string> musicNames;//曲目名
        public string chapterImg;//章节封面


        [JsonIgnore]
        public Dictionary<string, Music> musics = new Dictionary<string, Music>();//曲目
        [JsonIgnore]
        public MusicSet musicSet;
        public Chapter()
        {

        }

        /// <summary>
        /// 加载音乐
        /// </summary>
        public void LoadAllMusic()
        {
            // foreach (string s in musicNames)
            // {
            //     if (!musics.ContainsKey(s))
            //     {
            //         musics.Add(s, DataUtil.ReaderDate<Music>(Application.streamingAssetsPath + "/GameSaveData/Music/", s + ".json"));
            //     }
            // }
            MusicSet musicSet = DataUtil.ReaderDate<MusicSet>(Application.streamingAssetsPath + "/GameSaveData/Music/", chapterName + "_musics.json");

            foreach (Music m in musicSet.musics)
            {
                if (!musics.ContainsKey(m.musicName))
                {
                    musics.Add(m.musicName, m);
                }
            }
        }
        public void LoadMusic()
        {

        }
    }

    [System.Serializable]
    public class MusicSet
    {
        public List<Music> musics;
        public MusicSet()
        {

        }
    }

    [System.Serializable]
    /// <summary>
    /// 音乐
    /// </summary>
    public class Music
    {

        public string musicName;//音乐名
        public string author;
        public List<int> level;//等级

        [JsonIgnore]
        public Dictionary<string, GamePlayResult> gamePlayResult;//曲目
        public Music()
        {

        }



        /// <summary>
        /// 加载这个音乐名加载铺面BMS
        /// </summary>
        /// <returns>是否加载成功</returns>
        public BMS LoadBMS()
        {
            return null;
        }
    }
}