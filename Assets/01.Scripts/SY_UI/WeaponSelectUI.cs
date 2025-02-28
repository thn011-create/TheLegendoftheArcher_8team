using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponSelectUI : BaseUI
{
    [SerializeField] WeaponHandler weaponHandler;
    [SerializeField] PlayerController player;
    [SerializeField] Button weaponSlot;

    [SerializeField] Button confirmBtn;

    [SerializeField] List<Sprite> images;

    public GameObject prefab; // 한 칸에 들어갈 프리팹
    public Transform gridContainer; // Grid Layout Group이 적용된 부모 오브젝트
    public int itemCount = 35; // 생성할 개수

    private GameObject scrollView;

    int[] key = new int[35];
    int[] unlockLevel =
    {
        0,0,0,0,0,0,0,
        5,6,7,8,9,10,11,
        10,11,12,13,14,15,16,
        15,16,17,18,19,20,21,
        20,21,22,23,24,25,26
    };
    public void Awake()
    {
        uiState = UIState.WeaponSelect;
        Time.timeScale = 0f;
        
    }

    void Start()
    {
        PopulateGrid();
        //confirmBtn.onClick.AddListener(() => UIManager.Instance.ChangeState(UIState.InGame));
        confirmBtn.onClick.AddListener(OnConfirmBtn);
    }


    void PopulateGrid()
    {
        for (int i = 0; i < 35; i++)
        {
            weaponSlot = Instantiate(prefab, gridContainer).GetComponent<Button>();

            weaponSlot.image.sprite = images[i];
            if (PlayerPrefs.GetInt("BestStage") < unlockLevel[i])
            {
                weaponSlot.image.sprite = images[35];
            }
            weaponSlot.GetComponentInChildren<Text>().text = "Button " + (i + 1);

            int index = i; // 클로저를 위해 인덱스 저장
            if (weaponSlot.image.sprite != images[35])
            {
                weaponSlot.onClick.AddListener(() => OnButtonClick(index));
            }
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        RangeWeaponHandler rw = player.GetComponentInChildren<RangeWeaponHandler>();

        for (int i = 0; i < 35; i++)
        {
            key[i] = ((i / 7) + 1) + ((i % 7) + 1) * 1000;
        }
        if (player != null && player.WeaponPrefab != null)
        {

            player.WeaponPrefab.Key = key[buttonIndex];
            rw.Key = key[buttonIndex];
            Debug.Log("Weapon changed to key: " + key[buttonIndex]);
        }
        else
        {
            Debug.LogError("Player or WeaponPrefab is not assigned!");
        }
    }

    void OnConfirmBtn()
    {
        Time.timeScale = 1f;
        UIManager.Instance.ChangeState(UIState.InGame);
    }


}
