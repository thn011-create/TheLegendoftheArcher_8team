using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    // 버튼
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
     
        // 시간 멈춤?
        // Time.timeScale = 0f;
        // 팝업 끄고 게임 이어서 하기    
        gameObject.SetActive(false); 
    }

    void OnClickExitButton() 
    {
        // 홈으로 나가기 (홈 미구현)
        // UIState.HOME
    }

}
