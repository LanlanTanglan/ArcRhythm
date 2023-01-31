using System.Collections.Generic;
using UnityEngine;
using TLTemplate;
namespace TLUI
{
    public class TLUIManager : Singleton<TLUIManager>
    {

        public void PlayAnimAndLock(GameObject ui, bool isEnter)
        {
            EventManager.Instance.TriggerEvent("lockCanvas", new EventParam());
            ITLAnim[] uiAnims = ui.GetComponentsInChildren<ITLAnim>();
            if (isEnter)
                foreach (ITLAnim a in uiAnims)
                    a.PlayEnterAnim();
            else
                foreach (ITLAnim a in uiAnims)
                    a.PlayLeaveAnim();
        }
    }
}