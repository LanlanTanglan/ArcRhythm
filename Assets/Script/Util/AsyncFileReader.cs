using System.Threading;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Converters;
using ArkTemplate;
using ArkRhythm;

namespace TLUtil
{
    public class AsyncFileReader
    {
        public JObject data;
        public BMS bms;
        public AsyncFileReader(string filename)
        {
            new Thread(() =>
            {
                data = JsonUtil.readJSON(filename);
                bms = new BMS().SetParam(data);
                Debug.Log("正在异步读取内容");
                Singleton<EventManager>.Instance.TriggerEventAnsyc("test");
                Debug.Log("异步读取内容结束");
            }).Start();
        }
    }
}