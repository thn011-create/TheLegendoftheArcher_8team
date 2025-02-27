using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlots : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Button selectButton;
    [SerializeField] private GameObject lockOverlay; // 해금되지 않으면 이미지를 가리는 용도
    private System.Action<WeaponInfo> SelectWeapon; // 플레이어가 선택한 무기

    private WeaponInfo weaponInfo; // 무기 데이터

    public void Init(WeaponInfo weaponData) //bool isUnlock, System.Action<WeaponInfo> select) 
    {
        weaponInfo = weaponData;
        //SelectWeapon = select;

        weaponIcon.sprite = FindImage(weaponData.SpriteIndex);

        selectButton.onClick.AddListener(OnSelectBtnClick);

    }

    private Sprite FindImage(int idx)
    {
        string imageName = $"fantasy_weapons_pack1_noglow_{idx}";
        return Resources.Load<Sprite>($"WeaponImages/{imageName}");
    }

    private void OnSelectBtnClick() 
    {
        SelectWeapon?.Invoke(weaponInfo);
    }

}




