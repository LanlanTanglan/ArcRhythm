using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using TMPro;

/// <summary>
/// 章节显示脚本
/// </summary>
public class ChapterInfoWarp : MonoBehaviour
{
    public Dictionary<string, Transform> chds;//子物体
    void Awake()
    {
        chds = new Dictionary<string, Transform>();

        chds.Add("chp_cover_gs", this.transform.Find("background/chp_cover_gs"));
        chds.Add("chp_cover", this.transform.Find("chp_cover"));
        chds.Add("chp_title", this.transform.Find("chp_title"));
        chds.Add("all", this.transform.Find("score_warp/all"));
        chds.Add("clear", this.transform.Find("score_warp/clear"));
        chds.Add("full_combo", this.transform.Find("score_warp/full_combo"));
        chds.Add("ark", this.transform.Find("score_warp/ark"));
    }
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 设置封面
    /// </summary>
    public void set_chp_cover(string p)
    {
        if (chds.ContainsKey("chp_cover"))
        {
            //获取物体的Transform
            Transform sccgt = chds["chp_cover"];
            //载入高斯图片
            AssetBundle backdrop = Singleton<ABManager>.Instance.GetAssetBundle("backdrop");
            Sprite s = backdrop.LoadAsset<Sprite>(p);

            //设置这个图片的相关信息
            SpriteRenderer sr = sccgt.GetComponent<SpriteRenderer>();
            sr.sprite = s;
        }
    }
    /// <summary>
    /// 设置高斯模糊背景
    /// </summary>
    /// <param name="p"></param>
    public void set_chp_cover_gs(string p)
    {
        Debug.Log("正在加载高斯模糊背景");

        if (chds.ContainsKey("chp_cover_gs"))
        {
            //获取物体的Transform
            Transform sccgt = chds["chp_cover_gs"];
            //载入高斯图片
            AssetBundle backdrop = Singleton<ABManager>.Instance.GetAssetBundle("backdrop");
            Sprite s = backdrop.LoadAsset<Sprite>(p);

            //设置这个图片的相关信息
            SpriteRenderer sr = sccgt.GetComponent<SpriteRenderer>();
            sr.sprite = s;
            //调整大小 TODO 这是常量
            float w = 5 / sr.bounds.size.x;
            float h = 4.1f / sr.bounds.size.y;
            sccgt.localScale = new Vector3(sccgt.localScale.x * w, sccgt.localScale.y * h, sccgt.localScale.z);

            //调整位置
            sccgt.localPosition = new Vector3(0, 45, 1);
        }
    }

    /// <summary>
    /// 设置章节标题
    /// </summary>
    public void set_chp_title(string t)
    {
        if (chds.ContainsKey("chp_title"))
        {
            Transform ctt = chds["chp_title"];
            TMP_Text tm = ctt.GetComponent<TMP_Text>();
            tm.text = "<color=\"white\">" + t;
        }
    }

    /// <summary>
    /// 设置分数相关
    /// </summary>
    public void set_score(int a, int c, int fc, int arc)
    {
        if (chds.ContainsKey("all"))
        {
            chds["all"].GetComponent<TMP_Text>().text = "All\n" + a;
        }
        if (chds.ContainsKey("clear"))
        {
            chds["clear"].GetComponent<TMP_Text>().text = "Clear\n" + c;
        }
        if (chds.ContainsKey("full_combo"))
        {
            chds["full_combo"].GetComponent<TMP_Text>().text = "<color=#0078DE>Full Combo</color>\n" + fc;
        }
        if (chds.ContainsKey("ark"))
        {
            chds["ark"].GetComponent<TMP_Text>().text = "<color=yellow>Ark</color>\n" + arc;
        }
    }
}
