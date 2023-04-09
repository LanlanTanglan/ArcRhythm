using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//放置Note,可以上下拖动
public class TapNotePlacer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler
{
    public CreateBMSTopController createBMSTopController;

    public bool isSelected = false;//是否被选中
    Vector3 lastMousePos;
    SpriteRenderer spriteRenderer;
    public float currentTimeIdx = 0;
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        lastMousePos = Input.mousePosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            Color c = spriteRenderer.color;
            c.r = 255f;
            c.g = 0;
            c.b = 0;
            spriteRenderer.color = c;
        }
        else
        {
            Color c = spriteRenderer.color;
            c.r = 255;
            c.g = 255;
            c.b = 255;
            spriteRenderer.color = c;
        }
        isSelected = !isSelected;
    }


    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSelected)
        {
            Vector2 deltaPosition = Input.mousePosition - lastMousePos;
            transform.localPosition += new Vector3(0, deltaPosition.y);
            currentTimeIdx = transform.localPosition.y / (createBMSTopController.speed * createBMSTopController.currentZoom);
            if (transform.localPosition.y < 0)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
                currentTimeIdx = 0;
            }
            lastMousePos = Input.mousePosition;
        }
    }
    //更新Note位置
    public void UpdatePos(float currentZoom)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, currentTimeIdx * currentZoom * createBMSTopController.speed, transform.localPosition.z);
    }
}
