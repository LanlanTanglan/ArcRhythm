using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArcRhythm
{
    //输入信息结构体基类
    public class BaseInput
    {
        public int ttl;//存活时间
        public float createTime;//创建的时间
        public InputType inputType;//输入类型
        public bool isLocked = false;
        public BaseInput()
        {

        }

        public BaseInput SetTTL(int c)
        {
            this.ttl = c;
            return this;
        }
        public virtual BaseInput SetCreateTime(float c)
        {
            this.createTime = c;
            return this;
        }
        public virtual BaseInput SetInpuKey(KeyCode f, KeyCode s)
        {
            return this;
        }

        public virtual BaseInput SetInputType(InputType i)
        {
            this.inputType = i;
            return this;
        }

        public bool IsRightInputType(InputType i)
        {
            return this.inputType == i;
        }

        public virtual bool IsRightKeyType(KeyCode c)
        {
            return false;
        }

        public void Lock()
        {
            this.isLocked = true;
        }
    }

    public class KeyboardInput : BaseInput
    {
        public KeyCode firstKey;//第一个按下的键
        public KeyCode secondKey;//第二个按下的键

        public KeyboardInput()
        {

        }

        public override BaseInput SetInpuKey(KeyCode f, KeyCode s)
        {
            this.firstKey = f;
            this.secondKey = s;
            return this;
        }

        public override bool IsRightKeyType(KeyCode c)
        {
            return this.firstKey == c;
        }

    }


    //移动端输入结构
    public class AndroidTouchInput : BaseInput
    {

    }

    public class IOSTouchInput : BaseInput
    {

    }


}