using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill System/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;
    //public int levelRequirement;
    public SkillType type;
    public int value; // 예: 공격력 증가량, 발사체 추가 개수 등

    public enum SkillType { DamageUp, AttackSpeedUp, ExtraProjectile, Heal , Critical}
}
