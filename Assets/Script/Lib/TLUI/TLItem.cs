using System.Net.Mime;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using System.Reflection;
using TMPro;

namespace TLUI
{
    public class TLItem<T> : TLBaseUI
    {
        public T data;
        public Dictionary<string, string> textDict = new Dictionary<string, string>();
        private Dictionary<string, TextMeshProUGUI> textMPDict = new Dictionary<string, TextMeshProUGUI>(); // 字典用于存储子物体名和对应的TextMeshProUGUI
        FieldInfo[] fields = null;//T的字段

        public override void Awake()
        {
            base.Awake();
            // 遍历子物体
            foreach (Transform child in gameObject.transform)
            {
                // 获取子物体的名字和TextMeshProUGUI组件
                string childName = child.gameObject.name;
                TextMeshProUGUI textMesh = child.GetComponent<TextMeshProUGUI>();

                // 将子物体名和TextMeshProUGUI组件添加到字典中
                if (textMesh != null)
                {
                    textMPDict.Add(childName, textMesh);
                }
            }
        }

        

        public virtual void UpdateData(T newData)
        {

            // 获取类的所有字段
            if (fields == null)
                fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                // 如果字段类型是string类型
                if (field.FieldType == typeof(string))
                {
                    // 获取字段名和值
                    textDict[field.Name] = (string)field.GetValue(newData);
                }
            }
            //添加在textMeshPro上
            foreach (string k in textDict.Keys)
            {
                textMPDict[k].text = textDict[k];
            }
        }

        public virtual void SetDataInfo(T newData)
        {
            data = newData;
            UpdateData(newData);
        }
    }
}

