using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;
    private AnimationHandler animationHandler;
    public int attackRange = 2;

    [SerializeField] private float followRange = 15f;


    public float delay;
    public float Delay { get => delay; set => delay = value; }

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }


    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;
        if (distance <= followRange)
        {
            lookDirection = direction;

            if (distance <= attackRange)
            {
                int layerMaskTarget = 2;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Player")) | layerMaskTarget);


                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                    MoveMent(movementDirection);
                }

                movementDirection = Vector2.zero;
                return;
            }
            movementDirection = direction;
        }
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }

    protected override void MoveMent(Vector2 direction)
    {
        animationHandler = GetComponent<AnimationHandler>();
        direction = direction * enemyStats.MoveSpeed; // 기본 이동 속도 적용
        //Debug.Log($"Applying Velocity: {direction}");
        // 넉백 지속 중이면 이동 속도를 줄이고 넉백 벡터를 추가
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // 이동 속도를 20%로 줄임
            direction += knockback; // 넉백 반영
        }

        _rigidbody.velocity = direction; // Rigidbody2D에 적용
        animationHandler.Move(direction);
    }

    public override void HandleAttackDelay()
    {
        EnemyStats monster = GetComponent<EnemyStats>();

        if (timeSinceLastAttack <= Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (isAttacking && timeSinceLastAttack > (1f / (Delay * enemyStats.AttackSpeed)))
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected override void Attack()
    {
        if (lookDirection != Vector2.zero)
        {
            AttackAnimation();
        }
    }

    public virtual void AttackAnimation()
    {
        animationHandler.Attack();
    }
}

