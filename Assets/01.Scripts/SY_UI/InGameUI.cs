using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : BaseUI
{
    // 웨이브 넘버
    [SerializeField] private Text waveText;

    // pause 버튼
    [SerializeField] private Button pauseButton;

    //[SerializeField] private Slider hpSlider;


    void Start()
    {
        uiState = UIState.InGame;
        pauseButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.Pause));
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void OnClickPauseButton() 
    {
        
    }
}
