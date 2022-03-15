using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using ArcM2;


namespace Util
{
    public class AnimatorUtil
    {
        public static float GetAnimatorLength(Animator animator, string name)
        {
            //动画片段时间长度
            float length = 0;

            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            //Debug.Log(clips.Length);
            foreach (AnimationClip clip in clips)
            {
                //Debug.Log(clip.name);
                if (clip.name.Equals(name))
                {
                    length = clip.length;
                    break;
                }
            }
            return length;
        }
    }
    public class JsonUtil
    {
        public static JObject readJSON(string path)
        {
            //string类型的数据常量
            string readData;
            //获取到路径
            string fileUrl = Application.streamingAssetsPath + "\\" + path;
            //读取文件
            using (StreamReader sr = File.OpenText(fileUrl))
            {
                //数据保存
                readData = sr.ReadToEnd();
                Debug.Log(readData);
                sr.Close();
            }
            return JObject.Parse(readData);
        }
    }

    public class KeyCodeUtil
    {
        public static KeyCode GetKeyCodeByStr(string s)
        {
            if (s == "A")
            {
                return KeyCode.A;
            }
            //默认空格
            else
            {
                return KeyCode.Space;
            }
        }
    }

    public class ArcMUtil
    {
        //获取敌人对不同攻击范围的偏移量
        public static Vector3 GetEnemyOffset(ATTACK_RANGE_TYPE art, int attackId)
        {
            if (art == ATTACK_RANGE_TYPE.Vanguard_2)
            {
                if (attackId == 1)
                {
                    return new Vector3(128, 0, 0) / parma.perUnit;
                }
            }
            return new Vector3(0, 0, 0);
        }

        //生成敌人的初始位置 TODO 传入的是速度曲线
        public static Vector3 EnemyInitialPos(TapNote note, Operator oper)
        {
            
            return new Vector3(0, 0, 0);
        }

    }
}