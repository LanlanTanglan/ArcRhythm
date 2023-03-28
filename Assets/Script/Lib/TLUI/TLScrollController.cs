using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


namespace TLUI
{
    [SerializeField]
    public class TLScrollController<T>: MonoBehaviour
    {
        public GameObject HiddenItem;//隐藏的Item，便于之后的复制
        public GameObject Content;


        List<TLItem<T>> Items = new List<TLItem<T>>();

        public virtual void Awake()
        {

        }

        //将Item添加在Content下
        public virtual void AddItem(T d)
        {
            //复制一个Item
            GameObject newItem = Instantiate(HiddenItem, Content.transform);
            TLItem<T> item_T = newItem.GetComponent<TLItem<T>>();
            Items.Add(item_T);
            item_T.SetDataInfo(d);
        }
    }
}