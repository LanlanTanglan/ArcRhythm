using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArcM2
{
    public static class parma
    {
        public static float perUnit = 100;
    }
    //铺面等级
    public enum STAFF_LEVEL
    {
        EZ = 1,
        HD = 2,
        IN = 3,
        SP = 4,
        WW = 5
    }

    //敌人
    public enum ENEMY
    {
        YSC = 1,
    }

    //干员
    public enum OPERATOR
    {
        Myrtle = 1//桃金娘
    }

    //Note种类
    public enum NOTE_TYPE
    {
        TAP = 1,
        LONG_TAP = 2,
        PUSH = 3,
        PUT = 4
    }

    //方向
    public enum DIRECTION
    {
        LEFT = 1,
        UP = 2,
        RIGHT = 3,
        DOWN = 4,
        IN = 5,
        OUT = 6
    }

    //判定线操作
    public enum OP
    {
        CM = 1,
        CR = 2,
        CA = 3,
        SP = 4,
        SR = 5,
        SA = 6,
    }

    //攻击范围(加上自身的位置)
    public enum ATTACK_RANGE_TYPE
    {
        Vanguard_2 = 2//例如推进之王
    }

    //数据类型
    public enum InputKeyType_PC
    {
        TAP = 1,//单个Key按下

        SLIDE = 2,//滑动

        LONG_TAP = 3,//长按
    }

    //判定结果
    public enum JUDGE_RESULT
    {
        Perfect = 1,
        Good = 2,
        Bad = 3,
        Miss = 4
    }

    [System.Serializable]
    public class StaffInfo
    {
        public string musicName;
        public string staffName;
        public string author;
        public string media;
        public STAFF_LEVEL level;
        public int length;
        public int total;
        public StaffInfo(JToken d)
        {
            this.musicName = (string)d["musicName"];
            this.staffName = (string)d["staffName"];
            this.author = (string)d["author"];
            this.media = (string)d["media"];
            this.level = (STAFF_LEVEL)(int)d["level"];
            this.length = (int)d["length"];
            this.total = (int)d["total"];
        }
    }

    [System.Serializable]
    public class TapNote
    {
        public NOTE_TYPE type;
        public ENEMY enemy;
        public int operatorIdx;
        public int attackId;//目的干员攻击范围Id
        public DIRECTION direction;
        public float endTime;
        public Vector3 offset;
        public int judgeNum;
        public bool isStatic;

        public TapNote()
        {

        }
        public TapNote(JToken d)
        {
            this.type = (NOTE_TYPE)(int)d["type"];
            this.enemy = (ENEMY)(int)d["enemy"];
            this.operatorIdx = (int)d["operatorIdx"];
            this.attackId = (int)d["attackId"];
            this.direction = (DIRECTION)(int)d["direction"];
            this.endTime = (float)d["endTime"];
            this.offset = new Vector3((float)d["offset"][0], (float)d["offset"][1], (float)d["offset"][2]);

        }

    }
    [System.Serializable]
    public class PushNote
    {
        public NOTE_TYPE type;
        public ENEMY enemy;
        public int operatorIdx;
        public int attackId;//目的干员攻击范围Id
        public DIRECTION direction;
        public int judgeCount;//判定次数
        public List<float> endTimeList;//目标判定时间
        public List<int> targetOpers;
        public PushNote(JToken d)
        {
            this.type = (NOTE_TYPE)(int)d["type"];
            this.enemy = (ENEMY)(int)d["enemy"];
            this.operatorIdx = (int)d["operatorIdx"];
            this.attackId = (int)d["attackId"];
            this.direction = (DIRECTION)(int)d["direction"];
            this.judgeCount = (int)d["judgeCount"];
            this.endTimeList = new List<float>();
            this.targetOpers = new List<int>();

            JArray etl = (JArray)d["endTimeList"];
            for (int i = 0; i < etl.Count; i++)
                endTimeList.Add((float)etl[i]);

            JArray ta = (JArray)d["targetOpers"];
            for (int i = 0; i < ta.Count; i++)
                targetOpers.Add((int)ta[i]);
        }

        public PushNote()
        {

        }
    }

    [System.Serializable]
    public class Oper
    {
        public OP type;
        public float beginTime;
        public float endTime;
        public Vector3 endPos;
        public Ease motionType;
        public Oper(JToken d)
        {
            this.type = (OP)(int)d["type"];
            this.beginTime = (float)d["beginTime"];
            this.endTime = (float)d["endTime"];
            this.endPos = new Vector3((float)d["endPos"][0], (float)d["endPos"][1], (float)d["endPos"][2]);
            this.motionType = (Ease)(int)d["motionType"];
        }
    }

    [System.Serializable]
    public class Operator
    {
        public int idx;
        public OPERATOR type;
        public ATTACK_RANGE_TYPE attackRange;
        public DIRECTION direction;
        public KeyCode keyType;
        public List<Oper> opers;
        public Operator(JToken d)
        {
            this.idx = (int)d["idx"];
            this.type = (OPERATOR)(int)d["type"];
            this.attackRange = (ATTACK_RANGE_TYPE)(int)d["attackRange"];
            this.direction = (DIRECTION)(int)d["direction"];
            this.keyType = (KeyCode)(int)d["keyType"];
            this.opers = new List<Oper>();
            JArray t = (JArray)d["opers"];
            for (int i = 0; i < t.Count; i++)
                opers.Add(new Oper(t[i]));

        }
    }



    public class Note
    {
        public List<TapNote> tapNotes;
        public List<PushNote> pushNotes;


        public Note(JToken d)
        {
            this.tapNotes = new List<TapNote>();
            this.pushNotes = new List<PushNote>();


            JArray tn = (JArray)d["tapNotes"];
            for (int i = 0; i < tn.Count; i++)
                tapNotes.Add(new TapNote(tn[i]));

            JArray pn = (JArray)d["pushNotes"];
            for (int i = 0; i < pn.Count; i++)
                pushNotes.Add(new PushNote(pn[i]));
        }
    }

    [System.Serializable]
    public class Staff
    {
        public StaffInfo staffInfo;
        public Note note;
        public List<Operator> operators;
        public Staff(JObject d)
        {
            this.staffInfo = new StaffInfo(d["staffInfo"]);
            this.note = new Note(d["note"]);
            this.operators = new List<Operator>();

            JArray o = (JArray)d["operators"];
            for (int i = 0; i < o.Count; i++)
                operators.Add(new Operator(o[i]));
        }
    }

    [System.Serializable]
    //输入状态结构
    public class InputTypePC
    {
        public int idx;
        public InputKeyType_PC type;//输入类型
        public KeyCode firstKeyName;//第一个Key键
        public KeyCode secondKeyName;//随后的Key键
        public float tappingTime;//Tap的开始时间
        public int ttl = 0;//存活帧数
        public Vector3 pos;//点击位置
        public bool isLocked = false;//是否锁住
    }

    //游戏信息
    public class GameSetting
    {
        public string assertRootPath;
    }






}