using UnityEngine;
using TLUI;
using DG.Tweening;
using TMPro;

public class TL_TextTwinkleAnim : TLBaseAnim
{
    public TextMeshProUGUI _textMeshPro;
    public Sequence _sustainSequence;
    public override void Awake()
    {
        base.Awake();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public override void SetSustainAnim()
    {
        base.SetSustainAnim();
        //设置持续动画
        _sustainAnim.Append(TLSustainAnim.DoLoopFade(_textMeshPro));
        _sustainAnim.SetLoops(-1, LoopType.Yoyo);
        _sustainAnim.Pause();
    }
}