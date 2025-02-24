using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour, IAnimationHandler
{
    Animator animator;

    public static readonly int IsMoving = Animator.StringToHash("IsMove");
    public static readonly int IsAttacking = Animator.StringToHash("IsAttack");
    public static readonly int IsHitting = Animator.StringToHash("IsHit");
    public static readonly int IsDying = Animator.StringToHash("IsDie");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }
    public void Attack()
    {
        animator.SetBool(IsAttacking, true);
    }
    public void Hit()
    {
        animator.SetBool(IsHitting, true);
    }

    public void Die()
    {
        animator.SetBool(IsDying, true); ;
    }


    public void InvincibilityEnd()
    {
        animator.SetBool(IsHitting, false);
    }

}
