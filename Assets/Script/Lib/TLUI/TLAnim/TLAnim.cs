using System.Net.Mime;
using UnityEngine;
using DG.Tweening;
using TMPro;


namespace TLUI
{
    public static class TLSustainAnim
    {
        public static Tweener DoLoopFade(TextMeshProUGUI obj)
        {
            return obj.DOFade(0, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public static class TLReactAnim
    {
        
    }
}
