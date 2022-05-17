using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArkRhythm
{
    [System.Serializable]
    //输入信息结构体基类
    public class BaseInput
    {
        public int ttl = 0;//存活时间
        public float createTime;//创建的时间
        public InputType inputType;//输入类型
        public bool isLocked = false;
        public bool isDead = false;//是否死亡
        public BaseInput()
        {

        }

        /// <summary>
        /// 设置存活时间
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public BaseInput SetTTL(int c)
        {
            this.ttl = c;
            return this;
        }

        /// <summary>
        /// 设置创建时间
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual BaseInput SetCreateTime(float c)
        {
            this.createTime = c;
            return this;
        }

        /// <summary>
        /// 设置输入Key
        /// </summary>
        /// <param name="f"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual BaseInput SetInpuKey(KeyCode f, KeyCode s)
        {
            return this;
        }

        /// <summary>
        /// 设置输入input类型
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public virtual BaseInput SetInputType(InputType i)
        {
            this.inputType = i;
            return this;
        }

        /// <summary>
        /// 是否为正确的输入类型
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool IsRightInputType(InputType i)
        {
            return this.inputType == i;
        }

        /// <summary>
        /// 是否为正确的Key类型
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual bool IsRightKeyType(KeyCode c)
        {
            return false;
        }

        /// <summary>
        /// 锁住
        /// </summary>
        public void Lock()
        {
            this.isLocked = true;
        }

        /// <summary>
        /// 延长存活时间
        /// </summary>
        public void ProlongTTL()
        {
            this.ttl += 1;
        }

        /// <summary>
        /// 时间流逝
        /// </summary>
        /// <returns></returns>
        public int TTLUpdate()
        {
            return --this.ttl;
        }
    }

    [System.Serializable]
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
    [System.Serializable]
    public class AndroidTouchInput : BaseInput
    {

    }

    [System.Serializable]
    public class IOSTouchInput : BaseInput
    {

    }


}