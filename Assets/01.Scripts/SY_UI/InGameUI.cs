using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : BaseUI
{
    // ���̺� �ѹ�
    [SerializeField] private Text waveText;
    //[SerializeField] private Slider hpSlider;

    
    void Start()
    {
        uiState = UIState.InGame;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }
}
