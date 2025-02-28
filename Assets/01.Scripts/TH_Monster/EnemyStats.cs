using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, ICharacter
{
    [SerializeField] private string enemyname;
    [SerializeField] private int level = 1;
    [SerializeField] private float attackDamage = 5;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float experience = 0;
    [SerializeField] private float attackSpeed = 1;



    public string Name { get => enemyname; set => enemyname = value; }
    public int Level { get => level; set => level = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Experience { get; set; }

    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
}
