using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLTemplate;

[RequireComponent(typeof(CanvasGroup))]
public class TLCanvas : MonoBehaviour
{
    //是否在动画当中
    public bool _isLock = false;
    public CanvasGroup _canvasGroup;
    private Action<EventParam> _DOlock;
    private Action<EventParam> _DoUnlock;

    void Awake()
    {
        _DOlock = new Action<EventParam>(LockCanvasEvent);
        _DoUnlock = new Action<EventParam>(UnLockCanvasEvent);
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        EventManager.Instance.StartListening("lockCanvas", LockCanvasEvent);
        EventManager.Instance.StartListening("unlockCanvas", UnLockCanvasEvent);
    }

    void Start()
    {

    }

    void Update()
    {
        if (_isLock)
        {
            _canvasGroup.blocksRaycasts = false;
        }
        else
        {
            _canvasGroup.blocksRaycasts = true;
        }
    }

    void OnDestroy()
    {
        EventManager.Instance.StopListening("lockCanvas", _DOlock);
        EventManager.Instance.StopListening("unlockCanvas", _DoUnlock);
    }

    public void LockCanvasEvent(EventParam p)
    {
        _isLock = true;
    }
    public void UnLockCanvasEvent(EventParam p)
    {
        _isLock = false;
    }
}
