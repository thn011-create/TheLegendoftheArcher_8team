using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    string Name { get; set; }
    int Level { get; set; }
    int AttackDamage { get; set; }
    float MoveSpeed { get; set; }
    int CurrentHealth { get; set; }
    int MaxHealth { get; set; }
    float Experience { get; set; }
}
