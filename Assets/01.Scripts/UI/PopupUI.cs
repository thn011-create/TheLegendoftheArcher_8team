using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    [SerializeField] Button noBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] Button exitBtn;


    public RectTransform panelTransform; // 팝업 판넬의 transform
    private bool isPopupAcitve = false; // 팝업 열렸는지, 닫혔는지
    public CanvasGroup popupCanvasGroup; // canvas 전체


    public void Start()
    {
        popupCanvasGroup.alpha = 0;  // 처음은 투명한 상태
        popupCanvasGroup.blocksRaycasts = false;  // 클릭x
        popupCanvasGroup.interactable = false;  // 비활성화

        noBtn.onClick.AddListener(OnCloseButtonClick); // no 버튼
        closeBtn.onClick.AddListener(OnCloseButtonClick); // 닫기 버튼
        exitBtn.onClick.AddListener(OnExitButtonClick); // 게임 종료 버튼
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPopupAcitve) { ClosePopup(); }
            else { OpenPopup(); }
        }
    }

    // 팝업 오픈
    public void OpenPopup()
    {
        isPopupAcitve = true;

        // 판넬 활성화
        popupCanvasGroup.gameObject.SetActive(true);

        // DOtween 애니메이션 
        // 2초동안 확대 애니메이션
        popupCanvasGroup.DOFade(1, 0.2f).SetEase(Ease.OutBack);

        popupCanvasGroup.blocksRaycasts = true; // 클릭 활성화
        popupCanvasGroup.interactable = true;
    }

    public void ClosePopup()
    {
        isPopupAcitve = false;
        // 크기 줄어들면서 팝업 사라짐
        popupCanvasGroup.DOFade(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            popupCanvasGroup.blocksRaycasts = false;
            popupCanvasGroup.interactable = false;
        });
    }

    // 게임 이어서하기 버튼 (팝업 닫기)
    public void OnCloseButtonClick()
    {
        ClosePopup();
    }

    // 게임 종료 버튼
    public void OnExitButtonClick()
    {
        // 전처리 작업 필요
        Application.Quit();
    }


}
