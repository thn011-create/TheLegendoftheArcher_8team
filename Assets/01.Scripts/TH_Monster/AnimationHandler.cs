using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour, IAnimationHandler
{
    Animator animator;

    // 문자열로 비교하는 것보다 숫자로 비교하는 게 더 좋기 때문에 
    // 문자열을 해쉬값으로 변환해주는 것.
    public static readonly int IsMoving = Animator.StringToHash("IsMove");
    public static readonly int IsAttacking = Animator.StringToHash("IsAttack");
    public static readonly int IsDamage = Animator.StringToHash("IsHit");
    public static readonly int IsDying = Animator.StringToHash("IsDie");
    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Move(Vector2 obj)
    {
        if (IsMoving > 0)
        {
            animator.SetBool(IsMoving, obj.magnitude > .5f);
        }
        else if (IsMoving <= 0)
            return;
    }
    public void Attack()
    {
        animator.SetBool(IsAttacking, true);
    }
    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }

    public void Die()
    {
        animator.SetBool(IsDying, true); ;
    }

}
