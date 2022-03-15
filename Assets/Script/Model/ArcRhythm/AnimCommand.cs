using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArcRhythm
{
    public class AnimCommandFactory
    {
        public static AnimCommand CreatAC(JToken jt)
        {
            ANIM_COMMAND ac = (ANIM_COMMAND)(int)jt["animCommandType"];
            switch (ac)
            {
                case ANIM_COMMAND.OP_CM:
                    return new OpCM().SetParam(jt);
                default:
                    return new AnimCommand();
            }
        }
    }

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
        public AnimCommand()
        {

        }
    }

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

    //移动
    public class OpCM : OperatorAC
    {
        public Vector3 endPos;
        public override Tween GetTween(Transform t)
        {

            return t.DOLocalMove(this.endPos, base.endTime - base.beginTime);
        }

        public override AnimCommand SetParam(JToken jt)
        {
            this.endPos = new Vector3((float)jt["endPos"][0], (float)jt["endPos"][1], (float)jt["endPos"][2]);

            return base.SetParam(jt);
        }

        public OpCM()
        {

        }

    }
    //改变旋转角度
    public class OpCR : OperatorAC
    {
        public Vector3 endRotate;
        public override Tween GetTween(Transform t)
        {
            return t.DOLocalRotate(this.endRotate, base.endTime - endTime);
        }

        public OpCR()
        {

        }

        public override AnimCommand SetParam(JToken jt)
        {
            this.endRotate = new Vector3((float)jt["endRotate"][0], (float)jt["endRotate"][1], (float)jt["endRotate"][2]);
            return base.SetParam(jt);
        }
    }

    //改变透明度
    public class OpCA : OperatorAC
    {
        public float endalpha;

        public override Tween GetTween(SpriteRenderer sr)
        {
            return sr.DOFade(this.endalpha, base.endTime - base.beginTime);
        }

        public OpCA()
        {

        }
        public override AnimCommand SetParam(JToken jt)
        {
            this.endalpha = (float)jt["endalpha"] / 255;
            return base.SetParam(jt);
        }
    }

    //设置速度
    public class OpSV : OperatorAC
    {
        public float newSpeed;

        public override AnimCommand SetParam(JToken jt)
        {
            this.newSpeed = (float)jt["newSpeed"];
            return base.SetParam(jt);
        }

        public OpSV()
        {
            
        }
    }


    public class EnemyAC : AnimCommand
    {

    }
}