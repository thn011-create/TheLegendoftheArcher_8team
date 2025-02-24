using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //Todo : player 클래스 추가되면 위 작업 수행
    public PlayerStats PlayerStats { get; set; }



    public SaveData CreateDefault()
    {
        return new SaveData
        {
            Name = "Default_Name",

            AttackDamage = PlayerStats.AttackDamage,
            MoveSpeed = PlayerStats.MoveSpeed,
            MaxHealth = PlayerStats.MaxHealth,


            Weapons = new List<WeaponInfo>()

        };
    }

    /*public void CreateSaveWeponData()
    {
        Weapons = GameManager.Instance.inventory.ToWeaponData();
    }*/
}
