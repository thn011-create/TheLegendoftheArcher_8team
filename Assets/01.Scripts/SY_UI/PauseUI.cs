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
    // 버튼
    [SerializeField] Button continueBtn;
    [SerializeField] Button exitBtn;

    // 게임 정지 상태인지 아닌지
    public static bool GamePause = false;

    // 플레이어 정보 text
    [SerializeField] public Text playerNameText;
    [SerializeField] public Text playerLevelText;
    [SerializeField] public Text playerHealthText;
    [SerializeField] public Text playerAttackText;
    [SerializeField] public Text playerSpeedText;

    // 스킬 패널/정보
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

        // 내정보
        PlayerStatsUI();
        // 획득한 스킬
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
     
        // 시간 멈춤?
        // Time.timeScale = 0f;
        // 팝업 끄고 게임 이어서 하기    
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        UIManager.Instance.ChangeState(UIState.InGame);
    }

    void OnClickExitButton() 
    {
        // 홈으로 나가기 

        SceneManager.LoadScene("HomeScene"); // 0 = HomeScene이어야 함
    }

    public void PlayerStatsUI()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null)
        {
            playerNameText.text = $"이름: {playerStats.Name}";
            playerLevelText.text = $"레벨: {playerStats.Level}";
            playerHealthText.text = $"체력: {playerStats.CurrentHealth} / {playerStats.MaxHealth}";
            playerAttackText.text = $"공격력: {playerStats.AttackDamage}";
            playerSpeedText.text = $"공격속도: {playerStats.AttackSpeed}";
        }
    }

    public void PlayerSkillUI()
    {
        // 획득한 스킬 목록
        ///Dictionary<int, int> acquiredSkills = SkillManager.Instance.
        List<AbilityTable> allSkills = DataManager.Instance.AbilityTableLoader.ItemsList;
        List<AbilityTable> acquiredSkills = new List<AbilityTable>();

        int index = 0;


        foreach (var skill in allSkills)
        {


            int skillLevel = SkillManager.Instance.GetSkillLevel(skill.key);
            // 스킬을 얻으면 얻은 스킬 목록에 추가
            if (skillLevel > 0)
            {
                acquiredSkills.Add(skill);
            }
        }
        // 슬롯
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

                // 버튼 내부의 Image 컴포넌트를 찾아 아이콘 적용
                //Image iconImage = skillButtons[i].GetComponentInChildren<Image>();
                //if (iconImage != null && index - 1 < Icon.Length)
                //{
                //    iconImage.sprite = Icon[acquiredSkills[i].key - 1];   //Icon[index];
                //    iconImage.gameObject.SetActive(true);
                //}


            }
        }




    }

    // 해당 스킬 정보 찾기
//    AbilityTable skillData = allSkills.Find(s => s.key == skillKey);
//                if (skillData != null)
//                {
//                      UI 적용
//                    skillSlots[index].SetActive(true);
//    skillIcons[index].sprite = SkillUI.Instance.Icon[skillData.key - 1];
//                    skillNames[index].text = skillData.Name;
//                    skillLevels[index].text = $"Lv. {skillLevel}";
//                    skillDescriptions[index].text = skillData.Description;
//                }
//            }
//            else
//{
//    남은 슬롯 비활성화
//                slot.SetActive(false);
//}
//index++;
//        }
//    }
}
