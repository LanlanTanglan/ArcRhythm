using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// 用于控制分数
/// </summary>
[System.Serializable]
public class ScoreCotro : MonoBehaviour
{
    public int targetScore;//目标分数
    public int currentScore;//当前分数
    public int counter;//计数器
    public int frame;
    public TMP_Text scoretmp;
    public int gap;//间隔
    public int bis;
    public int targetLen;//目标长度
    public bool isAddZero;//是否填0
    public int max_gap;
    void Awake()
    {
        scoretmp = this.GetComponent<TMP_Text>();

        //初始化数据
        targetScore = 0;
        currentScore = 0;
        counter = 0;
        gap = 17;
        bis = 377;
        max_gap = 1000;
        targetLen = 7;
        frame = 5;
    }
    void Start()
    {

    }

    void Update()
    {
        //如果目标分数与当前分数不同，则进行修改变换
        //每隔5帧进行调用
        if (currentScore != targetScore)
        {
            counter++;
            if (counter >= frame)
            {
                currentScore = ScoreChanger();
                //设置数字
                scoretmp.text = ScoreTextMaker(currentScore);
                counter = 0;
            }
        }
    }

    public void SetScore(int s)
    {
        targetScore = s;
    }

    /// <summary>
    /// 数字文本转换器，七位位不足高位补0
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public string ScoreTextMaker(int n)
    {
        string str = n.ToString();
        string s = "";
        int len = targetLen - str.Length;
        if (isAddZero)
        {
            for (int i = 0; i < len; i++)
            {
                s += "0";
            }
        }

        return s + str;
    }

    /// <summary>
    /// /数字变化器
    /// </summary>
    /// <returns></returns>
    public int ScoreChanger()
    {
        //如果在最小差距直接向当前目标分数变换
        if (Math.Abs(currentScore - targetScore) <= max_gap)
        {
            return targetScore;
        }
        //靠近这个数字
        else
        {
            int t = targetScore - currentScore;
            return currentScore + t / gap + (t > 0 ? bis : -bis);
        }
    }

}
