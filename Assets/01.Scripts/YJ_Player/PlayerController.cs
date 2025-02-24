using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Transform target; // 공격할 타겟(적)
    private float attackCooldown = 1.0f; // 공격 쿨다운
    private float lastAttackTime = 0.0f; // 마지막 공격 시간

    public Joystick joy;
    private Camera camera; // 카메라 변수 선언
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }


    protected override void HandleAction()
    {
        // 1. 조이스틱 입력 처리
        float h = joy.Horizontal; // Input.GetAxisRaw("Horizontal");
        float v = joy.Vertical; // Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(h, v).normalized; ;

        //if (movementDirection.magnitude > 0.1f) // 입력이 일정 이상일 때만 방향 갱신
        //{
        //    lookDirection = movementDirection.normalized; // 이동 방향으로 바라보기
        //}
        //else
        //{
        //    // 2. 가장 가까운 적 찾기 (입력이 없을 때만)
        //    FindNearestTarget();
        //    if (target != null)
        //    {
        //        lookDirection = (target.position - transform.position).normalized; // 적 방향으로 회전
        //    }
        //}

        //// 3. 공격 쿨다운 체크 후 공격 실행
        //if (target != null && Time.time - lastAttackTime >= attackCooldown)
        //{
        //    Attack();
        //    lastAttackTime = Time.time; // 공격 시간 업데이트
        //}
    }


    // 가장 가까운 적 찾기
    private void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // "Enemy" 태그를 가진 모든 적 찾기
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

    protected override void Attack()
    {
        if (lookDirection != Vector2.zero)
        {
            Debug.Log("적을 향해 공격!");
            // 여기에 무기 발사 로직 추가 (예: 발사체 생성)
        }
    }
}
