using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TLUI
{
    public class TLPanel : MonoBehaviour, ITLAnim
    {
        public CanvasGroup _cGroup;

        public virtual void Awake()
        {
            _cGroup = GetComponent<CanvasGroup>();
            
        }
        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void Show()
        {
            TLUIManager.Instance.PlayAnim(gameObject, true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual void Hide()
        {
            TLUIManager.Instance.PlayAnim(gameObject, false);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public void PlayEnterAnim()
        {
            _cGroup.alpha = 1;
            transform.localPosition = new Vector3(-3000, 0, 0);
            
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutCubic));
        }

        public void PlayLeaveAnim()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOLocalMove(new Vector3(3000, 0, 0), 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _cGroup.alpha = 0;
            }));
        }
    }

}