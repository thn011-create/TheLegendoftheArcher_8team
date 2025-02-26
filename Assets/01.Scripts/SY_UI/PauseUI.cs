using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    void Start()
    {
        uiState = UIState.Pause;

        continueBtn.onClick.AddListener(OnClickContinueButton);
        exitBtn.onClick.AddListener(OnClickExitButton);

        PlayerStatsUI();
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
}
