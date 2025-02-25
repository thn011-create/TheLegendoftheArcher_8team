using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    private List<SkillData> allSkills = new List<SkillData>();  // 모든 스킬 데이터 리스트
    private Dictionary<int, int> acquiredSkills = new Dictionary<int, int>(); // 획득한 스킬 (key, 획득 횟수)

    private void Awake()
    {
        if (Instance == null) Instance = this;
        LoadSkillsFromJson();
    }

    // JSON에서 스킬 데이터를 불러오기
    private void LoadSkillsFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("skills");
        if (jsonFile != null)
        {
            SkillList skillList = JsonUtility.FromJson<SkillList>(jsonFile.text);
            allSkills = skillList.Items;

            Debug.Log($"총 {allSkills.Count}개의 스킬이 로드됨");
        }
        else
        {
            Debug.LogError("skills.json 파일을 찾을 수 없습니다.");
        }
    }

    // 랜덤한 스킬 선택 (중복 허용)
    public List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> availableSkills = new List<SkillData>(allSkills);
        //availableSkills.Shuffle();

        return availableSkills.GetRange(0, Mathf.Min(count, availableSkills.Count));
    }

    // 스킬 획득 (중복 허용)
    public void AcquireSkill(SkillData skill)
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
    private void ApplySkillEffect(SkillData skill)
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        switch (skill.skillType)
        {
            case SkillData.SkillType.AttackBoost: // 공격력 증가
                player.AttackDamage += skill.Value;
                break;
            case SkillData.SkillType.AttackSpeedBoost: // 공격 속도 증가
                player.AttackSpeed += skill.Value;
                break;
            case SkillData.SkillType.MoveSpeed: // 이동속도 증가
                player.MoveSpeed += skill.Value;
                break;
            case SkillData.SkillType.Critical: // 크리티컬 확률 및 데미지 증가
                if (skill.Name.Contains("CriticalDamage"))
                {
                    player.CriticalDamage += skill.Value;
                }
                else
                {
                    player.CriticalChance += skill.Value;
                }
                break;
            case SkillData.SkillType.HPBoost: //최대체력 , 회복량 , 흡혈률 증가 , 회복아이템 드랍률
                if (skill.Name.Contains("HPBoost"))
                {
                    player.MaxHealth += skill.Value;
                }
                else if(skill.Name.Contains("HealBoost"))
                {
                    player.RecoveryRate += skill.Value; //회복률 증가
                }
                else if(skill.Name.Contains("Thirst of Blood"))
                {
                    player.BloodAbsorptionRate += skill.Value;//흡혈률 증가
                }
                else if(skill.Name.Contains("HealDropRate"))
                {
                    player.RecoveryDropRate += skill.Value;
                }
                break;
            case SkillData.SkillType.HeadShot: 

                player.HeadShotRate += skill.Value; // 헤드샷 확률 증가
                break;
            case SkillData.SkillType.Evasion:
                player.Evasionrate += skill.Value; //회피률 증가
                break;
            case SkillData.SkillType.AdditionalArrow: //발사체 증가
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
