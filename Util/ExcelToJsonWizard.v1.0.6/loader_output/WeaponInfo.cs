using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class WeaponInfo
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// 이름
    /// </summary>
    public string Name;

    /// <summary>
    /// 등급
    /// </summary>
    public DesignEnums.Grade Grade;

    /// <summary>
    /// 공격력 수치
    /// </summary>
    public float Atk;

    /// <summary>
    /// 공격속도
    /// </summary>
    public float Speed;

    /// <summary>
    /// 설명
    /// </summary>
    public string Description;

}
public class WeaponInfoLoader
{
    public List<WeaponInfo> ItemsList { get; private set; }
    public Dictionary<int, WeaponInfo> ItemsDict { get; private set; }

    public WeaponInfoLoader(string path = "JSON/WeaponInfo")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, WeaponInfo>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<WeaponInfo> Items;
    }

    public WeaponInfo GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public WeaponInfo GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
