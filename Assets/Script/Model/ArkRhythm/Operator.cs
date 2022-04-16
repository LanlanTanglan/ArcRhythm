using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArkRhythm
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
        public List<OpSV> opsvList;
        public float speed;
        public Operator()
        {

        }

        public Operator SetParam(JToken jt)
        {
            opsvList = new List<OpSV>();
            this.operatorType = (OPERATOR)(int)jt["operatorType"];
            this.attackRange = (ATTACK_RANGE_TYPE)(int)jt["attackRange"];
            this.direction = (DIRECTION)(int)jt["direction"];
            this.keyType = (KeyCode)(int)jt["keyType"];
            this.speed = (float)jt["speed"];
            this.animCommands = new List<AnimCommand>();

            JArray ac = (JArray)jt["animCommands"];

            opsvList.Add(new OpSV(ArcNum.defaultBeginTime, this.speed));
            
            for (int i = 0, len = ac.Count; i < len; i++)
            {
                AnimCommand a = AnimCommandFactory.CreatAC(ac[i]);
                animCommands.Add(a);
                if (a is OpSV)
                {
                    opsvList.Add((OpSV)a);
                }
            }
            return this;
        }
    }
}