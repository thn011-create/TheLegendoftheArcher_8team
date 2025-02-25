using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance { get;  set; } // 싱글톤 인스턴스
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private Text[] skillLevelTexts;
    [SerializeField] private Text[] skillNames;
    [SerializeField] private Text[] skillDescriptions;

    private List<SkillData> currentSkills;

    private void Awake()
    {
        Debug.Log("SkillUI Awake 실행됨");  // 실행 확인용 로그

        if (Instance == null)
        {
            Instance = this;
            Debug.Log("SkillUI 싱글톤 설정 완료!");
        }
        else
        {
            Debug.LogWarning("SkillUI 인스턴스가 이미 존재하여 삭제됩니다.");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SkillUI skillUI = FindObjectOfType<SkillUI>();
        if (skillUI == null)
        {
            Debug.LogError("씬에 SkillUI 오브젝트가 없습니다! SkillUI 프리팹을 추가했는지 확인하세요.");
        }
        else
        {
            Debug.Log("SkillUI 오브젝트를 찾았습니다!");
        }
    }
    public void ShowSkillSelection()
    {
        if (skillPanel == null)
        {
            Debug.LogError("skillPanel이 설정되지 않았습니다!");
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
