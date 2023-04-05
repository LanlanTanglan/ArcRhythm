using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//统一管理下级的干员Note列表组
public class OperatorGroupController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool isDragging = false;
    public bool isVerticalDrag = false;
    public bool isHorizonDrag = false;
    public bool isVerticalOut = false;
    public bool isHorizonOut = false;

    public float minX = 740.82f - 899.87f;
    public float minY = 0;
    public float maxX = 0;
    public float maxY = 0;
    public UnityEvent OnWarpDrag;
    Vector3 lastMousePos;

    public void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        lastMousePos = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        isHorizonDrag = false;
        isVerticalDrag = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        OnWarpDrag.Invoke();
        // Debug.Log("当前位置" + this.transform.localPosition);
        if (isDragging)
        {
            if (!isHorizonDrag && !isVerticalDrag)
            {
                Vector2 j = lastMousePos - Input.mousePosition;
                if (Mathf.Abs(j.x) > Mathf.Abs(j.y))
                    isHorizonDrag = true;
                else
                    isVerticalDrag = true;
            }

            Vector2 deltaPosition = Input.mousePosition - lastMousePos;
            if (isHorizonDrag)
            {
                // Only move horizontally
                if (transform.localPosition.x + deltaPosition.x >= minX && transform.localPosition.x + deltaPosition.x <= maxX)
                    transform.localPosition += new Vector3(deltaPosition.x, 0);
                else
                {
                    isHorizonOut = true;
                    if (Math.Abs(deltaPosition.x) != 0)
                        // transform.localPosition += new Vector3(deltaPosition.x, 0) / Math.Abs(deltaPosition.x) * 5;
                        transform.localPosition += new Vector3(deltaPosition.x, 0);
                }
            }
            else
            {
                // Only move vertically
                if (transform.localPosition.y + deltaPosition.y >= minY && transform.localPosition.y + deltaPosition.y <= maxY)
                    transform.localPosition += new Vector3(0, deltaPosition.y);
                else
                {
                    isVerticalOut = true;
                    if (Math.Abs(deltaPosition.y) != 0)
                        // transform.localPosition += new Vector3(0, deltaPosition.y) / Math.Abs(deltaPosition.y) * 5;
                        transform.localPosition += new Vector3(0, deltaPosition.y);
                }
            }
            lastMousePos = Input.mousePosition;
            if (isHorizonOut)
            {
                if (transform.localPosition.x < minX)
                {
                    transform.localPosition = new Vector3(minX, transform.localPosition.y);
                }
                if (transform.localPosition.x > maxX)
                {
                    transform.localPosition = new Vector3(maxX, transform.localPosition.y);
                }
                isHorizonOut = false;
            }
            if (isVerticalOut)
            {
                if (transform.localPosition.y < minY)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, minY);
                }
                if (transform.localPosition.y > maxY)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, maxY);
                }
                isVerticalOut = false;
            }
        }
    }
}
