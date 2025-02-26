using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : BaseUI
{
    // 버튼
    [SerializeField] Button continueBtn;
    [SerializeField] Button exitBtn;

    // 게임 정지 상태인지 아닌지
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

}
