using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    public WeaponInfoLoader WeaponInfoLoader {  get; private set; }
    public AbilityTableLoader AbilityTableLoader { get; private set; }
    public DropItemLoader DropItemLoader { get; private set; }

    public void Initialize()
    {
        WeaponInfoLoader = new WeaponInfoLoader();
        AbilityTableLoader = new AbilityTableLoader();
        DropItemLoader = new DropItemLoader();
    }
}
