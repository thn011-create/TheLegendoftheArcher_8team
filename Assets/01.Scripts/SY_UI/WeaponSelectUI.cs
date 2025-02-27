using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField] WeaponHandler weaponHandler;

    public void SelectWeapon()
    {
        weaponHandler.Key = 1001;
    }

}
