using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour, ICharacter
{
    [SerializeField] private string playerName = "Player";
    [SerializeField] private int level = 1;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float experience = 0f;

    public string Name { get => playerName; set => playerName = value; }
    public int Level { get => level; set => level = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Experience { get => experience; set => experience = value; }

    

    
}
