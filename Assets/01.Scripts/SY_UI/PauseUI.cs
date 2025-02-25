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

   
    void Start()
    {
        uiState = UIState.Pause;

        continueBtn.onClick.AddListener(OnClickContinueButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
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

}
