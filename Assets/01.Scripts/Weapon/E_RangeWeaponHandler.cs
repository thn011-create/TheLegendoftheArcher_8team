using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static DesignEnums;

/// <summary>
/// 원거리 무기
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

    // 공격을 실행하는 함수 (부모 클래스의 Attack()을 오버라이드)
    public override void Attack()
    {
        // 부모 클래스의 Attack() 함수 실행
        base.Attack();

        // 투사체 간의 각도 간격 설정
        float projectileAngleSpace = MultipleProjectileAngle;

        // 한 번의 공격에서 발사할 투사체 개수 설정
        int numberofProjectilePerShot = NumberofProjectilesPerShot;

        // 투사체가 퍼지는 최소 각도 계산 (중앙을 기준으로 분산됨)
        float minAngle = -(numberofProjectilePerShot / 2) * projectileAngleSpace;

        // 설정된 개수만큼 투사체를 생성
        for (int i = 0; i < numberofProjectilePerShot; i++)
        {
            // 현재 투사체의 각도 계산
            float angle = minAngle + projectileAngleSpace * i;

            // 랜덤한 퍼짐(Spread) 값을 추가하여 투사체가 자연스럽게 분산되도록 함
            float randomSpread = Random.Range(-Spread, Spread);
            angle += randomSpread;

            // 투사체 생성
            CreateProjectile(Controller.LookDirection.normalized, angle);
        }
    }

    protected override void LoadData(int key)
    {
        var data = dataManager.MonsterWeaponInfoLoader.GetByKey(key);
        Debug.Assert(!(null == data), "키 값을 확인하세요.");
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
