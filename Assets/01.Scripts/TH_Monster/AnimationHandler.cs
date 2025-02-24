using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour, IAnimationHandler
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Hit()
    {
        throw new System.NotImplementedException();
    }

    public void InvincibilityEnd()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector2 obj)
    {
        throw new System.NotImplementedException();
    }
}
