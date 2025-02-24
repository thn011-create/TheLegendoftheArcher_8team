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

            // 씬 추가시 DontDestroyOnLoad 추가 예정

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
