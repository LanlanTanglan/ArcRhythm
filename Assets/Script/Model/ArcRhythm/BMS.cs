using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArcRhythm
{
    public class BMS
    {
        public BMSInfo BMSInfo;
        public List<Operator> operSet;
        public NoteSet noteSet;

        public BMS()
        {
            this.operSet = new List<Operator>();
        }

        public BMS SetParam(JToken jt)
        {
            this.BMSInfo = new BMSInfo().SetParam(jt["BMSInfo"]);

            JArray os = (JArray)jt["operSet"];
            for (int i = 0, len = os.Count; i < len; i++)
                operSet.Add(new Operator().SetParam(os[i]));

            this.noteSet = new NoteSet().SetParam(jt["noteSet"]);
            return this;
        }
    }

    //铺面信息
    public class BMSInfo
    {
        public string musicName;//音乐名称
        public string BMSName;//铺面名称
        public string author;//作者
        public string musicAuthor;
        public string media;
        public STAFF_LEVEL level;
        public int length;
        public int total;
        public BMSInfo()
        {

        }

        public BMSInfo SetParam(JToken jt)
        {
            this.musicName = (string)jt["musicName"];
            this.BMSName = (string)jt["BMSName"];
            this.author = (string)jt["author"];
            this.musicAuthor = (string)jt["musicAuthor"];
            this.media = (string)jt["media"];
            this.level = (STAFF_LEVEL)(int)jt["level"];
            this.length = (int)jt["length"];
            return this;
        }
    }
}