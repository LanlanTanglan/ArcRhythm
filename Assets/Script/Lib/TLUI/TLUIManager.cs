using System.Collections.Generic;
using UnityEngine;
using TLTemplate;
namespace TLUI
{
    public class TLUIManager : Singleton<TLUIManager>
    {
        //事件区域
        //使用队列播放动画
        public void PlayAnimAndLock(GameObject ui, bool isEnter)
        {
            EventManager.Instance.TriggerEvent("lockCanvas", new EventParam());
            Queue<TLBaseAnim> uiQueue = new Queue<TLBaseAnim>();
            uiQueue.Enqueue(ui.GetComponent<TLBaseAnim>());
            while (uiQueue.Count != 0)
            {
                TLBaseAnim top = uiQueue.Dequeue();
                if (isEnter && top.isPlayEnterAnim)
                    top.PlayEnterAnim();
                else if (!isEnter && top.isPlayLeaveAnim)
                    top.PlayLeaveAnim();

                //判断top的子物体是否需要入队
                for (int i = 0; i < top.transform.childCount; i++)
                {
                    // Debug.LogWarning(top.transform.childCount);
                    Transform childTransform = top.transform.GetChild(i);
                    if (childTransform == null)
                    {
                        continue;
                    }
                    TLBaseAnim childUI = childTransform.GetComponent<TLBaseAnim>();
                    if (childUI != null && childUI.isFollowParentAnim)
                    {
                        uiQueue.Enqueue(childUI);
                    }
                }
            }

        }

        public void PlayIAnim()
        {

        }

        //TODO 跟那个UI播放动画一样，也需要讨论Enter的问题
        public void DOUIEnter(GameObject ui)
        {
            ITLLifeCycle[] lcs = ui.GetComponentsInChildren<ITLLifeCycle>();
            foreach (ITLLifeCycle l in lcs)
            {
                l.TLUIEnter();
            }
        }

        public void DOUILeave(GameObject ui)
        {
            ITLLifeCycle[] lcs = ui.GetComponentsInChildren<ITLLifeCycle>();
            foreach (ITLLifeCycle l in lcs)
            {
                l.TLUILeave();
            }
        }
    }
}