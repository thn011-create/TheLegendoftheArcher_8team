using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    
    private Dictionary<int, int> acquiredSkills = new Dictionary<int, int>(); // ȹ���� ��ų (key, ȹ�� Ƚ��)

    private void Awake()
    {
        Instance = this;

    }



    // ������ ��ų ���� (�ߺ� ���)
    public List<AbilityTable> GetRandomSkills()
    {
        List<AbilityTable> availableSkills = DataManager.Instance.AbilityTableLoader.ItemsList;
        List<AbilityTable> returnSkills = new List<AbilityTable>();

        HashSet<int> randomIndexes = new HashSet<int>();
        while (randomIndexes.Count < 3)
        {
            randomIndexes.Add(Random.Range(0, availableSkills.Count));
        }

        foreach (int index in randomIndexes)
        {
            returnSkills.Add(availableSkills[index]);
        }

        return returnSkills;
    }

    // ��ų ȹ�� (�ߺ� ���)
    public void AcquireSkill(AbilityTable skill)
    {
        if (!acquiredSkills.ContainsKey(skill.key))
        {
            acquiredSkills[skill.key] = 0;
        }

        // �ִ� ���� ���� Ȯ��
        if (acquiredSkills[skill.key] < skill.MaxCount)
        {
            acquiredSkills[skill.key]++;
            ApplySkillEffect(skill);
        }
        else
        {
            Debug.Log($"{skill.Name} ��ų�� �ִ� ������ �����߽��ϴ�!");
        }
    }

    // ��ų ȿ�� ����
    private void ApplySkillEffect(AbilityTable skill)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        switch (skill.skillType)
        {
            case DesignEnums.SkillType.DamageUp: // ���ݷ� ����
                player.AttackDamage += skill.Value;
                break;
            case DesignEnums.SkillType.AttackSpeedUp: // ���� �ӵ� ����
                player.AttackSpeed += skill.Value;
                break;
            case DesignEnums.SkillType.MoveSpeedUp: // �̵��ӵ� ����
                player.MoveSpeed += skill.Value;
                break;
            case DesignEnums.SkillType.Critical: // ũ��Ƽ�� Ȯ�� �� ������ ����
                if (skill.Name.Contains("CriticalDamage"))
                {
                    player.CriticalDamage += skill.Value;
                }
                else
                {
                    player.CriticalChance += skill.Value;
                }
                break;
            case DesignEnums.SkillType.Heal: //�ִ�ü�� , ȸ���� , ������ ���� , ȸ�������� �����
                if (skill.Name.Contains("HPBoost"))
                {
                    player.MaxHealth += skill.Value;
                }
                else if (skill.Name.Contains("HealBoost"))
                {
                    player.RecoveryRate += skill.Value; //ȸ���� ����
                }
                else if (skill.Name.Contains("Thirst of Blood"))
                {
                    player.BloodAbsorptionRate += skill.Value;//������ ����
                }
                else if (skill.Name.Contains("HealDropRate"))
                {
                    player.RecoveryDropRate += skill.Value;
                }
                break;
            case DesignEnums.SkillType.HeadShot:

                player.HeadShotRate += skill.Value; // ��弦 Ȯ�� ����
                break;
            case DesignEnums.SkillType.Evasion:
                player.Evasionrate += skill.Value; //ȸ�Ƿ� ����
                break;
            case DesignEnums.SkillType.ExtraProjectile: //�߻�ü ����
                player.ExtraProjectiles += 1;
                break;
        }

    }

    // Ư�� ��ų�� ȹ�� Ƚ�� ��ȯ
    public int GetSkillLevel(int skillKey)
    {
        return acquiredSkills.ContainsKey(skillKey) ? acquiredSkills[skillKey] : 0;
    }
}