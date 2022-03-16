using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArcRhythm
{
    [System.Serializable]
    /// <summary>
    /// 干员
    /// </summary>
    public class Operator
    {
        public OPERATOR operatorType;
        public ATTACK_RANGE_TYPE attackRange;
        public DIRECTION direction;
        public KeyCode keyType;
        public List<AnimCommand> animCommands;
        public Operator()
        {

        }

        public Operator SetParam(JToken jt)
        {
            this.operatorType = (OPERATOR)(int)jt["operatorType"];
            this.attackRange = (ATTACK_RANGE_TYPE)(int)jt["attackRange"];
            this.direction = (DIRECTION)(int)jt["direction"];
            this.keyType = (KeyCode)(int)jt["keyType"];

            this.animCommands = new List<AnimCommand>();

            JArray ac = (JArray)jt["animCommands"];
            for (int i = 0, len = ac.Count; i < len; i++)
                animCommands.Add(AnimCommandFactory.CreatAC(ac[i]));

            return this;
        }
    }
}