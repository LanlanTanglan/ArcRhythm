using DG.Tweening;
using TMPro;
using UnityEngine;
using TLUI;

public class TL_TextRainbowAnim : TLBaseAnim
{
    public TextMeshProUGUI _textMeshPro;
    public Color _color = Color.green;
    public override void Awake()
    {
        base.Awake();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    public override void SetSustainAnim()
    {
        base.SetSustainAnim();
        // 循环，让文本的颜色在红色和?色之间来回变化
        _sustainAnim.Append(_textMeshPro.DOColor(_color, _yoyoDuration).SetEase(Ease.Linear))
                    .SetLoops(-1, LoopType.Yoyo);
    }
}