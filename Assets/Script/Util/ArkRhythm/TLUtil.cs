using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using ArkRhythm;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Converters;

using TLTemplate;

namespace TLUtil
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
        //TODO 使只能够读取AB包的内容
        public static JObject readJSON(string path)
        {
            //string类型的数据常量
            string readData;
            //获取到路径
            string fileUrl = path;

            // Singleton<ABManager>.Instance.getAssetBundle("bms").LoadAsset<>("kazimier");

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

    public class ArkRhythmUtil
    {
        //获取敌人对不同攻击范围的偏移量
        public static Vector3 GetNoteOffset(ATTACK_RANGE_TYPE art, int attackId)
        {
            if (art == ATTACK_RANGE_TYPE.Vanguard_2)
            {
                if (attackId == 1)
                {
                    return (new Vector3(250, 0, 0) + ArcNum.perY) / ArcNum.pixelPreUnit;
                }
            }
            return new Vector3(0, 0, 0);
        }

        //生成敌人的初始位置 TODO 传入的是速度曲线
        public static Vector3 EnemyInitialPos(TapNote note, Operator oper)
        {

            return new Vector3(0, 0, 0);
        }

        ///根据Note的放置时间, 以及oper的命令效果，生成距离判定点的距离
        public static float GenerateNotePos(Note note, Operator oper)
        {

            float ct = Singleton<GameClockManager>.Instance.currentGamePalyTime;
            float distance = 0;//长度

            int idx = 0;
            //找到ct的位置
            while (idx < oper.opsvList.Count && ct >= oper.opsvList[idx].beginTime)
                idx++;
            idx--;

            //计算ct与endTime的距离
            distance -= (ct - oper.opsvList[idx].beginTime) * oper.opsvList[idx].newSpeed;
            while (idx < oper.opsvList.Count - 1 && note.endTime > oper.opsvList[idx + 1].beginTime)
            {
                distance += (oper.opsvList[idx + 1].beginTime - oper.opsvList[idx].beginTime) * oper.opsvList[idx].newSpeed;
                idx++;
            }
            distance += (note.endTime - oper.opsvList[idx].beginTime) * oper.opsvList[idx].newSpeed;
            Debug.Log(ct + ":" + note.endTime + ":" + distance);

            return distance * 100;
        }


        /// <summary>
        /// 根据Note的朝向设置其位置
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="len"></param>
        public static Vector3 GetPosByDirection(DIRECTION dir, float len)
        {
            if (dir == DIRECTION.LEFT)
            {
                return new Vector3(len, 0, 0);
            }
            else if (dir == DIRECTION.UP)
            {
                return new Vector3(0, -len, 0);
            }
            else if (dir == DIRECTION.RIGHT)
            {
                return new Vector3(-len, 0, 0);
            }
            else if (dir == DIRECTION.DOWN)
            {
                return new Vector3(0, len, 0);
            }
            return Vector3.zero;
        }

        /// <summary>
        /// 获取判定完美度
        /// </summary>
        /// <param name="et">结束时间</param>
        /// <param name="ct">当前时间</param>
        /// <param name="playSound">是否播放声音</param>
        /// <returns></returns>
        public static JUDGE_RESULT GetJudgeResult(float et, float ct, bool playSound)
        {
            float c = ct - et;
            if (playSound)
            {
                if (c >= ArcNum.prJudgeTime && c < ArcNum.prJudgeTime + ArcNum.badJudgeTime)
                {
                    return JUDGE_RESULT.Bad;
                }
                //Good
                else if ((c >= -2 * ArcNum.perJudgeTime && c < -ArcNum.perJudgeTime) || (c > ArcNum.perJudgeTime && c <= 2 * ArcNum.perJudgeTime))
                {
                    Singleton<AudioManager>.Instance.AudioInstantiate("Note/Dead/tap");
                    return JUDGE_RESULT.Good;
                }
                //Perfect
                else if ((c >= -ArcNum.perJudgeTime && c <= 0) || (c >= 0 && c <= ArcNum.perJudgeTime))
                {
                    Singleton<AudioManager>.Instance.AudioInstantiate("Note/Dead/tap");
                    return JUDGE_RESULT.Perfect;
                }
                //Miss
                else //(c > ArcNum.neJudgeTime)
                {
                    return JUDGE_RESULT.Miss;
                }
            }
            else
            {
                if (c >= ArcNum.prJudgeTime && c < ArcNum.prJudgeTime + ArcNum.badJudgeTime)
                {
                    return JUDGE_RESULT.Bad;
                }
                //Good
                else if ((c >= -2 * ArcNum.perJudgeTime && c < -ArcNum.perJudgeTime) || (c > ArcNum.perJudgeTime && c <= 2 * ArcNum.perJudgeTime))
                {
                    return JUDGE_RESULT.Good;
                }
                //Perfect
                else if ((c >= -ArcNum.perJudgeTime && c <= 0) || (c >= 0 && c <= ArcNum.perJudgeTime))
                {
                    return JUDGE_RESULT.Perfect;
                }
                //Miss
                else //(c > ArcNum.neJudgeTime)
                {
                    return JUDGE_RESULT.Miss;
                }
            }

        }

        //TODO 更多的攻击范围
        public static List<Vector2> GetAttackRange(ATTACK_RANGE_TYPE art)
        {
            List<Vector2> vector2s = new List<Vector2>();
            if (art == ATTACK_RANGE_TYPE.Vanguard_2)
            {
                vector2s.Add(new Vector2(0, 0));
                vector2s.Add(new Vector2(2.5f, 0));
            }
            return vector2s;
        }

        public static List<Vector2> ChangeArrackRangeDirection(DIRECTION d, List<Vector2> v2)
        {
            List<Vector2> t = new List<Vector2>();
            switch (d)
            {
                case DIRECTION.RIGHT:
                    // Debug.Log("修改RIGHT");
                    return v2;
                case DIRECTION.LEFT:
                    foreach (Vector2 v in v2)
                        t.Add(new Vector2(-v.x, v.y));
                    // Debug.Log("修改LEFT");
                    return t;
                case DIRECTION.UP:
                    foreach (Vector2 v in v2)
                        t.Add(new Vector2(v.y, v.x));
                    // Debug.Log("修改UP");
                    return t;
                case DIRECTION.DOWN:
                    foreach (Vector2 v in v2)
                        t.Add(new Vector2(-v.y, v.x));
                    // Debug.Log("修改DOWN");
                    return t;
                default:
                    Debug.Log("无修改");
                    return v2;
            }
        }
    }

    public class DataUtil
    {
        /// <summary>
        /// 写数据，只允许写入可以序列化的类为json
        /// </summary>
        /// <param name="p">目录路径  如   a/b/ (注意有个/)</param>
        /// <param name="fn">文件名</param>
        public static void WriteData(string p, string fn)
        {
            using (StreamWriter sw = new StreamWriter(p + fn))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.TypeNameHandling = TypeNameHandling.All;

                    JsonWriter writer = new JsonTextWriter(sw);
                    // serializer.Serialize(writer, Singleton<BMSManager>.Instance.bms);
                    writer.Close();
                    sw.Close();
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }

            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="p">路径</param>
        /// <param name="fn">文件名</param>
        /// <typeparam name="T">可序列化类</typeparam>
        /// <returns></returns>
        public static T ReaderDate<T>(string p, string fn)
        {
            try
            {
                using (StreamReader sr = new StreamReader(p + fn))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.TypeNameHandling = TypeNameHandling.All;

                    JsonReader reader = new JsonTextReader(sr);
                    return serializer.Deserialize<T>(reader);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return default(T);
            }
        }
    }
}