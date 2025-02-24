using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public enum UIState
{
    //Game Scene
    GameMission, // Ŭ���� ����
    SelectSkill, // �ɷ� ����
    InGame, // �ΰ���
    GameOver, // ���ӿ���
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    private UIState currentState;
    
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            // �� �߰��� DontDestroyOnLoad �߰� ����

            // DontDestroyOnLoad(gameObject);
        }
        // else {Destroy(gameObject);} 
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
