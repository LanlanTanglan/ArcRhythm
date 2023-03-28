using UnityEngine;
using TLUI;
using DG.Tweening;


[RequireComponent(typeof(CanvasGroup))]
public class TL_PanelDefaultAnim : TLBaseAnim
{
    public Vector3 _hidePos = new Vector3(-3000, 0, 0);
    public Vector3 _showPos = new Vector3(0, 0, 0);
    public Vector3 _leavePos = new Vector3(3000, 0, 0);
    public CanvasGroup _cGroup;
    public override void Awake()
    {
        base.Awake();
        _cGroup = GetComponent<CanvasGroup>();
    }
    public override void PlayEnterAnim()
    {
        base.PlayEnterAnim();
        //基本的进入与离开动画，这个是可以修改的
        _cGroup.alpha = 1;
        transform.localPosition = _hidePos;
        _enterAnim.Append(transform.DOLocalMove(_showPos, _enterDuration).SetEase(_enterEase));
    }

    public override void PlayLeaveAnim()
    {
        base.PlayLeaveAnim();
        _leaveAnim.Append(transform.DOLocalMove(_leavePos, _leaveDuration).SetEase(_leaveEase).OnComplete(() =>
        {
            _cGroup.alpha = 0;
        }));
    }
}