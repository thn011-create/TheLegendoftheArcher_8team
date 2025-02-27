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
    public int Damage;

    /// <summary>
    /// 설명
    /// </summary>
    public string Description;

    /// <summary>
    /// 공격속도
    /// </summary>
    public float Delay;

    /// <summary>
    /// 투사체 속도
    /// </summary>
    public float Speed;

    /// <summary>
    /// 공격 범위
    /// </summary>
    public float AttackRange;

    /// <summary>
    /// 넉백 여부
    /// </summary>
    public bool isOnKnockback;

    /// <summary>
    /// 넉백 거리
    /// </summary>
    public float KnockbackPower;

    /// <summary>
    /// 넉백 시간
    /// </summary>
    public float KnockbackTime;

    /// <summary>
    /// 이미지 인덱스
    /// </summary>
    public int SpriteIndex;

    /// <summary>
    /// 착용여부
    /// </summary>
    public bool Equip;

    /// <summary>
    /// 튕기는 횟수
    /// </summary>
    public int Bouncing;

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
