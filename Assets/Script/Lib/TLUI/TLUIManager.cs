using System.Collections.Generic;
using UnityEngine;
using TLTemplate;
namespace TLUI
{
    public class TLUIManager : Singleton<TLUIManager>
    {
        
        public void PlayAnim(GameObject ui, bool isEnter)
        {
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