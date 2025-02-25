using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance { get; private set; } // �̱��� �ν��Ͻ�
    public static SkillManager skillManager;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private Text[] skillLevelTexts;
    [SerializeField] private Text[] skillNames;
    [SerializeField] private Text[] skillDescriptions;

    private List<AbilityTable> currentSkills;
    private void Awake()
    {
        // �̱��� ���� ����
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
            Debug.LogError("skillPanel�� �������� �ʾҽ��ϴ�!");
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