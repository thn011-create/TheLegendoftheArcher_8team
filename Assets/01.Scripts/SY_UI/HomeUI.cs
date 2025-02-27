using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // DOTween ���̺귯���� ��� ����


public class HomeUI : BaseUI
{
    // ��ư
    [SerializeField] Button startBtn;
    [SerializeField] Button inventoryBtn;
    [SerializeField] Button shopBtn;
    [SerializeField] Button settingBtn;

    // �ְ��� �ؽ�Ʈ
    [SerializeField] Text bestStageTxt;

    public SettingUI settingUI;

    void Start()
    {
        startBtn.onClick.AddListener(OnClickStartBtn);
        //inventoryBtn.onClick.AddListener(OnClickInventoryBtn);
        //shopBtn.onClick.AddListener(OnClickShopBtn);
        settingBtn.onClick.AddListener(OnSettingBtn);

        PlayerPrefs.GetInt("BestStage");
        bestStageTxt.text = "�ְ� ���: " + PlayerPrefs.GetInt("BestStage").ToString();
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
        StartCoroutine(WaitTimeforNextScene());
        
    }

    public IEnumerator WaitTimeforNextScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene"); // �� �̸� 
    }

    public void OnSettingBtn() 
    {
        settingUI.SettingOpen();
    }
    //public void OnClickInventoryBtn() 
    //{ }
    //public void OnClickShopBtn() 
    //{ }
}
