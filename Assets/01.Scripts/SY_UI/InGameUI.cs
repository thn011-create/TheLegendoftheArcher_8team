using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : BaseUI
{
    // ���̺� �ѹ�
    [SerializeField] private Text waveText;

    // pause ��ư
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
