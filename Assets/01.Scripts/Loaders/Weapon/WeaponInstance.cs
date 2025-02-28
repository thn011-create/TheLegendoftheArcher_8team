using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    public WeaponInfo WeaponData {  get; private set; }
    public float WeaponDamage { get; private set; }
    public WeaponInstance(WeaponInfo weaponInfo)
    {
        WeaponData = weaponInfo;
        WeaponDamage = weaponInfo.Damage;
    }

    public string GetWeponName()
    {
        if (WeaponData == null)
            return "";

        return WeaponData.Name;
    }

    public int GetWeaponId()
    {
        if ( WeaponData == null )
            return 0;
        return WeaponData.key;
    }
}
