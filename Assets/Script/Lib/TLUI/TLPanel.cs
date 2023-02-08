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
        public CanvasGroup _cGroup;
        

        public override void Awake()
        {
            _cGroup = GetComponent<CanvasGroup>();
        }
        public override void Start()
        {

        }
        public override void Update()
        {

        }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void Show()
        {
            TLUIManager.Instance.PlayAnimAndLock(gameObject, true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual void Hide()
        {
            TLUIManager.Instance.PlayAnimAndLock(gameObject, false);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public override void PlayEnterAnim()
        {
            _cGroup.alpha = 1;
            transform.localPosition = new Vector3(-3000, 0, 0);
            _enterAnim = DOTween.Sequence();
            _enterAnim.Append(transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutCubic));
            base.PlayEnterAnim();
        }

        public override void PlayLeaveAnim()
        {
            _leaveAnim = DOTween.Sequence();
            _leaveAnim.Append(transform.DOLocalMove(new Vector3(3000, 0, 0), 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _cGroup.alpha = 0;
            }));
            base.PlayLeaveAnim();
        }
    }

}