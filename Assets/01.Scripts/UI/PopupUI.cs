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


    public RectTransform panelTransform; // �˾� �ǳ��� transform
    private bool isPopupAcitve = false; // �˾� ���ȴ���, ��������
    public CanvasGroup popupCanvasGroup; // canvas ��ü


    public void Start()
    {
        popupCanvasGroup.alpha = 0;  // ó���� ������ ����
        popupCanvasGroup.blocksRaycasts = false;  // Ŭ��x
        popupCanvasGroup.interactable = false;  // ��Ȱ��ȭ

        noBtn.onClick.AddListener(OnCloseButtonClick); // no ��ư
        closeBtn.onClick.AddListener(OnCloseButtonClick); // �ݱ� ��ư
        exitBtn.onClick.AddListener(OnExitButtonClick); // ���� ���� ��ư
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPopupAcitve) { ClosePopup(); }
            else { OpenPopup(); }
        }
    }

    // �˾� ����
    public void OpenPopup()
    {
        isPopupAcitve = true;

        // �ǳ� Ȱ��ȭ
        popupCanvasGroup.gameObject.SetActive(true);

        // DOtween �ִϸ��̼� 
        // 2�ʵ��� Ȯ�� �ִϸ��̼�
        popupCanvasGroup.DOFade(1, 0.2f).SetEase(Ease.OutBack);

        popupCanvasGroup.blocksRaycasts = true; // Ŭ�� Ȱ��ȭ
        popupCanvasGroup.interactable = true;
    }

    public void ClosePopup()
    {
        isPopupAcitve = false;
        // ũ�� �پ��鼭 �˾� �����
        popupCanvasGroup.DOFade(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            popupCanvasGroup.blocksRaycasts = false;
            popupCanvasGroup.interactable = false;
        });
    }

    // ���� �̾�ϱ� ��ư (�˾� �ݱ�)
    public void OnCloseButtonClick()
    {
        ClosePopup();
    }

    // ���� ���� ��ư
    public void OnExitButtonClick()
    {
        // ��ó�� �۾� �ʿ�
        Application.Quit();
    }


}
