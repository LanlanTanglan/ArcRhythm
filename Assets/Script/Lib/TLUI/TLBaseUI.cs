using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLTemplate;
using DG.Tweening;
namespace TLUI
{
    [SerializeField]
    public class TLBaseUI : MonoBehaviour, ITLLifeCycle
    {

        //是否遵循上层的DO
        public bool isFollowParentDoEnter = true;
        public bool isFollowParentDoLeave = true;

        public GameObject parentWarp;
        public RectTransform _rectTransform;
        public BoxCollider2D _boxCollider2D;
        public CanvasGroup _cGroup;


        public virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _cGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void TLUIEnter()
        {

        }

        public virtual void TLUILeave()
        {

        }


    }

}
