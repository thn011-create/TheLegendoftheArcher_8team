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

    // hp 슬라이더
    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        uiState = UIState.InGame;
    }

    void Start()
    {
        
        pauseButton.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.Pause));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC눌림");
            
            UIManager.Instance.ChangeState(UIState.Pause);
            Time.timeScale = 0f;



            //if (GamePause)
            //{
            //    Resume();
            //}
            //else
            //{
            //    Pause();
            //}
        }
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
