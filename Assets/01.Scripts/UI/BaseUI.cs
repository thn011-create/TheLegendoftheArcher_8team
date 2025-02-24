using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected UIManager uiManager;
    public UIState uIState;
    

    // UI Ȱ��ȭ
    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
    }

    // UI ��Ȱ��ȭ

    public virtual void HideUI()
    {
        gameObject?.SetActive(false);
    }

    
    

    
    
}
