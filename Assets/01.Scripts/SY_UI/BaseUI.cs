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
        gameObject?.SetActive(true);
        Debug.Log($"{uiState}가 활성화됨");

        // 애니메이션 추가
    }

    // UI 비활성화

    public virtual void HideUI()
    {
        gameObject?.SetActive(false);

        // 애니메이션 추가
    }

    
    

    
    
}
