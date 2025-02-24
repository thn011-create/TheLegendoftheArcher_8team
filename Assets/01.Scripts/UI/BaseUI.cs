using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected UIManager uiManager;
    public UIState uiState;
    

    // UI 활성화
    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
    }

    // UI 비활성화

    public virtual void HideUI()
    {
        gameObject?.SetActive(false);
    }

    
    

    
    
}
