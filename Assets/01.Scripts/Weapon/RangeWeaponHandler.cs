using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ÿ� ����
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
    public SpriteRenderer _weaponRenderer;



    protected ProjectileManager projectileManager;


    private void Awake()
    {
        _weaponRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    // ������ �����ϴ� �Լ� (�θ� Ŭ������ Attack()�� �������̵�)
    public override void Attack()
    {
        // �θ� Ŭ������ Attack() �Լ� ����
        base.Attack();

        // ����ü ���� ���� ���� ����
        float projectileAngleSpace = multipleProjectileAngle;


        // �� ���� ���ݿ��� �߻��� ����ü ���� ����

        PlayerStats playerProjectile = gameObject.GetComponentInParent<PlayerStats>();
        int numberofProjectilePerShot;
        if (playerProjectile != null)
        {
            numberofProjectilePerShot = numberofProjectilesPerShot + playerProjectile.ExtraProjectiles;
        }
        else
        {
            numberofProjectilePerShot = numberofProjectilesPerShot;
        }

        // ����ü�� ������ �ּ� ���� ��� (�߾��� �������� �л��)
        float minAngle = -(numberofProjectilePerShot / 2) * projectileAngleSpace;

        // ������ ������ŭ ����ü�� ����
        for (int i = 0; i < numberofProjectilePerShot; i++)
        {
            // ���� ����ü�� ���� ���
            float angle = minAngle + projectileAngleSpace * i;

            // ������ ����(Spread) ���� �߰��Ͽ� ����ü�� �ڿ������� �л�ǵ��� ��
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;

            // ����ü ����
            CreateProjectile(Controller.LookDirection.normalized, angle);
        }
    }

    /// <summary>
    /// ����ü�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="_lookDir">����</param>
    /// <param name="angle">����</param>
    protected void CreateProjectile(Vector2 _lookDir, float angle)
    {
        // ����ü �Ŵ����� ���� �Ѿ��� �߻�
        projectileManager.ShootBullet(
            this,  // ���� ���� �ڵ鷯 ����
            projectileSpawnPosition.position,  // ����ü�� ������ ��ġ
            RotateVector2(_lookDir, angle),   // ������ ȸ������ ����
            this.Key,
            this.Bouncing
            );

    }

    /// <summary>
    /// ���͸� Ư�� ������ ȸ����Ű�� �Լ�
    /// </summary>
    /// <param name="v">����</param>
    /// <param name="degree">����</param>
    /// <returns></returns>
    protected static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v; // 2D ���� ȸ�� ����
    }
}