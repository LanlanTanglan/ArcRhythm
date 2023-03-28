using UnityEngine;
using DG.Tweening;

namespace TLDoTweenAnim
{
    //鼠标悬浮
    public class HoverShakeMouseAnim : MonoBehaviour
    {
        public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);
        public Ease hoverEnterEase = Ease.OutElastic;
        public Ease hoverLeaveEase = Ease.OutCubic;
        public float shakeDuration = 0.5f;
        public float shakeStrength = 10f;
        public int vibrato = 90;
        public float shakeRandom = 90f;

        private void OnMouseEnter()
        {
            transform.DOScale(hoverScale, 0.5f).SetEase(hoverEnterEase);
            transform.DOShakePosition(shakeDuration, shakeStrength, vibrato, shakeRandom, false, true);
        }

        private void OnMouseExit()
        {
            transform.DOScale(Vector3.one, 0.5f).SetEase(hoverLeaveEase);
        }
    }
}

