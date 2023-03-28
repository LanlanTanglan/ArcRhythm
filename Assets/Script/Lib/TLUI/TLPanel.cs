using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TLUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TLPanel : TLBaseUI
    {


        public override void Awake()
        {
            base.Awake();
            _cGroup = GetComponent<CanvasGroup>();
        }
        public override void Start()
        {
            base.Start();
        }
        public override void Update()
        {

        }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void Show()
        {
            TLUIManager.Instance.DOUIEnter(gameObject);
            //调用动画
            TLUIManager.Instance.PlayAnimAndLock(gameObject, true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual void Hide()
        {
            TLUIManager.Instance.DOUILeave(gameObject);

            TLUIManager.Instance.PlayAnimAndLock(gameObject, false);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }

}