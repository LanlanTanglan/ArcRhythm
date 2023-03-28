using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLUI;
using System.Reflection;

public class BMSInfoItem : TLItem<BMSInfoItemDataer>
{
    
}

//承接数据器
public class BMSInfoItemDataer
{
    public string name;
    public string bms_author;

    public BMSInfoItemDataer(string name, string bms_author)
    {
        this.name = "铺面名称: " + name;
        this.bms_author = "铺面作者: " + bms_author;
    }
}
