using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    string Name { get; set; }
    int Level { get; set; }
    float AttackDamage { get; set; }
    float MoveSpeed { get; set; }
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    float Experience { get; set; }
}
