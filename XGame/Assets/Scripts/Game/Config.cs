using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static readonly Dictionary<int, ConfigData> porpertyConfig = new Dictionary<int, ConfigData>()
    {
        [2] = new ConfigData("ľͷ", 10, null),
        [4] = new ConfigData("ͭ˿", 10, null),
        [5] = new ConfigData("������", 30, null),
        [7] = new ConfigData("��˿", 50, null),
    };
}

public class ConfigData
{
    public string propertyName;
    public int goldValue;
    public string iconPath;

    public ConfigData(string name,int value,string iconpath)
    {
        propertyName = name;
        goldValue = value;
        iconPath = iconpath;
    }
}
