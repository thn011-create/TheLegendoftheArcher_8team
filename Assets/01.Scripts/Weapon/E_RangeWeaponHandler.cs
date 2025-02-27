using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static DesignEnums;

/// <summary>
/// ���Ÿ� ����
/// </summary>
public class E_RangeWeaponHandler : RangeWeaponHandler
{

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
        dataManager = DataManager.Instance;
        dataManager.Initialize();
    }

    // ������ �����ϴ� �Լ� (�θ� Ŭ������ Attack()�� �������̵�)
    public override void Attack()
    {
        // �θ� Ŭ������ Attack() �Լ� ����
        base.Attack();

        // ����ü ���� ���� ���� ����
        float projectileAngleSpace = MultipleProjectileAngle;

        // �� ���� ���ݿ��� �߻��� ����ü ���� ����
        int numberofProjectilePerShot = NumberofProjectilesPerShot;

        // ����ü�� ������ �ּ� ���� ��� (�߾��� �������� �л��)
        float minAngle = -(numberofProjectilePerShot / 2) * projectileAngleSpace;

        // ������ ������ŭ ����ü�� ����
        for (int i = 0; i < numberofProjectilePerShot; i++)
        {
            // ���� ����ü�� ���� ���
            float angle = minAngle + projectileAngleSpace * i;

            // ������ ����(Spread) ���� �߰��Ͽ� ����ü�� �ڿ������� �л�ǵ��� ��
            float randomSpread = Random.Range(-Spread, Spread);
            angle += randomSpread;

            // ����ü ����
            CreateProjectile(Controller.LookDirection.normalized, angle);
        }
    }

    protected override void LoadData(int key)
    {
        var data = dataManager.MonsterWeaponInfoLoader.GetByKey(key);
        Debug.Assert(!(null == data), "Ű ���� Ȯ���ϼ���.");
        ItemName = data.Name;
        Damage = data.Damage;

        Delay = data.Delay;
        Speed = data.Speed;
        AttackRange = data.AttackRange;
        IsOnKnockback = data.isOnKnockback;
        KnockbackPower = data.KnockbackPower;
        KnockbackTime = data.KnockbackTime;

        string imageName = data.SpriteName;

        weaponRenderer.sprite = FindImage("null_weapone");
    }

    protected Sprite FindImage(string name)
    {
        foreach (Sprite img in images)
        {
            //Debug.Log(img.name);
            if (name == img.name)
            {
                return img;
            }
        }
        return null;
    }

}