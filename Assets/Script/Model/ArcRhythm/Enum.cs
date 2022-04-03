using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;
namespace ArcRhythm
{
    [System.Serializable]
    public static class parma
    {
        public static float perUnit = 100;
    }
    //铺面等级
    [System.Serializable]
    public enum STAFF_LEVEL
    {
        EZ = 1,
        HD = 2,
        IN = 3,
        SP = 4,
        WW = 5
    }
    [System.Serializable]
    //敌人
    public enum ENEMY
    {
        YSC = 1,
    }
    [System.Serializable]
    //干员
    public enum OPERATOR
    {
        Myrtle = 1,//桃金娘
        Bpipe = 2
    }
    [System.Serializable]
    //Note种类
    public enum NOTE_TYPE
    {
        TAP = 1,
        LONG_TAP = 2,
        PUSH = 3,
        PUT = 4
    }
    [System.Serializable]
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
    [System.Serializable]
    //判定线操作
    public enum ANIM_COMMAND
    {
        OP_CM = 1,
        OP_CR = 2,
        OP_CA = 3,
        OP_SV = 4,
    }
    [System.Serializable]
    //攻击范围(加上自身的位置)
    public enum ATTACK_RANGE_TYPE
    {
        Vanguard_2 = 2//例如推进之王
    }
    [System.Serializable]
    //数据类型
    public enum InputType
    {
        TAP = 1,//单个Key按下

        SLIDE = 2,//滑动

        LONG_TAP = 3,//长按

        Drag = 4,//触控即可
    }
    [System.Serializable]
    //判定结果
    public enum JUDGE_RESULT
    {
        Perfect = 1,
        Good = 2,
        Bad = 3,
        Miss = 4
    }

}