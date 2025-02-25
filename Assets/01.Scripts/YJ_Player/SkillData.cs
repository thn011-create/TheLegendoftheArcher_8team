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
    public SkillType skillType;

    public enum SkillType
    {
        AttackBoost,       // 0 - 공격력 증가
        AttackSpeedBoost,  // 1 - 공격속도 증가
        MoveSpeed,         // 2 - 이동속도 증가
        Critical,          // 3 - 크리티컬 
        HPBoost,           // 4 - HP 관련
        HeadShot,          // 5 - 헤드샷
        Evasion,           // 6 - 회피률
        AdditionalArrow    // 7 - 발사체 추가
    }
}

[Serializable]
public class SkillList
{
    public List<SkillData> Items;
}
