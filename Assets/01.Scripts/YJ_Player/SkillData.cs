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
        AttackBoost,       // 0 - ���ݷ� ����
        AttackSpeedBoost,  // 1 - ���ݼӵ� ����
        MoveSpeed,         // 2 - �̵��ӵ� ����
        Critical,          // 3 - ũ��Ƽ�� 
        HPBoost,           // 4 - HP ����
        HeadShot,          // 5 - ��弦
        Evasion,           // 6 - ȸ�Ƿ�
        AdditionalArrow    // 7 - �߻�ü �߰�
    }
}

[Serializable]
public class SkillList
{
    public List<SkillData> Items;
}
