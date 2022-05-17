using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ArkRhythm;

/// <summary>
/// 等级Warp的点击事件
/// </summary>
public class LevelWarpCotro : MonoBehaviour, IPointerClickHandler
{
    public STAFF_LEVEL warpLevel;

    public ChpMusicListWarpCotro cmlwC;

    void Start()
    {
        cmlwC = transform.GetComponentInParent<ChpMusicListWarpCotro>();
    }

    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //当前音乐没有该等级的铺面,无需切换
        if ((int)warpLevel > cmlwC.currentMusic.level.Count)
            return;

        //修改当前指向等级列表
        cmlwC.ChangeLevelList(warpLevel);

        //移动移动条
        cmlwC.levelBarCotro.hoverLevelCotro.ChangeLevel(warpLevel, cmlwC.currentMusic.level[(int)warpLevel - 1]);

    }
}
