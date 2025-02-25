using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Transform target; // ������ Ÿ��(��)
    private float attackCooldown = 1.0f; // ���� ��ٿ�
    private float lastAttackTime = 0.0f; // ������ ���� �ð�

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
        // 1. ���̽�ƽ �Է� ó��
        float h = joy.Horizontal;
        float v = joy.Vertical;
        movementDirection = new Vector2(h, v).normalized;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            statHandler.GainExperience(90);
            
        }
        if (movementDirection.magnitude > 0.1f)
        {
            // ���̽�ƽ ������ �ٶ�
            lookDirection = movementDirection;
            isAttacking = false; // �̵� �߿��� �������� ����
        }
        else
        {
            // ���̽�ƽ �Է��� ������ ���� ����� ���� ã�� �� ������ �ٶ�
            FindNearestTarget();
            if (target != null)
            {
                lookDirection = (target.position - transform.position).normalized;
                isAttacking = true;  // ���� ���� ���� ����
            }
            else
            {
                isAttacking = false; // ���� ������ �������� ����
            }
        }
    }


    // ���� ����� �� ã��
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

        target = nearestEnemy; // ���� ����� ���� Ÿ������ ����
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
