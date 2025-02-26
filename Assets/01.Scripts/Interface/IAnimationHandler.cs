using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationHandler
{
    public static readonly int IsMoving;
    public static readonly int IsAttacking;
   
    public static readonly int IsDying;

    public void Move(Vector2 obj); // 움직일 때 Move 애니메이션 재
    public void Attack(); // 공격할 때 Attack 애니메이션 재생
    public void Die(); //죽을 때 Die 애니메이션 재생
    public void InvincibilityEnd(); // 무적 끝날 때 Hit 애니메이션 종료
}
