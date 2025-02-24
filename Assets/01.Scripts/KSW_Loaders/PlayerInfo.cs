using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    DataManager dataManager;

    List<WeaponInfo> weapons;

    private void Awake()
    {
        dataManager = DataManager.Instance;
    }

    private void Start()
    {
        weapons = dataManager.WeaponInfoLoader.ItemsList;
    }
    

}
