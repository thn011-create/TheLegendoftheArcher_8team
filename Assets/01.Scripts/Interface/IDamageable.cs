using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Attack(); // 공격 처리
    public void TakeDamage(int damage);   //대미지 처리
}
