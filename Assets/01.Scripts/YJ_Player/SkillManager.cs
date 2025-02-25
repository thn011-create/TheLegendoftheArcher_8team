using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    private List<SkillData> allSkills = new List<SkillData>();  // ��� ��ų ������ ����Ʈ
    private Dictionary<int, int> acquiredSkills = new Dictionary<int, int>(); // ȹ���� ��ų (key, ȹ�� Ƚ��)

    private void Awake()
    {
        if (Instance == null) Instance = this;
        LoadSkillsFromJson();
    }

    // JSON���� ��ų �����͸� �ҷ�����
    private void LoadSkillsFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("skills");
        if (jsonFile != null)
        {
            SkillList skillList = JsonUtility.FromJson<SkillList>(jsonFile.text);
            allSkills = skillList.Items;

            Debug.Log($"�� {allSkills.Count}���� ��ų�� �ε��");
        }
        else
        {
            Debug.LogError("skills.json ������ ã�� �� �����ϴ�.");
        }
    }

    // ������ ��ų ���� (�ߺ� ���)
    public List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> availableSkills = new List<SkillData>(allSkills);
        //availableSkills.Shuffle();

        return availableSkills.GetRange(0, Mathf.Min(count, availableSkills.Count));
    }

    // ��ų ȹ�� (�ߺ� ���)
    public void AcquireSkill(SkillData skill)
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
    private void ApplySkillEffect(SkillData skill)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        switch (skill.skillType)
        {
            case SkillData.SkillType.AttackBoost: // ���ݷ� ����
                player.AttackDamage += skill.Value;
                break;
            case SkillData.SkillType.AttackSpeedBoost: // ���� �ӵ� ����
                player.AttackSpeed += skill.Value;
                break;
            case SkillData.SkillType.MoveSpeed: // �̵��ӵ� ����
                player.MoveSpeed += skill.Value;
                break;
            case SkillData.SkillType.Critical: // ũ��Ƽ�� Ȯ�� �� ������ ����
                if (skill.Name.Contains("CriticalDamage"))
                {
                    player.CriticalDamage += skill.Value;
                }
                else
                {
                    player.CriticalChance += skill.Value;
                }
                break;
            case SkillData.SkillType.HPBoost: //�ִ�ü�� , ȸ���� , ������ ���� , ȸ�������� �����
                if (skill.Name.Contains("HPBoost"))
                {
                    player.MaxHealth += skill.Value;
                }
                else if(skill.Name.Contains("HealBoost"))
                {
                    player.RecoveryRate += skill.Value; //ȸ���� ����
                }
                else if(skill.Name.Contains("Thirst of Blood"))
                {
                    player.BloodAbsorptionRate += skill.Value;//������ ����
                }
                else if(skill.Name.Contains("HealDropRate"))
                {
                    player.RecoveryDropRate += skill.Value;
                }
                break;
            case SkillData.SkillType.HeadShot: 

                player.HeadShotRate += skill.Value; // ��弦 Ȯ�� ����
                break;
            case SkillData.SkillType.Evasion:
                player.Evasionrate += skill.Value; //ȸ�Ƿ� ����
                break;
            case SkillData.SkillType.AdditionalArrow: //�߻�ü ����
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
