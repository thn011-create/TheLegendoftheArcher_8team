using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public enum UIState
{
    //Game Scene
    GameMission, // 클리어 조건
    SelectSkill, // 능력 선택
    InGame, // 인게임
    Pause, // 게임 일시정지
    GameOver, // 게임오버
}

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    private static UIManager instance;

    private UIState currentState;

    // UI 딕셔너리
    private Dictionary<UIState, BaseUI> uiDictionary = new Dictionary<UIState, BaseUI>();

    InGameUI InGameUI;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;


            BaseUI[] UIs = GetComponentsInChildren<BaseUI>(true);
            foreach (BaseUI ui in UIs)
            {
                uiDictionary.TryAdd(ui.uiState, ui);
                ui.HideUI();
            }

            foreach (BaseUI ui in UIs)
            {
                Debug.Log($"등록된 UI: {ui.uiState}");
            }

            // 씬 추가시 DontDestroyOnLoad 추가 예정

            // DontDestroyOnLoad(gameObject);
        }
        // else {Destroy(gameObject);} 
    }

    // UI 상태 변경
    public void ChangeState(UIState nextState) // 변경될 UI 상태
    {
        Debug.Log($"[ChangeState] 현재 상태: {currentState}, 변경될 상태: {nextState}");

        // 변화 없으면 return
        if (currentState == nextState) return;

        //딕셔너리에서 현재 상태와 같은 UI가 있다면 숨김
        if (uiDictionary.ContainsKey(currentState))
        {
           uiDictionary[currentState].HideUI();
        }

        // 변경될 ui로 상태 변경
        currentState = nextState;

        // 딕셔너리에서 변경될 UI가 있다면, UI 나타남
        if (uiDictionary.ContainsKey(currentState))
        {
            uiDictionary[currentState].ShowUI();
        }
    }

    // 게임 시작
    public void SetPlayGame()
    {
        Debug.Log("[SetPlayGame] 호출됨!");
        ChangeState(UIState.InGame);
        
    }

    // 다음 웨이브
    public void ChangeWave(int currentWaveIndex)
    {
        InGameUI.UpdateWaveText(currentWaveIndex);
    }


    // hp바 

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        InGameUI.UpdateHPSlider(currentHP/maxHP);
    }

    // 게임 오버
    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    // 게임 일시정지 상태

    public void SetPause() 
    {
        ChangeState(UIState.Pause);
    }

}
