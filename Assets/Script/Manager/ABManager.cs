using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using ArkRhythm;
using ArkTemplate;
using Spine.Unity;

/// <summary>
/// AB包管理器
/// </summary>
public class ABManager : Singleton<ABManager>
{
    public Hashtable _assetBundles = new Hashtable();//已加载的AB包
    public Dictionary<string, GameObject> _loadedGameobjs = new Dictionary<string, GameObject>();//已加载的Obj
    //Spine管理
    public AssetBundle _skAB = null;
    public Hashtable _materials = new Hashtable();
    public Hashtable _skeletonDatas = new Hashtable();
    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="path"></param>
    public void LoadAssetBundleWithPath(string path)
    {
        if (_assetBundles.ContainsKey(path))
            return;
        //TODO 判断path与abNames集合是否重复加载
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + path);
        _assetBundles.Add(path, ab);
    }
    //Is Editor of Ipad Code-App
    //这是在电脑上修改的语句
    public AssetBundle GetAssetBundle(string n)
    {
        return (AssetBundle)_assetBundles[n];
    }

    public GameObject LoadGameObject(string abName, string objName)
    {
        // Stopwatch sw = new Stopwatch();
        // sw.Start();
        if (!_loadedGameobjs.ContainsKey(abName + "_" + objName))
        {
            _loadedGameobjs.Add(abName + "_" + objName, GetAssetBundle(abName).LoadAsset(objName, typeof(GameObject)) as GameObject);
        }
        // sw.Stop();

        // UnityEngine.Debug.Log("加载AB内容所耗时间" + sw.ElapsedMilliseconds + "ms");
        return _loadedGameobjs[abName + "_" + objName];
    }

    public void _spineInit()
    {
        ABManager.Instance.LoadAssetBundleWithPath("spine_char");
        this._skAB = ABManager.Instance.GetAssetBundle("spine_char");
    }

    public Material GetMaterial(string s)
    {
        if (_skAB == null) _spineInit();
        if (!_materials.ContainsKey(s))
        {
            Material m = _skAB.LoadAsset<Material>(s + "_Material");
            _materials.Add(s, m);
            UnityEngine.Debug.Log("加载Material成功");
        }
        return (Material)_materials[s];
    }
    public SkeletonDataAsset GetSkeletonDataAsset(string s)
    {
        if (_skAB == null) _spineInit();
        if (!_skeletonDatas.ContainsKey(s))
        {
            SkeletonDataAsset m = _skAB.LoadAsset<SkeletonDataAsset>(s + "_SkeletonData");
            _skeletonDatas.Add(s, m);
            UnityEngine.Debug.Log("加载SkeletonDataAsset成功");
        }
        return (SkeletonDataAsset)_skeletonDatas[s];
    }
}