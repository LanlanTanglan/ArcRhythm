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
        /// <summary>
        /// 无参数
        /// </summary>
        protected override void OnAwake()
        {
            eventDictionary = new Dictionary<string, Action>();
            eventDictionaryP = new Dictionary<string, Action<EventParam>>();
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


