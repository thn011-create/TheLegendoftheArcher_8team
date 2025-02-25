using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    public string Name { get; set; }
    public int AttackDamage { get; set; }
    public float MoveSpeed { get; set; }
    public int MaxHealth { get; set; }
    public List<WeaponInfo> Weapons { get; set; }
    public PlayerStats PlayerStats { get; set; }

    
    public SaveData CreateDefault(PlayerStats playerStats)
    {
        return new SaveData
        {
            Name = "Default_Name",
            AttackDamage = playerStats.AttackDamage,
            MoveSpeed = playerStats.MoveSpeed,
            MaxHealth = playerStats.MaxHealth,
            Weapons = new List<WeaponInfo>(),
            PlayerStats = playerStats
        };
    }

    /*public void CreateSaveWeponData()
    {
        
    }*/
}
