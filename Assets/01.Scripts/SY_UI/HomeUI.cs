using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // DOTween ���̺귯���� ��� ����


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
            // ���� ���� �˾� ����
        }
    }

    public void OnClickStartBtn() 
    {
        SceneManager.LoadScene("MainScene"); // �� �̸� 
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
