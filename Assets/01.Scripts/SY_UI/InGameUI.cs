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

    // hp �����̴�
    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        uiState = UIState.InGame;
    }

    void Start()
    {
        
        pauseButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.Pause));
    }



    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    

    public void OnClickPauseButton() 
    {
        base.HideUI();
        //base.ShowUI(UIState.Pause);
    }
}
