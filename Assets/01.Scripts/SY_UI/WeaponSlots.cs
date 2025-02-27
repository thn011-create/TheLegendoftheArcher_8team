using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlots : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Button selectButton;
    [SerializeField] private GameObject lockOverlay; // �رݵ��� ������ �̹����� ������ �뵵
    private System.Action<WeaponInfo> SelectWeapon; // �÷��̾ ������ ����

    private WeaponInfo weaponInfo; // ���� ������

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




