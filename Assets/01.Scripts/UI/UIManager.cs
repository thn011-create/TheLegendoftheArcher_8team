using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
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
