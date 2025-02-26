using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationHandler
{
    public static readonly int IsMoving;
    public static readonly int IsAttacking;
   
    public static readonly int IsDying;

    public void Move(Vector2 obj); // ������ �� Move �ִϸ��̼� ��
    public void Attack(); // ������ �� Attack �ִϸ��̼� ���
    public void Die(); //���� �� Die �ִϸ��̼� ���
    public void InvincibilityEnd(); // ���� ���� �� Hit �ִϸ��̼� ����
}
