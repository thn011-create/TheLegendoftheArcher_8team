using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : BaseUI
{
    [SerializeField] Text bestStageText;
    [SerializeField] Button closeBtn;


    private void Start()
    {
        uiState = UIState.GameOver;

        closeBtn.onClick.AddListener(OnCloseButtonClick);
    }
    // 현재 스테이지 정보 가져오고, 저장
    //void OnEnable()
    //{
    //    //int currentStage = GameManager.instance.currentWaveIndex;
    //    int bestStage = PlayerPrefs.GetInt("BestStage");
    //    if (currentStage > bestStage)
    //    {
    //        PlayerPrefs.SetInt("BestStage", currentStage);
    //        PlayerPrefs.Save();
    //    }

    //    bestStageText.text = "최고 기록: " + PlayerPrefs.GetInt("BestStage").ToString();
    //}

    public void OnCloseButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }


}
