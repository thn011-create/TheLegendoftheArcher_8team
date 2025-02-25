using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill System/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;
    //public int levelRequirement;
    public SkillType type;
    public int value; // ��: ���ݷ� ������, �߻�ü �߰� ���� ��

    public enum SkillType { DamageUp, AttackSpeedUp, ExtraProjectile, Heal , Critical}
}
