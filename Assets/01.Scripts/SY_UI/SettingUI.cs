using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    // 소리 설정 슬라이더
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    // 버튼
    [SerializeField] private Button closeButton;
    [SerializeField] private Button saveButton;

    public RectTransform settingTransform; // 세팅창 transform
    private bool isSettingAcitve = false; // 세팅창 열렸는지, 닫혔는지
    public CanvasGroup settingCanvasGroup; // 세팅 canvas 전체



    void Start()
    {
        settingCanvasGroup.alpha = 0;  // 처음은 투명한 상태
        settingCanvasGroup.blocksRaycasts = false;  // 클릭x
        settingCanvasGroup.interactable = false;  // 비활성화

        closeButton.onClick.AddListener(OnCloseButtonClick); // no 버튼
        saveButton.onClick.AddListener(OnSaveButtonClick); // 저장버튼

        // 볼륨 슬라이더
        bgmSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        bgmSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    
    public void SetSFXVolume(float volume) { SoundManager.instance.SetSFXVolume(volume); }
    public void SetMusicVolume(float volume) { SoundManager.instance.SetMusicVolume(volume); } 


    void Update()
    {

    }

    public void SettingOpen()
    {
        isSettingAcitve = true;

        // 판넬 활성화
        settingCanvasGroup.gameObject.SetActive(true);

        // DOtween 애니메이션 
        // 2초동안 확대 애니메이션
        settingCanvasGroup.DOFade(1, 0.2f).SetEase(Ease.OutBack);

        settingCanvasGroup.blocksRaycasts = true; // 클릭 활성화
        settingCanvasGroup.interactable = true; 

    }

    public void SettingClose()
    {
        isSettingAcitve = false;
        // 크기 줄어들면서 팝업 사라짐
        settingCanvasGroup.DOFade(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            settingCanvasGroup.blocksRaycasts = false;
            settingCanvasGroup.interactable = false;
        });
    }

    // 닫기 버튼
    public void OnCloseButtonClick() 
    {
        SettingClose();
    }

    // 설정 저장 버튼 (저장 시스템 구현해야 함)
    public void OnSaveButtonClick() { }

}

