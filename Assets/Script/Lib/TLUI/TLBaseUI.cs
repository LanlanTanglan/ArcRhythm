using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLTemplate;
using DG.Tweening;
namespace TLUI
{
    public class TLBaseUI : MonoBehaviour, ITLAnim
    {
        public Sequence _enterAnim;
        public Sequence _leaveAnim;

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void PlayEnterAnim()
        {
            _enterAnim.OnComplete(() =>
            {
                EventManager.Instance.TriggerEvent("unlockCanvas", new EventParam());
            });
        }
        public virtual void PlayLeaveAnim()
        {
            _leaveAnim.OnComplete(() =>
            {
                EventManager.Instance.TriggerEvent("unlockCanvas", new EventParam());
            });
        }
    }

}
