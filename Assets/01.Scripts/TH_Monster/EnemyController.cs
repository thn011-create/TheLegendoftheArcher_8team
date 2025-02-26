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

        // 타겟이 null인 경우 이동 방향을 초기화하고 종료
        if (target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        // 타겟과의 거리 및 방향 계산
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        // 공격 상태 초기화
        isAttacking = false;

        // 타겟이 추적 범위(followRange) 내에 있는 경우
        if (distance <= followRange)
        {
            lookDirection = direction; // 타겟을 바라보기

            // 타겟이 공격 범위(attackRange) 내에 있는 경우
            if (distance <= attackRange)
            {
                // 레이어 마스크 설정 (예: "Player" 레이어만 감지)
                int layerMaskTarget = 1 << LayerMask.NameToLayer("Player");

                // 레이캐스트를 사용해 타겟 감지
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, // 시작 위치
                    direction, // 방향
                    attackRange * 1.5f, // 거리 (공격 범위의 1.5배)
                    layerMaskTarget // 대상 레이어 마스크
                );

                // 레이캐스트 디버깅 (시각화)
                Debug.DrawRay(transform.position, direction * attackRange * 1.5f, Color.red);

                // 레이캐스트가 타겟을 감지한 경우
                if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    isAttacking = true; // 공격 상태로 변경
                    Attack();
                    MoveMent(movementDirection); // 공격 중 이동 (필요한 경우)
                }

                // 공격 중에는 이동 방향 초기화
                movementDirection = Vector2.zero;
                return;
            }

            // 타겟이 추적 범위 내에 있지만 공격 범위 밖인 경우, 타겟을 향해 이동
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

