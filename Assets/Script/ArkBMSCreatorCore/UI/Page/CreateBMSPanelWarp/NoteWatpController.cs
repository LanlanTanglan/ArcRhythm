using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Note位置全局管理
public class NoteWatpController : MonoBehaviour, IPointerDownHandler
{
    //TODO 统计子Note信息
    public CreateBMSTopController createBMSTopController;
    private GameObject tapNoteItem;
    private Image image;
    public void Awake()
    {
        image = GetComponent<Image>();
        image.raycastTarget = false;
    }
    public void Start()
    {
        tapNoteItem = createBMSTopController.tapNoteItem;
    }

    public void Update()
    {
        //解决遮挡的问题
        if (createBMSTopController.isShortcutKeyEvent)
        {
            image.raycastTarget = true;
        }
        else
        {
            image.raycastTarget = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //如果右键点击
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //放置TapNote
            if (createBMSTopController.isPutTapNote)
            {
                GameObject g = Instantiate(tapNoteItem);
                g.transform.SetParent(this.transform);
                g.transform.localPosition = new Vector3(0, eventData.pressPosition.y + createBMSTopController.currentPlayIdx * createBMSTopController.currentZoom * createBMSTopController.speed - 150, tapNoteItem.transform.localPosition.z);
                g.transform.localScale = tapNoteItem.transform.localScale;
                //设置放置时间刻
                TapNotePlacer tapNotePlacer = g.GetComponent<TapNotePlacer>();
                tapNotePlacer.currentTimeIdx = g.transform.localPosition.y / (createBMSTopController.speed * createBMSTopController.currentZoom);
                tapNotePlacer.createBMSTopController = createBMSTopController;
            }
        }
    }

    public void UpdateTapNotePos(float zoom)
    {
        TapNotePlacer[] tapNotePlacers = transform.GetComponentsInChildren<TapNotePlacer>();
        foreach (TapNotePlacer t in tapNotePlacers)
        {
            t.UpdatePos(zoom);
        }
    }
}
