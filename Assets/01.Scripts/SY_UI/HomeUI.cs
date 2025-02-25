using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeUI : BaseUI
{
    [SerializeField] Button startBtn;
    [SerializeField] Button inventoryBtn;
    [SerializeField] Button shopBtn;


    void Start()
    {
        startBtn.onClick.AddListener(OnClickStartBtn);
        inventoryBtn.onClick.AddListener(OnClickInventoryBtn);
        shopBtn.onClick.AddListener(OnClickShopBtn);
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
        SceneManager.LoadScene("UIScene"); // �� �̸� ���濹��
    }
    public void OnClickInventoryBtn() 
    { }
    public void OnClickShopBtn() 
    { }
}
