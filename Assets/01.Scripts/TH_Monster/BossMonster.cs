using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    [Header("Boss Settings")]
    public float maxHealth = 100f; // ������ �ִ� ü��
    private float currentHealth; // ������ ���� ü��
    public float attackRange = 5f; // ���� ����
    public float attackCooldown = 1f; // ���� ��Ÿ��
    
    [Header("Boss Drops")]
    public GameObject healthPotionPrefab; // ü�� ȸ�� ������ ������
    public GameObject experienceOrbPrefab; // ����ġ ������ ������

    private enum AttackPattern
    {
        Melee,
        Ranged,
        Special
    }

    private AttackPattern currentAttackPattern; // ���� ���� ����

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth; // ���� �ʱ�ȭ
        timeSinceLastAttack = attackCooldown;
    }

    protected override void Update()
    {
        base.Update();

        // ���� ��Ÿ�� ����
        timeSinceLastAttack += Time.deltaTime;

        // ������ ü�� ���¿� ���� �ൿ
        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            //HandleBossAI(); // ������ AI ó��
        }
    }


    protected override void HandleAction()
    {
        // ���� ���� ���� �� ���� ó��
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
        timeSinceLastAttack = 0; // ���� �� ��Ÿ�� �ʱ�ȭ
    }

    private void MeleeAttack()
    {
        // ���� ���� ó��
        // ��: ��ó�� �÷��̾�� ������ �ֱ�
        Debug.Log("Boss performs melee attack!");
        // �÷��̾�� ������ �ִ� �ڵ� �߰�
    }

    private void RangedAttack()
    {
        // ���Ÿ� ���� ó��
        // ��: �÷��̾�� ����ü �߻�
        Debug.Log("Boss performs ranged attack!");
        // ����ü �߻� �ڵ� �߰�
    }

    private void SpecialAttack()
    {
        // Ư�� ���� ó��
        // ��: ���� ������ �����ϰų�, ������ ����
        Debug.Log("Boss performs special attack!");
        // Ư�� ���� �ڵ� �߰�
    }

    private void SelectAttackPattern()
    {
        // ������ ���� ������ �������� �����ϰų� ������ ���¿� ���� ������ �� ����
        currentAttackPattern = (AttackPattern)Random.Range(0, 3);
    }

    // ������ �׾��� �� ó��
    public override void Death()
    {
        base.Death();

        // ���� ���� �� ������ ���
        DropItems();

        // ����ġ ������ ���
        DropExperienceOrb();

        // ���� ��� ó�� (���� ���� ������Ʈ �ı�)
        Destroy(gameObject, 2f);
    }

    private void DropItems()
    {
        // ü�� ȸ�� ������ ���
        if (healthPotionPrefab != null)
        {
            Instantiate(healthPotionPrefab, transform.position, Quaternion.identity);
        }
    }

    private void DropExperienceOrb()
    {
        // ����ġ ������ ���
        if (experienceOrbPrefab != null)
        {
            Instantiate(experienceOrbPrefab, transform.position, Quaternion.identity);
        }
    }

    // ü�� ���� �Լ�
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }
}

