using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance { get; private set; } // 싱글톤 인스턴스
    public static SkillManager skillManager;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private Text[] skillLevelTexts;
    [SerializeField] private Text[] skillNames;
    [SerializeField] private Text[] skillDescriptions;

    private List<AbilityTable> currentSkills;
    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void ShowSkillSelection()
    {
        skillManager = SkillManager.Instance;
        if (skillPanel == null)
        {
            Debug.LogError("skillPanel이 설정되지 않았습니다!");
            return;
        }
        skillPanel.SetActive(true);
        currentSkills = skillManager.GetRandomSkills();

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < currentSkills.Count)
            {
                skillButtons[i].gameObject.SetActive(true);
                skillNames[i].text = currentSkills[i].Name;
                skillDescriptions[i].text = currentSkills[i].Description;

                int skillLevel = SkillManager.Instance.GetSkillLevel(currentSkills[i].key);
                skillLevelTexts[i].text = skillLevel > 0 ? $"Lv. {skillLevel + 1}" : "Lv. 1";

                int index = i;
                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => SelectSkill(index));
            }
            else
            {
                skillButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SelectSkill(int index)
    {
        SkillManager.Instance.AcquireSkill(currentSkills[index]);
        skillPanel.SetActive(false);
    }
}