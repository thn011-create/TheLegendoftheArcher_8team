using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // DOTween 라이브러리를 사용 선언


public class HomeUI : BaseUI
{
    // 버튼
    [SerializeField] Button startBtn;
    [SerializeField] Button inventoryBtn;
    [SerializeField] Button shopBtn;
    [SerializeField] Button settingBtn;

    // 최고기록 텍스트
    [SerializeField] Text bestStageTxt;

    public SettingUI settingUI;

    void Start()
    {
        startBtn.onClick.AddListener(OnClickStartBtn);
        //inventoryBtn.onClick.AddListener(OnClickInventoryBtn);
        //shopBtn.onClick.AddListener(OnClickShopBtn);
        settingBtn.onClick.AddListener(OnSettingBtn);

        PlayerPrefs.GetInt("BestStage");
        bestStageTxt.text = "최고 기록: " + PlayerPrefs.GetInt("BestStage").ToString();
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
        StartCoroutine(WaitTimeforNextScene());
        
    }

    public IEnumerator WaitTimeforNextScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene"); // 씬 이름 
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
