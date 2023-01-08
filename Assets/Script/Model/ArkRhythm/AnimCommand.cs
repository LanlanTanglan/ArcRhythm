using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

/// <summary>
/// 我们规定，单元的像素长度是100，并且写的时候是以 x * 100的形式表达
/// </summary>

namespace ArkRhythm
{
    [System.Serializable]
    public class AnimCommandFactory
    {
        public static AnimCommand CreatAC(JToken jt)
        {
            ANIM_COMMAND ac = (ANIM_COMMAND)(int)jt["animCommandType"];
            switch (ac)
            {
                case ANIM_COMMAND.OP_DoMove:
                    return new OpDoMove().SetParam(jt);
                case ANIM_COMMAND.OP_DoAlpha:
                    return new OpDoAlpha().SetParam(jt);
                case ANIM_COMMAND.OP_DoRotate:
                    return new OpDoRotate().SetParam(jt);
                case ANIM_COMMAND.OP_SetSpeed:
                    return new OpSetSpeed().SetParam(jt);
                default:
                    return new AnimCommand();
            }
        }
    }
    [System.Serializable]
    public class AnimCommand
    {
        public ANIM_COMMAND animCommandType;
        public float beginTime;
        public float endTime;
        public virtual Tween GetTween(Transform t)
        {
            return null;
        }

        public virtual Tween GetTween(SpriteRenderer sr)
        {
            return null;
        }

        public virtual AnimCommand SetParam(JToken jt)
        {
            this.animCommandType = (ANIM_COMMAND)(int)jt["animCommandType"];
            this.beginTime = (float)jt["beginTime"];
            this.endTime = (float)jt["endTime"];

            return this;
        }

        public virtual Vector3 GetPos()
        {
            return Vector3.zero;
        }

        public AnimCommand()
        {

        }
    }
    [System.Serializable]
    public class OperatorAC : AnimCommand
    {
        public Ease motionType;

        public override AnimCommand SetParam(JToken jt)
        {
            this.motionType = (Ease)(int)jt["motionType"];
            return base.SetParam(jt);
        }

        public OperatorAC()
        {

        }
    }
    [System.Serializable]
    //移动
    public class OpDoMove : OperatorAC
    {
        // public Vector3 endPos1;
        public List<float> endPos = new List<float>();
        public override Tween GetTween(Transform t)
        {

            // return t.DOLocalMove(this.endPos1, base.endTime - base.beginTime).SetEase(base.motionType);
            return t.DOLocalMove(new Vector3(endPos[0], endPos[1], endPos[2]), base.endTime - base.beginTime).SetEase(base.motionType);
        }

        public override AnimCommand SetParam(JToken jt)
        {
            // this.endPos1 = new Vector3((float)jt["endPos"][0], (float)jt["endPos"][1], (float)jt["endPos"][2]) / ArcNum.pixelPreUnit;

            this.endPos.Add((float)jt["endPos"][0] / ArcNum.pixelPreUnit);
            this.endPos.Add((float)jt["endPos"][1] / ArcNum.pixelPreUnit);
            this.endPos.Add((float)jt["endPos"][2] / ArcNum.pixelPreUnit);
            return base.SetParam(jt);
        }

        public OpDoMove()
        {

        }

    }
    //改变旋转角度
    [System.Serializable]
    public class OpDoRotate : OperatorAC
    {
        // public Vector3 endRotate1;
        public List<float> endRotate;
        public override Tween GetTween(Transform t)
        {
            // return t.DOLocalRotate(this.endRotate1, base.endTime - endTime).SetEase(base.motionType);
            return t.DOLocalRotate(new Vector3(endRotate[0], endRotate[1], endRotate[2]), base.endTime - endTime).SetEase(base.motionType);
        }

        public OpDoRotate()
        {

        }

        public override AnimCommand SetParam(JToken jt)
        {
            this.endRotate = new List<float>();
            // this.endRotate1 = new Vector3((float)jt["endRotate"][0], (float)jt["endRotate"][1], (float)jt["endRotate"][2]);

            this.endRotate.Add((float)jt["endRotate"][0]);
            this.endRotate.Add((float)jt["endRotate"][1]);
            this.endRotate.Add((float)jt["endRotate"][2]);
            return base.SetParam(jt);
        }
    }
    [System.Serializable]
    //改变透明度
    public class OpDoAlpha : OperatorAC
    {
        public float endalpha;

        public override Tween GetTween(SpriteRenderer sr)
        {
            return sr.DOFade(this.endalpha, base.endTime - base.beginTime).SetEase(base.motionType);
        }

        public OpDoAlpha()
        {

        }
        public override AnimCommand SetParam(JToken jt)
        {
            this.endalpha = (float)jt["endAlpha"] / 255;
            return base.SetParam(jt);
        }
    }

    //设置速度
    [System.Serializable]
    public class OpSetSpeed : OperatorAC
    {
        public float newSpeed;

        public override AnimCommand SetParam(JToken jt)
        {
            this.newSpeed = (float)jt["newSpeed"];
            return base.SetParam(jt);
        }

        public OpSetSpeed()
        {

        }

        public OpSetSpeed(float beginTime, float s)
        {
            this.newSpeed = s;
            this.beginTime = beginTime;
        }
    }

    public class OpSetPos : OperatorAC
    {
        public List<float> newPos = new List<float>();

        public OpSetPos()
        {

        }

        public override AnimCommand SetParam(JToken jt)
        {
            this.newPos.Add((float)jt["newPos"][0] / ArcNum.pixelPreUnit);
            this.newPos.Add((float)jt["newPos"][1] / ArcNum.pixelPreUnit);
            this.newPos.Add((float)jt["newPos"][2] / ArcNum.pixelPreUnit);
            return base.SetParam(jt);
        }
        public override Vector3 GetPos()
        {
            return new Vector3(newPos[0], newPos[1], newPos[2]);
        }
    }

    public class OpSetDirect : OperatorAC
    {
        public DIRECTION d1;//第一方向
        public DIRECTION d2;//第二方向
        public DIRECTION d3;//攻击范围方向
        public OpSetDirect()
        {

        }
        public override AnimCommand SetParam(JToken jt)
        {
            this.d1 = (DIRECTION)(int)jt["d1"];
            this.d2 = (DIRECTION)(int)jt["d2"];
            return base.SetParam(jt);
        }
    }

    [System.Serializable]
    public class EnemyAC : AnimCommand
    {

    }
}