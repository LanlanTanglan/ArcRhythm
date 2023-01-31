using System.Globalization;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HPBar : MonoBehaviour
{
    public Image bar;
    public Image delay;
    public Image back;

    public int times = 0;
    public float delayTimes = 0.25f;
    public float accT = 0f;
    public int count = 0;
    public Tween _tween;

    void Awake()
    {

    }
    void Start()
    {
        Image[] images = GetComponentsInChildren<Image>();
        back = images[0];
        delay = images[1];
        bar = images[2];
        //设置下位置
        transform.localPosition = new Vector3(0, -0.6f, 0);
    }

    void Update()
    {

    }
    public void Init(int time)
    {
        times = time;
    }
    public void SubHp()
    {
        count++;
        bar.fillAmount -= 1 / (float)times;
        _tween = DOTween.To(() => delay.fillAmount, (x) => delay.fillAmount = x, 1 - (1 / (float)times) * count, delayTimes);
    }

    public void CompleteTween()
    {
        _tween.Complete();
    }
}
