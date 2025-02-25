using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 원거리 무기
/// </summary>
public class RangeWeaponHandler : WeaponHandler
{
    [Header("Range Attack Data")]
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] float bulletSize = 1f;
    [SerializeField] float spread;
    [SerializeField] int numberofProjectilesPerShot;
    [SerializeField] float multipleProjectileAngle;
    [SerializeField] Color projectileColor;

    public float BulletSize { get { return bulletSize; } }
    public float Spread { get { return spread; } }
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }
    public Color ProjectileColor { get { return projectileColor; } }




    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    // 공격을 실행하는 함수 (부모 클래스의 Attack()을 오버라이드)
    public override void Attack()
    {
        // 부모 클래스의 Attack() 함수 실행
        base.Attack();

        // 투사체 간의 각도 간격 설정
        float projectileAngleSpace = multipleProjectileAngle;

        // 한 번의 공격에서 발사할 투사체 개수 설정
        int numberofProjectilePerShot = numberofProjectilesPerShot;

        // 투사체가 퍼지는 최소 각도 계산 (중앙을 기준으로 분산됨)
        float minAngle = -(numberofProjectilePerShot / 2) * projectileAngleSpace;

        // 설정된 개수만큼 투사체를 생성
        for (int i = 0; i < numberofProjectilePerShot; i++)
        {
            // 현재 투사체의 각도 계산
            float angle = minAngle + projectileAngleSpace * i;

            // 랜덤한 퍼짐(Spread) 값을 추가하여 투사체가 자연스럽게 분산되도록 함
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;

            // 투사체 생성
            CreateProjectile(Controller.LookDirection.normalized, angle);
        }
    }

    /// <summary>
    /// 투사체를 생성하는 함수
    /// </summary>
    /// <param name="_lookDir">방향</param>
    /// <param name="angle">각도</param>
    protected void CreateProjectile(Vector2 _lookDir, float angle)
    {
        // 투사체 매니저를 통해 총알을 발사
        projectileManager.ShootBullet(
            this,  // 현재 무기 핸들러 전달
            projectileSpawnPosition.position,  // 투사체가 생성될 위치
            RotateVector2(_lookDir, angle)   // 방향을 회전시켜 적용
            );
    }

    /// <summary>
    /// 벡터를 특정 각도로 회전시키는 함수
    /// </summary>
    /// <param name="v">벡터</param>
    /// <param name="degree">각도</param>
    /// <returns></returns>
    protected static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v; // 2D 벡터 회전 수행
    }
}
