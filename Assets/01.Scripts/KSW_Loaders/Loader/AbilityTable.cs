using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class AbilityTable
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
    public float Value;

    /// <summary>
    /// 중복가능횟수
    /// </summary>
    public int MaxCount;

    /// <summary>
    /// 스킬타입
    /// </summary>
    public DesignEnums.SkillType skillType;

}
public class AbilityTableLoader
{
    public List<AbilityTable> ItemsList { get; private set; }
    public Dictionary<int, AbilityTable> ItemsDict { get; private set; }

    public AbilityTableLoader(string path = "JSON/AbilityTable")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, AbilityTable>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<AbilityTable> Items;
    }

    public AbilityTable GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public AbilityTable GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
