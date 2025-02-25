using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    [SerializeField] private List<SkillData> allSkills; // ��� ��ų ���
    private Dictionary<SkillData, int> acquiredSkills = new Dictionary<SkillData, int>(); // ȹ���� ��ų�� �ߺ� ���� ����

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // ������ ��ų �������� (�ߺ� ȹ�� ����)
    public List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> availableSkills = new List<SkillData>(allSkills);
        //availableSkills.Shuffle();

        return availableSkills.GetRange(0, Mathf.Min(count, availableSkills.Count));
    }

    // ��ų ȹ�� (�ߺ� ����)
    public void AcquireSkill(SkillData skill)
    {
        if (!acquiredSkills.ContainsKey(skill))
        {
            acquiredSkills[skill] = 0;
        }
        acquiredSkills[skill]++; // �ߺ� ȹ�� ����

        ApplySkillEffect(skill);
    }

    // ��ų ȿ�� ���� (��ø ���)
    private void ApplySkillEffect(SkillData skill)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        switch (skill.type)
        {
            case SkillData.SkillType.DamageUp:
                player.AttackDamage += skill.value; // ���ݷ� ����
                break;
            case SkillData.SkillType.AttackSpeedUp:
                player.AttackSpeed += skill.value; //  ���ݼӵ� ����
                break;
            case SkillData.SkillType.ExtraProjectile:
                player.ExtraProjectiles += 1; // �߻�ü �� ���� (��ų�� +1)
                break;
            case SkillData.SkillType.MoveSpeedUp:
                player.MoveSpeed += skill.value;
                break;
            case SkillData.SkillType.Heal: // ü��ȸ�� , �ִ� ü�� ����
                if(skill.name.Equals("�ִ�ü������"))
                {
                    player.MaxHealth += skill.value;
                    player.CurrentHealth += skill.value;   
                }
                else
                {
                    player.CurrentHealth += skill.value;
                }                
                break;
            case SkillData.SkillType.Critical:
                if(skill.name.Equals("ũ��Ƽ��Ȯ������"))
                {
                    player.CriticalChance += skill.value;
                }
                else
                {
                    player.CriticalDamage += skill.value;
                }
                
                break;
        }
    }

    // Ư�� ��ų�� ȹ�� Ƚ�� ��ȯ (UI�� ǥ�ÿ�)
    public int GetSkillLevel(SkillData skill)
    {
        return acquiredSkills.ContainsKey(skill) ? acquiredSkills[skill] : 0;
    }
}
