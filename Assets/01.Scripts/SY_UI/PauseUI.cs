using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    // ��ư
    [SerializeField] Button continueBtn;
    [SerializeField] Button exitBtn;
   
    void Start()
    {
        uiState = UIState.Pause;

        continueBtn.onClick.AddListener(OnClickContinueButton);
        continueBtn.onClick.AddListener(OnClickContinueButton);
    }

    
    void Update()
    {
        
    }

    void OnClickContinueButton() 
    {
     
        // �ð� ����?
        // Time.timeScale = 0f;
        // �˾� ���� ���� �̾ �ϱ�    
        gameObject.SetActive(false); 
    }

    void OnClickExitButton() 
    {
        // Ȩ���� ������ (Ȩ �̱���)
        // UIState.HOME
    }

}
