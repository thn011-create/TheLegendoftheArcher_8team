using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int AttackDamage { get; set; }
    public float MoveSpeed { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public float Experience { get; set; }
}
