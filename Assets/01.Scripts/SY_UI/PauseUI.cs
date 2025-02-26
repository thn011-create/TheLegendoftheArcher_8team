using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.Experimental.GraphView;
using System.Linq;

public class PauseUI : BaseUI
{
    // ��ư
    [SerializeField] Button continueBtn;
    [SerializeField] Button exitBtn;

    // ���� ���� �������� �ƴ���
    public static bool GamePause = false;

    // �÷��̾� ���� text
    [SerializeField] public Text playerNameText;
    [SerializeField] public Text playerLevelText;
    [SerializeField] public Text playerHealthText;
    [SerializeField] public Text playerAttackText;
    [SerializeField] public Text playerSpeedText;

    // ��ų �г�/����
    //[SerializeField] private GameObject skillPanel;
    [SerializeField] private List<GameObject> skillSlots; 
    [SerializeField] private List<Image> skillIcons;
    [SerializeField] private List<Text> skillNames;
    [SerializeField] private List<Text> skillLevels;
    [SerializeField] private List<Text> skillDescriptions;


    void Start()
    {
        uiState = UIState.Pause;

        continueBtn.onClick.AddListener(OnClickContinueButton);
        exitBtn.onClick.AddListener(OnClickExitButton);

        // ������
        PlayerStatsUI();
        // ȹ���� ��ų
        PlayerSkillUI();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ChangeState(UIState.InGame);
            Time.timeScale = 1f;
        }

    }
   
    void OnClickContinueButton() 
    {
     
        // �ð� ����?
        // Time.timeScale = 0f;
        // �˾� ���� ���� �̾ �ϱ�    
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        UIManager.Instance.ChangeState(UIState.InGame);
    }

    void OnClickExitButton() 
    {
        // Ȩ���� ������ 

        SceneManager.LoadScene("HomeScene"); // 0 = HomeScene�̾�� ��
    }

    public void PlayerStatsUI()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null)
        {
            playerNameText.text = $"�̸�: {playerStats.Name}";
            playerLevelText.text = $"����: {playerStats.Level}";
            playerHealthText.text = $"ü��: {playerStats.CurrentHealth} / {playerStats.MaxHealth}";
            playerAttackText.text = $"���ݷ�: {playerStats.AttackDamage}";
            playerSpeedText.text = $"���ݼӵ�: {playerStats.AttackSpeed}";
        }
    }

    public void PlayerSkillUI()
    {
        // ȹ���� ��ų ���
        ///Dictionary<int, int> acquiredSkills = SkillManager.Instance.
        List<AbilityTable> allSkills = DataManager.Instance.AbilityTableLoader.ItemsList;
        List<AbilityTable> acquiredSkills = new List<AbilityTable>();

        int index = 0;


        foreach (var skill in allSkills)
        {


            int skillLevel = SkillManager.Instance.GetSkillLevel(skill.key);
            // ��ų�� ������ ���� ��ų ��Ͽ� �߰�
            if (skillLevel > 0)
            {
                acquiredSkills.Add(skill);
            }
        }
        // ����
        for (int i = 0; i < skillSlots.Count; i++)
        {
            if (i < acquiredSkills.Count)
            {
                AbilityTable skill = acquiredSkills[i];
                int skillLevel = SkillManager.Instance.GetSkillLevel(skill.key);

                skillSlots[i].SetActive(true);
                //skillIcons[i].sprite = SkillUI.Instance.Icon[skill.key - 1];
                skillNames[i].text = skill.Name;
                skillLevels[i].text = $"Lv. {skillLevel}";
                skillDescriptions[i].text = skill.Description;

                // ��ư ������ Image ������Ʈ�� ã�� ������ ����
                //Image iconImage = skillButtons[i].GetComponentInChildren<Image>();
                //if (iconImage != null && index - 1 < Icon.Length)
                //{
                //    iconImage.sprite = Icon[acquiredSkills[i].key - 1];   //Icon[index];
                //    iconImage.gameObject.SetActive(true);
                //}


            }
        }




    }

    // �ش� ��ų ���� ã��
//    AbilityTable skillData = allSkills.Find(s => s.key == skillKey);
//                if (skillData != null)
//                {
//                      UI ����
//                    skillSlots[index].SetActive(true);
//    skillIcons[index].sprite = SkillUI.Instance.Icon[skillData.key - 1];
//                    skillNames[index].text = skillData.Name;
//                    skillLevels[index].text = $"Lv. {skillLevel}";
//                    skillDescriptions[index].text = skillData.Description;
//                }
//            }
//            else
//{
//    ���� ���� ��Ȱ��ȭ
//                slot.SetActive(false);
//}
//index++;
//        }
//    }
}
