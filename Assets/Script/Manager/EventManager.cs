using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ArkRhythm
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventManager : Singleton<EventManager>
    {
        private Dictionary<string, Action> eventDictionary;
        private Dictionary<string, Action<EventParam>> eventDictionaryP;
        public Dictionary<string, bool> eventDictAsync;

        public bool isNeedAsyncLoop = false;
        /// <summary>
        /// 无参数
        /// </summary>
        protected override void OnAwake()
        {
            eventDictionary = new Dictionary<string, Action>();
            eventDictionaryP = new Dictionary<string, Action<EventParam>>();
            eventDictAsync = new Dictionary<string, bool>();
        }

        void Update()
        {
            //轮询异步字典
            if (isNeedAsyncLoop)
            {
                List<string> t = new List<string>();
                foreach (string l in eventDictAsync.Keys)
                {
                    if (eventDictAsync[l])
                    {
                        TriggerEvent(l);
                        t.Add(l);
                    }
                }
                foreach (string l in t)
                {
                    eventDictAsync[l] = false;
                }
                
                isNeedAsyncLoop = false;
            }
        }

        public void StartListening(string eventName, Action listener)
        {
            Action thisEvent;
            if (this.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Add more event to the existing one
                thisEvent += listener;

                //Update the Dictionary
                this.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                //Add event to the Dictionary for the first time
                thisEvent += listener;
                this.eventDictionary.Add(eventName, thisEvent);
            }
        }


        public void StopListening(string eventName, Action listener)
        {
            Action thisEvent;
            if (this.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Remove event from the existing one
                thisEvent -= listener;

                //Update the Dictionary
                this.eventDictionary[eventName] = thisEvent;
            }
        }
        public void TriggerEvent(string eventName)
        {
            Action thisEvent = null;
            if (this.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
                // OR USE instance.eventDictionary[eventName]();
            }
        }

        public void StartListeningAnsyc(string eventName, Action listener)
        {
            Action thisEvent;
            if (this.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                //Add more event to the existing one
                thisEvent += listener;

                //Update the Dictionary
                this.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                //Add event to the Dictionary for the first time
                thisEvent += listener;
                this.eventDictionary.Add(eventName, thisEvent);
                //添加异步是否执行完时间
                this.eventDictAsync.Add(eventName, false);
            }
        }

        public void TriggerEventAnsyc(string eventName)
        {
            if (eventDictAsync.ContainsKey(eventName))
            {
                eventDictAsync[eventName] = true;
            }
            isNeedAsyncLoop = true;
        }

        /// <summary>
        /// 带参数
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public void StartListening(string eventName, Action<EventParam> listener)
        {
            Debug.Log("开始监听" + eventName);
            Action<EventParam> thisEvent;
            if (this.eventDictionaryP.TryGetValue(eventName, out thisEvent))
            {
                //Add more event to the existing one
                thisEvent += listener;

                //Update the Dictionary
                this.eventDictionaryP[eventName] = thisEvent;
            }
            else
            {
                //Add event to the Dictionary for the first time
                thisEvent += listener;
                this.eventDictionaryP.Add(eventName, thisEvent);
            }
        }

        public void StopListening(string eventName, Action<EventParam> listener)
        {
            Action<EventParam> thisEvent;
            if (this.eventDictionaryP.TryGetValue(eventName, out thisEvent))
            {
                //Remove event from the existing one
                thisEvent -= listener;

                //Update the Dictionary
                this.eventDictionaryP[eventName] = thisEvent;
            }
        }

        public void TriggerEvent(string eventName, EventParam eventParam)
        {
            Action<EventParam> thisEvent = null;
            if (this.eventDictionaryP.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(eventParam);
                // OR USE  instance.eventDictionary[eventName](eventParam);
            }
        }

        public void Init()
        {

        }


        public struct EventParam
        {
            public string s;
            public int i;
            public float f;
            public bool b;

            public EventParam SetString(string s)
            {
                this.s = s;
                return this;
            }
            public EventParam SetInt(int i)
            {
                this.i = i;
                return this;
            }
            public EventParam SetFloat(float f)
            {
                this.f = f;
                return this;
            }
            public EventParam SetBool(bool b)
            {
                this.b = b;
                return this;
            }
        }
    }
}


