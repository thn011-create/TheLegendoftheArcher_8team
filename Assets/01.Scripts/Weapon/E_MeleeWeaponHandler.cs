using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E_MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;
    EnemyController enemy;

    protected override void Start()
    {
        base.Start();
        enemy = GetComponentInParent<EnemyController>();
    }

    public override void Attack()
    {
        base.Attack();

        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
            collideBoxSize, 0, Vector2.zero, 0, target);

        if (hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                // **����� �÷��̾����� ������ �Ǻ�**
                bool isPlayer = hit.collider.GetComponent<PlayerStats>() != null;

                // **ü�� ���� (�÷��̾� �Ǵ� ��)**
                resourceController.ChangeHealth(-Damage, isPlayer);

                // �ִϸ��̼� & �˹� ó��
                if (IsOnKnockback)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        enemy._animator.SetTrigger("IsAttack");
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }
    }


    public override void Rotate(bool isLeft)
    {
        if (isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }


    protected override void LoadData(int key)
    {
        var data = dataManager.WeaponInfoLoader.GetByKey(key);
        Debug.Assert(!(null == data), "Ű ���� Ȯ���ϼ���.");
        ItemName = data.Name;
        Damage = data.Damage;

        Delay = data.Delay;
        Speed = data.Speed;
        AttackRange = data.AttackRange;
        IsOnKnockback = data.isOnKnockback;
        KnockbackPower = data.KnockbackPower;
        KnockbackTime = data.KnockbackTime;
    }
}