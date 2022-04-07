using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using ArcRhythm;


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
        //TODO 使只能够读取AB包的内容
        public static JObject readJSON(string path)
        {
            //string类型的数据常量
            string readData;
            //获取到路径
            string fileUrl = Application.streamingAssetsPath + "\\" + path;

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

    public class ArcMUtil
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
            while (idx < oper.opsvList.Count && ct > oper.opsvList[idx].beginTime)
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
    }
}