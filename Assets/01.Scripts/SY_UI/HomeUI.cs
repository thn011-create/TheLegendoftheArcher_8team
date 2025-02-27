using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // DOTween 라이브러리를 사용 선언


public class HomeUI : BaseUI
{
    [SerializeField] Button startBtn;
    [SerializeField] Button inventoryBtn;
    [SerializeField] Button shopBtn;
    [SerializeField] Button settingBtn;

    public SettingUI settingUI;

    void Start()
    {
        startBtn.onClick.AddListener(OnClickStartBtn);
        inventoryBtn.onClick.AddListener(OnClickInventoryBtn);
        shopBtn.onClick.AddListener(OnClickShopBtn);
        settingBtn.onClick.AddListener(OnSettingBtn);
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        { 
            // 게임 종료 팝업 띄우기
        }
    }

    public void OnClickStartBtn() 
    {
        SceneManager.LoadScene("MainScene"); // 씬 이름 
    }
    public void OnSettingBtn() 
    {
        settingUI.SettingOpen();
    }
    public void OnClickInventoryBtn() 
    { }
    public void OnClickShopBtn() 
    { }

}
