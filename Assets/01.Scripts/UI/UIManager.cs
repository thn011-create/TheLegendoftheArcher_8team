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
