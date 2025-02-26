using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public enum UIState
{
    //Game Scene
    //GameMission, // Ŭ���� ����
    //SelectSkill, // �ɷ� ����
    InGame, // �ΰ���
    Pause, // ���� �Ͻ�����
    GameOver, // ���ӿ���
}

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    private static UIManager instance;

    private UIState currentState;

    // UI ��ųʸ�
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
                Debug.Log($"��ϵ� UI: {ui.uiState}");
            }

            currentState = UIState.InGame;

            if (uiDictionary.ContainsKey(UIState.InGame))
            {
                uiDictionary[UIState.InGame].ShowUI();
            }
            // �� �߰��� DontDestroyOnLoad �߰� ����

            // DontDestroyOnLoad(gameObject);
        }
        // else {Destroy(gameObject);} 
    }

    // UI ���� ����
    public void ChangeState(UIState nextState) // ����� UI ����
    {
        Debug.Log($"[ChangeState] ���� ����: {currentState}, ����� ����: {nextState}");

       
        

        //��ųʸ����� ���� ���¿� ���� UI�� �ִٸ� ���� / nextState�� ���� ���¿� �ٸ� ���� ����
        if (uiDictionary.ContainsKey(currentState) && currentState != nextState)
        {
           uiDictionary[currentState].HideUI();
        }
        

        if (currentState == nextState)
        {
            if (uiDictionary.ContainsKey(currentState))
            {
                uiDictionary[currentState].ShowUI();
            }
            // ��ȭ ������ return
            return;
        }

        // ����� ui�� ���� ����
        currentState = nextState;

        // ��ųʸ����� ����� UI�� �ִٸ�, UI ��Ÿ��
        if (uiDictionary.ContainsKey(currentState))
        {
            uiDictionary[currentState].ShowUI();
        }
    }

    // ���� ����
    public void SetPlayGame()
    {
        Debug.Log("[SetPlayGame] ȣ���!");
        ChangeState(UIState.InGame);
        
    }

    // ���� ���̺�
    public void ChangeWave(int currentWaveIndex)
    {
        InGameUI.UpdateWaveText(currentWaveIndex);
    }


    // hp�� 

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        InGameUI.UpdateHPSlider(currentHP/maxHP);
    }

    // ���� ����
    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    // ���� �Ͻ����� ����

    public void SetPause() 
    {
        ChangeState(UIState.Pause);
    }

}
