using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
using TLUtil;
using DG.Tweening;

public class BaseJudgePerfor : MonoBehaviour
{
    // Start is called before the first frame update
    public float sustain = 0.4f;
    public virtual void Start()
    {
        Sequence s = DOTween.Sequence();
        s.Append(this.transform.DOLocalMove(this.transform.localPosition + new Vector3(0, 2.8f, 0), sustain).SetEase(Ease.OutQuart));
        s.Join(this.transform.DOScale(new Vector3(2, 2, 2), sustain).SetEase(Ease.OutElastic));
        s.Join(this.transform.DOLocalRotate(new Vector3(0, 0, Random.Range(1, 3) == 1 ? 18 : -18), sustain).SetEase(Ease.OutElastic));
        s.OnComplete(() =>
                {
                    Destroy(this.gameObject);
                });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Init(ATTACK_RANGE_TYPE art)
    {
        //回到原点
        this.transform.localPosition = new Vector3(0, 0, -3);
        //解除与父物体的绑定关系
        this.transform.SetParent(null);
        //无角度
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
