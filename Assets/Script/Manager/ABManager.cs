using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;

/// <summary>
/// AB包管理器
/// </summary>
public class ABManager : Singleton<ABManager>
{
    public Hashtable assetBundles = new Hashtable();//已加载的AB包

    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="path"></param>
    public void LoadAssetBundleWithPath(string path)
    {
        if(assetBundles.ContainsKey(path))
            return;
        //TODO 判断path与abNames集合是否重复加载
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + path);
        assetBundles.Add(path, ab);
    }
    //Is Editor of Ipad Code-App
    //这是在电脑上修改的语句
    public AssetBundle GetAssetBundle(string n)
    {
        return (AssetBundle)assetBundles[n];
    }
}