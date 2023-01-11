using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ArkRhythm;


public class BMSCreator : EditorWindow
{
    public BMS _bms;
    [MenuItem("Window/BMSCreator")]//在unity菜单Window下有MyWindow选项
    static void Init()
    {
        BMSCreator bMSCreatorWindow = (BMSCreator)EditorWindow.GetWindow(typeof(BMSCreator), false, "BMSCreator", true);//创建窗口
        bMSCreatorWindow.Show();//展示
    }

}
