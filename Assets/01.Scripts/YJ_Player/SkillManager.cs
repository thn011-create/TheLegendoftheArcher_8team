using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    [SerializeField] private List<SkillData> allSkills; // 모든 스킬 목록
    private Dictionary<SkillData, int> acquiredSkills = new Dictionary<SkillData, int>(); // 획득한 스킬과 중복 개수 저장

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // 랜덤한 스킬 가져오기 (중복 획득 가능)
    public List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> availableSkills = new List<SkillData>(allSkills);
        //availableSkills.Shuffle();

        return availableSkills.GetRange(0, Mathf.Min(count, availableSkills.Count));
    }

    // 스킬 획득 (중복 가능)
    public void AcquireSkill(SkillData skill)
    {
        if (!acquiredSkills.ContainsKey(skill))
        {
            acquiredSkills[skill] = 0;
        }
        acquiredSkills[skill]++; // 중복 획득 가능

        ApplySkillEffect(skill);
    }

    // 스킬 효과 적용 (중첩 고려)
    private void ApplySkillEffect(SkillData skill)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        switch (skill.type)
        {
            case SkillData.SkillType.DamageUp:
                player.AttackDamage += skill.value; // 공격력 증가
                break;
            case SkillData.SkillType.AttackSpeedUp:
                player.AttackSpeed += skill.value; //  공격속도 증가
                break;
            case SkillData.SkillType.ExtraProjectile:
                player.ExtraProjectiles += 1; // 발사체 수 증가 (스킬당 +1)
                break;
            case SkillData.SkillType.MoveSpeedUp:
                player.MoveSpeed += skill.value;
                break;
            case SkillData.SkillType.Heal: // 체력회복 , 최대 체력 증가
                if(skill.name.Equals("최대체력증가"))
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
                if(skill.name.Equals("크리티컬확률증가"))
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

    // 특정 스킬의 획득 횟수 반환 (UI에 표시용)
    public int GetSkillLevel(SkillData skill)
    {
        return acquiredSkills.ContainsKey(skill) ? acquiredSkills[skill] : 0;
    }
}
