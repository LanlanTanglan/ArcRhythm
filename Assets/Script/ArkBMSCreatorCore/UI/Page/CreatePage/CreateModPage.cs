using UnityEngine;
using TLUI;

public class CreateModPage : TLPanel
{
    public TLScrollController<BMSInfoItemDataer> _scrollController;
    public GameObject _inputBMSWarpObj;
    public string _path;
    public override void Awake()
    {
        _scrollController = GetComponentInChildren<TLScrollController<BMSInfoItemDataer>>();
    }

    //新建铺面
    public void NewBMS()
    {
        TLUIManager.Instance.PlayAnimAndLock(_inputBMSWarpObj, true);
        TLUIManager.Instance.DOUIEnter(_inputBMSWarpObj);
    }

    //关闭输入行
    public void InpuBMSWarpObj_Close()
    {
        TLUIManager.Instance.PlayAnimAndLock(_inputBMSWarpObj, false);
        TLUIManager.Instance.DOUILeave(_inputBMSWarpObj);
    }

    //开始
    public void Begin()
    {
        TLUIManager.Instance.PlayAnimAndLock(_inputBMSWarpObj, false);
        TLUIManager.Instance.DOUILeave(_inputBMSWarpObj);
        //TODO 获取输入框上的数据
        //TODO 建立一个json文件用于存储数据，放在指定的文件夹下
    }

    public override void TLUIEnter()
    {
        //TODO 检查是否有可以读取的铺面文件

        //TODO 将铺面文件放置在Content下
    }
}