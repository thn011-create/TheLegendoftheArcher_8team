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

    //�������̽��� ����ȭ�� �Ұ����ϱ� ������ 
    //public Player PlayerStat { get; set; }  // ICharacter ��� Player�� ���
    //Todo : player Ŭ���� �߰��Ǹ� �� �۾� ����
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
