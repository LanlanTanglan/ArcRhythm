using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TLTemplate;

namespace TLUI
{
    public class TLBaseAnim : MonoBehaviour, ITLAnim
    {
        //是否遵循上层的UI动画命令
        public bool isFollowParentAnim = false;
        public bool isPlayEnterAnim = false;
        public bool isPlayLeaveAnim = false;
        public bool isPlaySustainAnim = false;
        public float _enterDuration = 0.5f;
        public Ease _enterEase = Ease.Unset;
        public float _leaveDuration = 0.5f;
        public float _yoyoDuration = 0.5f;
        public Ease _leaveEase = Ease.Unset;
        public Sequence _enterAnim = null;
        public Sequence _sustainAnim = null;
        public Sequence _leaveAnim = null;
        public TLBaseUI _baseUi = null;

        public virtual void Awake()
        {
            _baseUi = GetComponent<TLBaseUI>();
        }

        public virtual void Start()
        {
            SetSustainAnim();
            PlaySustainAnim();
        }

        public virtual void Update()
        {

        }

        public virtual void PlayEnterAnim()
        {
            //若序列没有被初始化，那么就新建一个
            if (_enterAnim == null) _enterAnim = DOTween.Sequence();
            _enterAnim.OnComplete(() =>
            {
                EventManager.Instance.TriggerEvent("unlockCanvas", new EventParam());
                _enterAnim = null;
            });
        }

        public virtual void PlayLeaveAnim()
        {
            if (_leaveAnim == null) _leaveAnim = DOTween.Sequence();
            _leaveAnim.OnComplete(() =>
            {
                EventManager.Instance.TriggerEvent("unlockCanvas", new EventParam());
                _leaveAnim = null;
            });
        }

        public virtual void PlaySustainAnim()
        {
            _sustainAnim.Play();
        }

        public virtual void SetSustainAnim()
        {
            if (_sustainAnim == null) _sustainAnim = DOTween.Sequence();
        }
    }
}

