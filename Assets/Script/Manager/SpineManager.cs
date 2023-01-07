using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using ArkRhythm;
using Spine.Unity;


public class SpineManager : Singleton<SpineManager>
{
    public AssetBundle _skAB = null;
    public Hashtable _materials = new Hashtable();
    public Hashtable _skeletonDatas = new Hashtable();
    // /// <summary>
    // /// 通过代码动态加载Spine动画资源文件(AB包格式)-AB包主要用于热更新下载，这里是把解压缩zip后的文件放在了对应的位置
    // /// </summary>
    // /// <param name="path">路径名</param>
    // /// <param name="dataAssetName">动画名，例如dankuang_SkeletonData这种名字</param>
    // /// <param name="transform">通过transform，控制动画的位置</param>
    // SkeletonGraphic LoadSpineAnimationFromABPackage(string path, string dataAssetName, Transform transform)
    // {
    //     //通过AB包获得bundle
    //     AssetBundle bundle = AssetBundle.LoadFromFile(path, 0);
    //     if (bundle != null)
    //     {
    //         Material material = Resources.Load<Material>("Spine/SkeletonGraphicDefault");
    //         //通过bundle获得SkeletonDataAsset。
    //         SkeletonDataAsset mySkeletonDataAsset = bundle.LoadAsset<SkeletonDataAsset>(dataAssetName);
    //         //当参数为false时，会释放掉assetbundle里面的关于资源的压缩文件数据。
    //         //当参数为true时，该bundle中加载的物体也将被销毁，如果场景中有物体引用该资源，引用会丢失。
    //         bundle.Unload(false);
    //         SkeletonGraphic myAnimation = SkeletonGraphic.NewSkeletonGraphicGameObject(mySkeletonDataAsset, transform, material);
    //         //设置锚点
    //         myAnimation.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    //         //设置名字
    //         myAnimation.name = myAnimation.skeletonDataAsset.name;
    //         //设置材质
    //         myAnimation.material = material;
    //         //设置层级
    //         myAnimation.transform.SetAsLastSibling();
    //         //将动画尺寸赋给myAnimation
    //         myAnimation.MatchRectTransformWithBounds();
    //         //通过射线控制是否可点击
    //         myAnimation.raycastTarget = false;
    //         //刷新
    //         myAnimation.Initialize(true);
    //         //
    //         return myAnimation;
    //     }
    //     return null;
    // }


    // public string Path()
    // {
    //     string myPath = "";
    //     if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
    //         myPath = Application.dataPath;

    //     if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
    //         myPath = Application.persistentDataPath;

    //     return myPath;
    // }

    public void Init()
    {
        ABManager.Instance.LoadAssetBundleWithPath("spine_char");
        this._skAB = ABManager.Instance.GetAssetBundle("spine_char");
    }

    public Material GetMaterial(string s)
    {
        if (_skAB == null) Init();
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
        if (_skAB == null) Init();
        if (!_skeletonDatas.ContainsKey(s))
        {
            SkeletonDataAsset m = _skAB.LoadAsset<SkeletonDataAsset>(s + "_SkeletonData");
            _skeletonDatas.Add(s, m);
            UnityEngine.Debug.Log("加载SkeletonDataAsset成功");
        }
        return (SkeletonDataAsset)_skeletonDatas[s];
    }
}