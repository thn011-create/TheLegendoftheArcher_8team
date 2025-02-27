using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] Text bestStageText;


    private void Start()
    {
        uiState = UIState.GameOver;
    }
    // 현재 스테이지 정보 가져오고, 저장
    void OnEnable()
    {
        int currentStage = GameManager.instance.currentWaveIndex;
        int bestStage = PlayerPrefs.GetInt("BestStage");
        if (currentStage > bestStage)
        {
            PlayerPrefs.SetInt("BestStage", currentStage);
            PlayerPrefs.Save();
        }

        bestStageText.text = "최고 기록: " + PlayerPrefs.GetInt("BestStage").ToString();
    }
}
