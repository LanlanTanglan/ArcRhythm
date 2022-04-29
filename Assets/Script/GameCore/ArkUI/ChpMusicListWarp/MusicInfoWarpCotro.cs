using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;


/// <summary>
/// 音乐列表
/// </summary>
public class MusicInfoWarpCotro : MonoBehaviour
{
    public string musicName;
    public string author;
    public List<int> level;//铺面等级
    public Music musicInfo;
    public Transform musicNameT;
    public Transform authorT;
    public Transform levelCountT;
    void Awake()
    {
        musicNameT = transform.Find("Info/MusicName");
        authorT = transform.Find("Info/Author");
        levelCountT = transform.Find("Level/count");
    }
    void Start()
    {

    }
    void Update()
    {

    }

    public void Init()
    {
        
    }

    //TODO 通过事件修改count
}
