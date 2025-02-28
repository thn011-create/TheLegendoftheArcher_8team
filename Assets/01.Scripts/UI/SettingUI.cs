using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    // �Ҹ� ���� �����̴�
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    // ��ư
    [SerializeField] private Button closeButton;
    [SerializeField] private Button saveButton;

    public RectTransform settingTransform; // ����â transform
    private bool isSettingAcitve = false; // ����â ���ȴ���, ��������
    public CanvasGroup settingCanvasGroup; // ���� canvas ��ü



    void Start()
    {
        settingCanvasGroup.alpha = 0;  // ó���� ������ ����
        settingCanvasGroup.blocksRaycasts = false;  // Ŭ��x
        settingCanvasGroup.interactable = false;  // ��Ȱ��ȭ

        closeButton.onClick.AddListener(OnCloseButtonClick); // no ��ư
        saveButton.onClick.AddListener(OnSaveButtonClick); // �����ư

        // ���� �����̴�
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

        // �ǳ� Ȱ��ȭ
        settingCanvasGroup.gameObject.SetActive(true);

        // DOtween �ִϸ��̼� 
        // 2�ʵ��� Ȯ�� �ִϸ��̼�
        settingCanvasGroup.DOFade(1, 0.2f).SetEase(Ease.OutBack);

        settingCanvasGroup.blocksRaycasts = true; // Ŭ�� Ȱ��ȭ
        settingCanvasGroup.interactable = true; 

    }

    public void SettingClose()
    {
        isSettingAcitve = false;
        // ũ�� �پ��鼭 �˾� �����
        settingCanvasGroup.DOFade(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            settingCanvasGroup.blocksRaycasts = false;
            settingCanvasGroup.interactable = false;
        });
    }

    // �ݱ� ��ư
    public void OnCloseButtonClick() 
    {
        SettingClose();
    }

    // ���� ���� ��ư (���� �ý��� �����ؾ� ��)
    public void OnSaveButtonClick() { }

}

