using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance { get;  set; } // �̱��� �ν��Ͻ�
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private Text[] skillLevelTexts;
    [SerializeField] private Text[] skillNames;
    [SerializeField] private Text[] skillDescriptions;

    private List<SkillData> currentSkills;

    private void Awake()
    {
        Debug.Log("SkillUI Awake �����");  // ���� Ȯ�ο� �α�

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("SkillUI �̱��� ���� �Ϸ�!");
        }
        else
        {
            Debug.LogWarning("SkillUI �ν��Ͻ��� �̹� �����Ͽ� �����˴ϴ�.");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SkillUI skillUI = FindObjectOfType<SkillUI>();
        if (skillUI == null)
        {
            Debug.LogError("���� SkillUI ������Ʈ�� �����ϴ�! SkillUI �������� �߰��ߴ��� Ȯ���ϼ���.");
        }
        else
        {
            Debug.Log("SkillUI ������Ʈ�� ã�ҽ��ϴ�!");
        }
    }
    public void ShowSkillSelection()
    {
        if (skillPanel == null)
        {
            Debug.LogError("skillPanel�� �������� �ʾҽ��ϴ�!");
            return;
        }
        skillPanel.SetActive(true);
        currentSkills = SkillManager.Instance.GetRandomSkills(3);

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
