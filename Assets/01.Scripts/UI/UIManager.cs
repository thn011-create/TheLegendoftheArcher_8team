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
    GameOver, // 게임오버
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    private UIState currentState;

    // UI 딕셔너리
    private Dictionary<UIState, BaseUI> uiDictionary = new Dictionary<UIState, BaseUI>();

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
                uiDictionary.Add(ui.uiState, ui);
                ui.HideUI();
            }


            // 씬 추가시 DontDestroyOnLoad 추가 예정

            // DontDestroyOnLoad(gameObject);
        }
        // else {Destroy(gameObject);} 
    }

    // UI 상태 변경
    public void ChangeState(UIState nextState) // 변경될 UI 상태
    {
        // 변화 없으면 return
        if (currentState == nextState) return;

        // 딕셔너리에서 현재 상태와 같은 UI가 있다면 숨김
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
}
