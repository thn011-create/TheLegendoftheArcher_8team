using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;
    private WeaponHandler _weaponHandler;

    [SerializeField] private float followRange = 3f;

    public void Init(EnemyManager enemyManager, Transform target, WeaponHandler weaponHandler)
    {
        this.enemyManager = enemyManager;
        this.target = target;
        this._weaponHandler = weaponHandler;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (base._weaponHandler == null || target == null)
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

            if (distance <= base._weaponHandler.AttackRange)
            {
                int layerMaskTarget = base._weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, base._weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

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

        direction = direction * enemyStats.MoveSpeed; // �⺻ �̵� �ӵ� ����
        Debug.Log($"Applying Velocity: {direction}");
        // �˹� ���� ���̸� �̵� �ӵ��� ���̰� �˹� ���͸� �߰�
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // �̵� �ӵ��� 20%�� ����
            direction += knockback; // �˹� �ݿ�
        }

        _rigidbody.velocity = direction; // Rigidbody2D�� ����
        animationHandler.Move(direction);
    }
}

