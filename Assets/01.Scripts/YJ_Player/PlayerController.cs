using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Transform target; // ������ Ÿ��(��)
    private float attackCooldown = 1.0f; // ���� ��ٿ�
    private float lastAttackTime = 0.0f; // ������ ���� �ð�

    public Joystick joy;
    private Camera camera; // ī�޶� ���� ����
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }


    protected override void HandleAction()
    {
        // 1. ���̽�ƽ �Է� ó��
        float h = joy.Horizontal; // Input.GetAxisRaw("Horizontal");
        float v = joy.Vertical; // Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(h, v).normalized; ;

        //if (movementDirection.magnitude > 0.1f) // �Է��� ���� �̻��� ���� ���� ����
        //{
        //    lookDirection = movementDirection.normalized; // �̵� �������� �ٶ󺸱�
        //}
        //else
        //{
        //    // 2. ���� ����� �� ã�� (�Է��� ���� ����)
        //    FindNearestTarget();
        //    if (target != null)
        //    {
        //        lookDirection = (target.position - transform.position).normalized; // �� �������� ȸ��
        //    }
        //}

        //// 3. ���� ��ٿ� üũ �� ���� ����
        //if (target != null && Time.time - lastAttackTime >= attackCooldown)
        //{
        //    Attack();
        //    lastAttackTime = Time.time; // ���� �ð� ������Ʈ
        //}
    }


    // ���� ����� �� ã��
    private void FindNearestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // "Enemy" �±׸� ���� ��� �� ã��
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

    protected override void Attack()
    {
        if (lookDirection != Vector2.zero)
        {
            Debug.Log("���� ���� ����!");
            // ���⿡ ���� �߻� ���� �߰� (��: �߻�ü ����)
        }
    }
}
