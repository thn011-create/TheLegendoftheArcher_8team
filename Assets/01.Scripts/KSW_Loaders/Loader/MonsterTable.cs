using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class MonsterTable
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
    public DesignEnums.MonsterGrade MonsterType;

    /// <summary>
    /// 공격력 수치
    /// </summary>
    public float AttackDamage;

    /// <summary>
    /// 이동속도
    /// </summary>
    public float MoveSpeed;

    /// <summary>
    /// 현재 체력
    /// </summary>
    public float CurrentHealth;

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float MaxHealth;

}
public class MonsterTableLoader
{
    public List<MonsterTable> ItemsList { get; private set; }
    public Dictionary<int, MonsterTable> ItemsDict { get; private set; }

    public MonsterTableLoader(string path = "JSON/MonsterTable")
    {
        string jsonData;
        jsonData = Resources.Load<TextAsset>(path).text;
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, MonsterTable>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<MonsterTable> Items;
    }

    public MonsterTable GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public MonsterTable GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
