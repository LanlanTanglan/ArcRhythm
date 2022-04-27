using System.Threading;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ArkRhythm;
using DG.Tweening;

//移动条控制器
public class HoverLevelCotro : MonoBehaviour
{
    public ScoreCotro Countsc;//当前铺面等级数字控制
    public STAFF_LEVEL currentLevel;//当前铺面等级
    public TMP_Text level;//等级
    public SpriteRenderer bg;//背景
    public int[] levelPos;//移动位置
    public List<Vector3> bgColor;//颜色Bg颜色

    void Awake()
    {
        //添加分数控制器脚本
        Countsc = this.transform.Find("Count").gameObject.AddComponent<ScoreCotro>();
        level = this.transform.Find("Lv").GetComponent<TMP_Text>();
        bg = this.transform.Find("Bg").GetComponent<SpriteRenderer>();
        
        //设置分数控制器的相关信息
        bgColor = new List<Vector3>() { new Vector3(0, 173, 52), new Vector3(0, 117, 255), new Vector3(255, 0, 14), new Vector3(250, 211, 0) };
        levelPos = new int[5] { 50, 150, 250, 350 ,450};
        Countsc.gap = 1000;
        Countsc.isAddZero = false;
        Countsc.bis = 1;
        Countsc.targetScore = 1;
        Countsc.currentScore = 1;
        Countsc.frame = 10;
        Countsc.max_gap = 0;

    }
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ChangeLevel(STAFF_LEVEL t, int num)
    {
        currentLevel = t;
        //设置分数
        Countsc.targetScore = num;
        //设置等级
        level.text = Enum.GetName(typeof(STAFF_LEVEL), (int)t);
        //移动levelBar
        transform.DOLocalMoveX(levelPos[(int)t - 1] - 610 / 2, 0.3f);
        //修改颜色
        bg.DOColor(new Color(bgColor[(int)t - 1][0] / 255, bgColor[(int)t - 1][1] / 255, bgColor[(int)t - 1][2] / 255, 1), 0.5f);
    }
    /// <summary>
    /// 将悬浮条移动到正确的地方
    /// </summary>
    public void SetTruePos()
    {
        
    }
}
