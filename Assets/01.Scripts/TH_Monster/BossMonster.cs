using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    [Header("Boss Settings")]
    public float maxHealth = 100f; // 보스의 최대 체력
    private float currentHealth; // 보스의 현재 체력
    public float attackRange = 5f; // 공격 범위
    public float attackCooldown = 1f; // 공격 쿨타임
    
    [Header("Boss Drops")]
    public GameObject healthPotionPrefab; // 체력 회복 아이템 프리팹
    public GameObject experienceOrbPrefab; // 경험치 아이템 프리팹

    private enum AttackPattern
    {
        Melee,
        Ranged,
        Special
    }

    private AttackPattern currentAttackPattern; // 현재 공격 패턴

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth; // 보스 초기화
        timeSinceLastAttack = attackCooldown;
    }

    protected override void Update()
    {
        base.Update();

        // 공격 쿨타임 관리
        timeSinceLastAttack += Time.deltaTime;

        // 보스의 체력 상태에 따른 행동
        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            //HandleBossAI(); // 보스의 AI 처리
        }
    }


    protected override void HandleAction()
    {
        // 공격 패턴 선택 및 공격 처리
        if (timeSinceLastAttack >= attackCooldown)
        {
            SelectAttackPattern();
            Attack();
        }
    }

    protected override void Attack()
    {
        switch (currentAttackPattern)
        {
            case AttackPattern.Melee:
                MeleeAttack();
                break;

            case AttackPattern.Ranged:
                RangedAttack();
                break;

            case AttackPattern.Special:
                SpecialAttack();
                break;
        }
        timeSinceLastAttack = 0; // 공격 후 쿨타임 초기화
    }

    private void MeleeAttack()
    {
        // 근접 공격 처리
        // 예: 근처의 플레이어에게 데미지 주기
        Debug.Log("Boss performs melee attack!");
        // 플레이어에게 데미지 주는 코드 추가
    }

    private void RangedAttack()
    {
        // 원거리 공격 처리
        // 예: 플레이어에게 투사체 발사
        Debug.Log("Boss performs ranged attack!");
        // 투사체 발사 코드 추가
    }

    private void SpecialAttack()
    {
        // 특수 공격 처리
        // 예: 넓은 범위에 공격하거나, 강력한 공격
        Debug.Log("Boss performs special attack!");
        // 특수 공격 코드 추가
    }

    private void SelectAttackPattern()
    {
        // 간단히 공격 패턴을 랜덤으로 선택하거나 보스의 상태에 따라 선택할 수 있음
        currentAttackPattern = (AttackPattern)Random.Range(0, 3);
    }

    // 보스가 죽었을 때 처리
    public override void Death()
    {
        base.Death();

        // 보스 죽음 후 아이템 드롭
        DropItems();

        // 경험치 아이템 드롭
        DropExperienceOrb();

        // 보스 사망 처리 (보스 게임 오브젝트 파괴)
        Destroy(gameObject, 2f);
    }

    private void DropItems()
    {
        // 체력 회복 아이템 드롭
        if (healthPotionPrefab != null)
        {
            Instantiate(healthPotionPrefab, transform.position, Quaternion.identity);
        }
    }

    private void DropExperienceOrb()
    {
        // 경험치 아이템 드롭
        if (experienceOrbPrefab != null)
        {
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
        }
    }

    // 체력 관리 함수
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }
}

