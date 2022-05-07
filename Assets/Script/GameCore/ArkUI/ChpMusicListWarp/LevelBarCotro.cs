using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 铺面等级条控制器
/// </summary>
public class LevelBarCotro : MonoBehaviour
{
    public Transform whiteBgT;//背景T
    public SpriteRenderer whiteBgSR;//背景的SR
    public CanvasGroup[] levelWarpsCG;//等级显示的所有CG
    public ScoreCotro[] levelWarpsSC;
    public List<Transform> levelWarpsT;//levelWarp游戏物体
    public int currentLevelNum;//当前等级数量

    public bool mutex;
    public int m;

    public HoverLevelCotro hoverLevelCotro;
    void Awake()
    {
        //获取
        whiteBgT = transform.GetChild(0);
        whiteBgSR = this.transform.Find("WhiteBg").GetComponent<SpriteRenderer>();
        hoverLevelCotro = this.GetComponentInChildren<HoverLevelCotro>();
        levelWarpsCG = this.transform.GetComponentsInChildren<CanvasGroup>();
        levelWarpsSC = this.transform.GetComponentsInChildren<ScoreCotro>();

        levelWarpsT.Add(transform.GetChild(1));
        levelWarpsT.Add(transform.GetChild(2));
        levelWarpsT.Add(transform.GetChild(3));
        levelWarpsT.Add(transform.GetChild(4));
        levelWarpsT.Add(transform.GetChild(5));

        //初始化样式
        whiteBgT.transform.localPosition = new Vector3(-55, 0, 0);
        whiteBgT.localScale = new Vector3(25, 8, 1);


        //Test
        currentLevelNum = 5;//当前等级数量
        // mutex = true;
        // m = 1;
        // SetLevelCount(new List<int> { 5, 10, 14, 16 });

    }
    void Start()
    {

    }

    void Update()
    {
        if (mutex)
        {
            ChangeLevelWarpNum(m);
            mutex = false;
        }
    }


    /// <summary>
    /// 更改当前的显示难度的LevelWarp的数量
    /// </summary>
    /// <param name="num">显示的数量</param>
    public void ChangeLevelWarpNum(int num)
    {
        if (num == currentLevelNum)
            return;
        //清除
        if (num < currentLevelNum)
        {
            ChangeBgSize(num - currentLevelNum);
            for (int i = num; i < currentLevelNum; i++)
            {
                levelWarpsCG[i].alpha = 0;
            }
        }
        //增加
        else
        {
            ChangeBgSize(num - currentLevelNum);
            for (int i = 0; i < num; i++)
            {
                levelWarpsCG[i].alpha = 1;
            }
        }
        currentLevelNum = num;
    }

    /// <summary>
    ///修改背景条大小
    /// </summary>
    /// <param name="n"></param>
    private void ChangeBgSize(int n)
    {
        whiteBgT.transform.DOLocalMoveX(whiteBgT.transform.localPosition.x + 50 * n, 0.3f);
        whiteBgT.DOScaleX(whiteBgT.transform.localScale.x + n * 5, 0.3f);
    }

    public void SetLevelCount(List<int> level)
    {
        for (int i = 0; i < level.Count; i++)
        {
            levelWarpsSC[i].SetScore(level[i]);
        }
        ChangeLevelWarpNum(level.Count);
    }

}
