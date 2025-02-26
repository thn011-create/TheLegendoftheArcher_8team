using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData
{
    public int key;
    public string Name;
    public string Description;
    public float Value;
    public int MaxCount;

}

[Serializable]
public class SkillList
{
    public List<SkillData> Items;
}