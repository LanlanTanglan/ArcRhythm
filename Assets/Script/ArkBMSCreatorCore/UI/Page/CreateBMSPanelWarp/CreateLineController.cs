using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生成线条管理器
[SerializeField]
public class CreateLineController : MonoBehaviour
{
    public float BPM = 120;//每秒节拍树
    public int spaceCount = 2;//间隔数
    public int bpmLineCount = 0;
    public List<LineRenderer> bpmLines;
    public List<LineRenderer> spaceLines;

    //设定速度间隔
    public float speed = 100;//表示：100px/s
    public float musicTime = 300;//音乐时长

    public float warpLen;//从下至上长度
    public float drawYPos = 0;//起始绘制线条相对父物体的Y的位置
    public float currentDrawYPos = 0;
    public float zoom = 1;//缩放大小为1到10
    public float currentZoom = 1;
    public Shader lineColorShader;

    public float bpmLineWidth = 0.09f;
    public float spaceLineWidth = 0.05f;
    public float bpmLineLen = 80;//px
    public float spaceLineLen = 50;//px
    public Material material = null;

    public Color bpmLineColor = Color.black;
    public Color spaceLineColor = Color.green;

    public int spaceCountListIdx = 0;
    public int bpmLineCountListIdx = 0;

    void Awake()
    {
        lineColorShader = Shader.Find("Sprites/Default");
        UpdateLinesInfo();
        UpdateBPM(120);
        UpdateSpaceCount(2);
    }

    void Update()
    {
        if (drawYPos != currentDrawYPos)
        {
            drawYPos = currentDrawYPos;
            UpdateZoom(currentZoom);
        }
    }

    //更新线的信息
    public void UpdateLinesInfo()
    {
        warpLen = speed * musicTime;
        //计算需要绘制多少个bpmLines
        bpmLineCount = (int)Math.Ceiling(musicTime / (60 / BPM));
    }


    // 当间隔数变化时
    public void UpdateSpaceCount(int newSpaceCount)
    {
        spaceCount = newSpaceCount;
        // 根据spaceLine从左至右修改位置
        // 如果不够则生成新的Line
        // 如果够则不需要生成，剩下的全部删除
        if (newSpaceCount == 0)
        {
            return;
        }

        int currentTotalSpaceCount = spaceCountListIdx;
        int idx = 0;
        for (int i = 0; i < bpmLineCount; i++)
        {
            for (int k = 1; k < spaceCount; k++)
            {
                if (idx < currentTotalSpaceCount)
                {
                    spaceLines[idx].SetPosition(0, new Vector3(-spaceLineLen, i * (60 / BPM) * speed * zoom + drawYPos + (60 / BPM) * speed * zoom / spaceCount * k, 0) / 100);
                    spaceLines[idx].SetPosition(1, new Vector3(0, i * (60 / BPM) * speed * zoom + drawYPos + (60 / BPM) * speed * zoom / spaceCount * k, 0) / 100);
                    idx++;
                }
                else
                {
                    GameObject so = new GameObject();
                    so.transform.parent = this.transform;
                    so.transform.localPosition = Vector3.zero;
                    so.name = "spaceLine";

                    //设置线的信息
                    LineRenderer soLr = so.AddComponent<LineRenderer>();
                    soLr.useWorldSpace = false;
                    soLr.material = material == null ? new Material(lineColorShader) : material;
                    soLr.startColor = spaceLineColor;
                    soLr.endColor = spaceLineColor;
                    soLr.startWidth = spaceLineWidth;
                    soLr.endWidth = spaceLineWidth;
                    soLr.sortingLayerName = "scaleLine";
                    soLr.sortingOrder = -100;
                    soLr.SetPosition(0, new Vector3(-spaceLineLen, i * (60 / BPM) * speed * zoom + drawYPos + (60 / BPM) * speed * zoom / spaceCount * k, 0) / 100);
                    soLr.SetPosition(1, new Vector3(0, i * (60 / BPM) * speed * zoom + drawYPos + (60 / BPM) * speed * zoom / spaceCount * k, 0) / 100);
                    if (spaceCountListIdx < spaceLines.Count)
                    {
                        spaceLines[spaceCountListIdx] = soLr;
                    }
                    else
                    {
                        spaceLines.Add(soLr);
                    }
                    spaceCountListIdx++;
                }
            }
        }
        if (idx < currentTotalSpaceCount)
        {
            for (int m = idx; m < currentTotalSpaceCount; m++)
            {
                Destroy(spaceLines[m].gameObject);
                spaceLines[m] = null;//令这个为null并不能修改列表的长度
                spaceCountListIdx--;
            }
        }
    }

    public void UpdateBPM(float newBPM)
    {
        BPM = newBPM;
        int currentTotalBpmLineCount = bpmLineCountListIdx;
        bpmLineCount = (int)Math.Ceiling(musicTime / (60 / BPM));
        int idx = 0;
        for (int i = 0; i < bpmLineCount; i++)
        {
            if (idx < currentTotalBpmLineCount)
            {
                bpmLines[idx].SetPosition(0, new Vector3(-bpmLineLen, i * (60 / BPM) * speed * zoom + drawYPos, 0) / 100);
                bpmLines[idx].SetPosition(1, new Vector3(0, i * (60 / BPM) * speed * zoom + drawYPos, 0) / 100);
                idx++;
            }
            else
            {
                //首先绘制bpmLine
                GameObject bo = new GameObject();
                bo.transform.parent = this.transform;
                bo.transform.localPosition = Vector3.zero;
                bo.name = "bpmLine";
                //设置线的信息
                LineRenderer boLr = bo.AddComponent<LineRenderer>();
                boLr.useWorldSpace = false;
                boLr.material = material == null ? new Material(lineColorShader) : material;
                boLr.startColor = bpmLineColor;
                boLr.endColor = bpmLineColor;
                boLr.startWidth = bpmLineWidth;
                boLr.endWidth = bpmLineWidth;
                boLr.sortingLayerName = "scaleLine";
                boLr.sortingOrder = -100;
                boLr.SetPosition(0, new Vector3(-bpmLineLen, i * (60 / BPM) * speed * zoom + drawYPos, 0) / 100);
                boLr.SetPosition(1, new Vector3(0, i * (60 / BPM) * speed * zoom + drawYPos, 0) / 100);
                bpmLines.Add(boLr);
                //TODO 我不知道为什么这么写
                if (bpmLineCountListIdx < bpmLines.Count)
                {
                    bpmLines[bpmLineCountListIdx] = boLr;
                }
                else
                {
                    bpmLines.Add(boLr);
                }
                bpmLineCountListIdx++;
            }
        }
        if (idx < currentTotalBpmLineCount)
        {
            for (int m = idx; m < currentTotalBpmLineCount; m++)
            {
                Destroy(bpmLines[m].gameObject);
                bpmLines[m] = null;
                bpmLineCountListIdx--;
            }
        }
    }



    //TODO 当缩放变化时
    public void UpdateZoom(float newZoom)
    {
        zoom = newZoom;
        //TODO 约束Zoom的范围
        //在缩放的时候，Line的个数是不变的，只需要改变Line的位置
        //从左至右一次改变Line的位置
        UpdateBPM(BPM);
        UpdateSpaceCount(spaceCount);
    }
}
