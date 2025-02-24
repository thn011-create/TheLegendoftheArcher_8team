using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public WeaponInfoLoader WeaponInfoLoader {  get; private set; }
    public AbilityTableLoader AbilityTableLoader { get; private set; }

    public void Initialize()
    {
        WeaponInfoLoader = new WeaponInfoLoader();
        AbilityTableLoader = new AbilityTableLoader();
    }
}
