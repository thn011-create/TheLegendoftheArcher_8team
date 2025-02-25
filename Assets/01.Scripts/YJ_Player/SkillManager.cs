using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    private List<SkillData> allSkills = new List<SkillData>();  // 모든 스킬 데이터 리스트
    private Dictionary<int, int> acquiredSkills = new Dictionary<int, int>(); // 획득한 스킬 (key, 획득 횟수)

    private void Awake()
    {
        Instance = this;
    }

    public List<AbilityTable> GetRandomSkills()
    {
        List<AbilityTable> availableSkills = DataManager.Instance.AbilityTableLoader.ItemsList;
        //availableSkills.Shuffle();
        List<AbilityTable> returnSkills = new List<AbilityTable>();
        int[] random = new int[3];
        int j = 0;
        while(true)
        {
            random[j] = Random.Range(0, availableSkills.Count);
            j++;
            if(j>=3)
            {
                if (random[0] == random[1] || random[0] == random[2] || random[1] == random[2])
                {
                    j = 0;
                }
                else
                {
                    break;
                }
            }
        }
        for(int i = 0; i<3; i++)
        {
            returnSkills.Add(availableSkills[i]);
        }
        


        return returnSkills;
    }




    // 스킬 획득 (중복 허용)
    public void AcquireSkill(AbilityTable skill)
    {
        if (!acquiredSkills.ContainsKey(skill.key))
        {
            acquiredSkills[skill.key] = 0;
        }

        // 최대 개수 제한 확인
        if (acquiredSkills[skill.key] < skill.MaxCount)
        {
            acquiredSkills[skill.key]++;
            ApplySkillEffect(skill);
        }
        else
        {
            Debug.Log($"{skill.Name} 스킬이 최대 레벨에 도달했습니다!");
        }
    }

    // 스킬 효과 적용
    private void ApplySkillEffect(AbilityTable skill)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        switch (skill.skillType)
        {
            case DesignEnums.SkillType.DamageUp: // 공격력 증가
                player.AttackDamage += skill.Value;
                break;
            case DesignEnums.SkillType.AttackSpeedUp: // 공격 속도 증가
                player.AttackSpeed += skill.Value;
                break;
            case DesignEnums.SkillType.MoveSpeedUp: // 이동속도 증가
                player.MoveSpeed += skill.Value;
                break;
            case DesignEnums.SkillType.Critical: // 크리티컬 확률 및 데미지 증가
                if (skill.Name.Contains("CriticalDamage"))
                {
                    player.CriticalDamage += skill.Value;
                }
                else
                {
                    player.CriticalChance += skill.Value;
                }
                break;
            case DesignEnums.SkillType.Heal: //최대체력 , 회복량 , 흡혈률 증가 , 회복아이템 드랍률
                if (skill.Name.Contains("HPBoost"))
                {
                    player.MaxHealth += skill.Value;
                }
                else if (skill.Name.Contains("HealBoost"))
                {
                    player.RecoveryRate += skill.Value; //회복률 증가
                }
                else if (skill.Name.Contains("Thirst of Blood"))
                {
                    player.BloodAbsorptionRate += skill.Value;//흡혈률 증가
                }
                else if (skill.Name.Contains("HealDropRate"))
                {
                    player.RecoveryDropRate += skill.Value;
                }
                break;
            case DesignEnums.SkillType.HeadShot:

                player.HeadShotRate += skill.Value; // 헤드샷 확률 증가
                break;
            case DesignEnums.SkillType.Evasion:
                player.Evasionrate += skill.Value; //회피률 증가
                break;
            case DesignEnums.SkillType.ExtraProjectile: //발사체 증가
                player.ExtraProjectiles += 1;
                break;
        }
    }

    // 특정 스킬의 획득 횟수 반환
    public int GetSkillLevel(int skillKey)
    {
        return acquiredSkills.ContainsKey(skillKey) ? acquiredSkills[skillKey] : 0;
    }
}
