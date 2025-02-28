using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : BaseUI
{
    [SerializeField] Text bestStageText;
    [SerializeField] Text rewardExpText;
    [SerializeField] Button closeBtn;


    private void Start()
    {
        uiState = UIState.GameOver;

        closeBtn.onClick.AddListener(OnCloseButtonClick);

        if (GameManager.instance == null)
            Debug.LogError("GameManager.instance�� null�Դϴ�!");

        int currentStage = GameManager.instance.currentWaveIndex;
        int bestStage = PlayerPrefs.GetInt("BestStage");
        if (currentStage > bestStage)
        {
            PlayerPrefs.SetInt("BestStage", currentStage);
            PlayerPrefs.Save();
        }
        bestStageText.text = "�ְ� ���: " + PlayerPrefs.GetInt("BestStage").ToString();
        
    }
   public void gainedExpUI(float amount)
    {
        rewardExpText.text = amount.ToString();
    }

    public void OnCloseButtonClick()
    {
        StartCoroutine(WaitTimeforNextScene());
        
    }
    public IEnumerator WaitTimeforNextScene()
    {
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("HomeScene"); // �� �̸� 
    }


}
