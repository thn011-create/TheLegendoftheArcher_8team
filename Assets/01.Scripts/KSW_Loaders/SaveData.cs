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
    //public List<ItemInfo> items { get; set; }

    //인터페이스는 직렬화가 불가능하기 때문에 
    //public Player PlayerStat { get; set; }  // ICharacter 대신 Player를 사용
    public PlayerStats PlayerStats { get; set; }

    //public SaveData(string name, int AttackDamage, int MoveSpeed, int MaxHealth, 
    //    List<WeaponInfo> weapons, PlayerStats playerStats) 
    //{
    //
    //}
    
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
