using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager; // 적 매니저 참조
    private Transform target; // 타겟(플레이어 등) 참조

    [SerializeField] private float followRange = 15f; // 적이 플레이어를 추적하는 범위

    // 적 초기화 (적 매니저와 타겟 설정)
    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    // 타겟과의 거리 계산
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    // 적의 행동 처리
    protected override void HandleAction()
    {
        base.HandleAction();

        if (_weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget(); // 타겟과의 거리 측정
        Vector2 direction = DirectionToTarget(); // 타겟 방향 계산

        isAttacking = false;
        if (distance <= followRange) // 타겟이 추적 범위 내에 있는 경우
        {
            lookDirection = direction;
            // 벽 감지 Raycast 추가
            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, 1f, 1 << LayerMask.NameToLayer("Level"));
            if (wallHit.collider != null)
            {
                // 벽을 감지하면 경로 변경
                movementDirection = GetAlternativeDirection(direction);
            }
            else
            {
                movementDirection = direction;
            }

            if (distance <= _weaponHandler.AttackRange) // 타겟이 공격 범위 내에 있는 경우
            {
                int layerMaskTarget = _weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, direction, _weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                // 공격 대상이 감지되면 공격 수행
                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }

                movementDirection = Vector2.zero; // 공격 시 이동 중지
                return;
            }

            movementDirection = direction; // 타겟을 향해 이동
        }
    }

    // 타겟 방향을 계산하여 반환
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
       
    }
    private Vector2 GetAlternativeDirection(Vector2 originalDirection)
    {
        // 벽을 기준으로 좌우로 회피할 수 있는지 검사
        Vector2 leftDirection = new Vector2(-originalDirection.y, originalDirection.x); // 좌측 90도 회전
        Vector2 rightDirection = new Vector2(originalDirection.y, -originalDirection.x); // 우측 90도 회전
        bool canMoveLeft = !Physics2D.Raycast(transform.position, leftDirection, 1f, 1 << LayerMask.NameToLayer("Level"));
        bool canMoveRight = !Physics2D.Raycast(transform.position, rightDirection, 1f, 1 << LayerMask.NameToLayer("Level"));
        if (canMoveLeft) return leftDirection;
        if (canMoveRight) return rightDirection;
        return Vector2.zero; // 이동할 수 없는 경우 정지
    }

}
