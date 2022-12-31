using System;
using UnityEngine;

public abstract class Enum2TypeFactory
{
    //根据所给枚举返回完整类型名
    public static Type GetType(Enum typeEnum)
    {
        var enumName = typeEnum.GetType().Name;

        //去掉后缀得到外部类名
        var outerClassName = "";
        var words = enumName.Split('_');
        outerClassName += words[0];
        for (int i = 1; i < words.Length - 1; i++)
        {
            outerClassName += '_' + words[i];
        }

        //拼接
        string targetClassName = $"{outerClassName}+{enumName}_{Enum.GetName(typeEnum.GetType(), typeEnum)}";
        Type type = Type.GetType(targetClassName);

        if (type == null)
            Debug.LogError($"枚举类型[{typeEnum}]没有找到对应的类，请检查");

        return type;
    }
}