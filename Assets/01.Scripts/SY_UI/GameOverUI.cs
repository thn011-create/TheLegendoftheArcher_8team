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
    // ���� �������� ���� ��������, ����
    void OnEnable()
    {
        int currentStage = GameManager.instance.currentWaveIndex;
        int bestStage = PlayerPrefs.GetInt("BestStage");
        if (currentStage > bestStage)
        {
            PlayerPrefs.SetInt("BestStage", currentStage);
            PlayerPrefs.Save();
        }

        bestStageText.text = "�ְ� ���: " + PlayerPrefs.GetInt("BestStage").ToString();
    }
}
