using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using Util;
using UnityEngine.UI;


/// <summary>
/// MusicListScroll的控制器
/// </summary>
public class MusicListScrollCotro : MonoBehaviour
{
    public RectTransform contentRT;
    public RectTransform warpRect;//content下Warp的RT
    public float contentH;
    public float padH;//中央item与屏幕的高度距离
    public float itemH;//item的高度
    public int itemNum = 1;//item的个数
    public float warpH;//Warp的高度
    public ScrollRect s;
    public float moveDistants;//移动的距离
    public int cIdx;
    public int lastIdx;
    void Awake()
    {

        //TODO 获取屏幕宽度与高度
        s = this.GetComponent<ScrollRect>();
        contentRT = s.content.GetComponent<RectTransform>();
        warpRect = s.content.GetChild(0).GetComponent<RectTransform>();


        //设置参数
        itemH = 140;
        padH = (1080 - itemH) / 2;

        SetItemNum(2);

    }
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 修改Rect的相关位置信息
    /// </summary>
    public void UpdeteRectPos()
    {
        //修改ContentRT
        // Debug.Log(contentRT.offsetMin);
        // Debug.Log(contentRT.offsetMax);
        // Debug.Log(contentRT.sizeDelta);
        //left, bottom
        contentRT.offsetMin = new Vector2(-480, -1080 - (itemH + 60) * (itemNum - 1));
        //right, top
        contentRT.offsetMax = new Vector2(480, 0);

        //修改WarpRT
        //left, bottom
        warpRect.offsetMin = new Vector2(-480, padH);
        //right, top
        warpRect.offsetMax = new Vector2(480, -padH);

    }

    /// <summary>
    /// 设置Item个数
    /// </summary>
    /// <param name="n"></param>
    public void SetItemNum(int n)
    {
        itemNum = n;
        UpdeteRectPos();
    }
}
