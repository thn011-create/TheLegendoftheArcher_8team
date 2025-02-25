using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Transform target; // 공격할 타겟(적)
    private float attackCooldown = 1.0f; // 공격 쿨다운
    private float lastAttackTime = 0.0f; // 마지막 공격 시간

    //public LayerMask Etarget;
    public Joystick joy;
    private Camera camera;
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        // 1. 조이스틱 입력 처리
        float h = joy.Horizontal;
        float v = joy.Vertical;
        movementDirection = new Vector2(h, v).normalized;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            statHandler.GainExperience(90);
            
        }
        if (movementDirection.magnitude > 0.1f)
        {
            // 조이스틱 방향을 바라봄
            lookDirection = movementDirection;
            isAttacking = false; // 이동 중에는 공격하지 않음
        }
        else
        {
            // 조이스틱 입력이 없으면 가장 가까운 적을 찾고 그 방향을 바라봄
            FindNearestTarget();
            if (target != null)
            {
                lookDirection = (target.position - transform.position).normalized;
                isAttacking = true;  // 적이 있을 때만 공격
            }
            else
            {
                isAttacking = false; // 적이 없으면 공격하지 않음
            }
        }
    }


    // 가장 가까운 적 찾기
    private void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            target = null;
            return;
        }
        float minDistance = float.MaxValue;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        target = nearestEnemy; // 가장 가까운 적을 타겟으로 설정
    }

    public override void HandleAttackDelay()
    {
        PlayerStats player = GetComponent<PlayerStats>();

        if (_weaponHandler == null)
            return;
        if (timeSinceLastAttack <= _weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (isAttacking && timeSinceLastAttack > (1f / (_weaponHandler.Delay * player.AttackSpeed)))
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }
}
