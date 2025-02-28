using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DropItem
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
    /// 설명
    /// </summary>
    public string Description;

    /// <summary>
    /// 증가율
    /// </summary>
    public int Value;

}
public class DropItemLoader
{
    public List<DropItem> ItemsList { get; private set; }
    public Dictionary<int, DropItem> ItemsDict { get; private set; }

    public DropItemLoader(string path = "JSON/DropItem")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DropItem>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DropItem> Items;
    }

    public DropItem GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public DropItem GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
