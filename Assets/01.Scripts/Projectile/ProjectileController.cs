using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRanderer;

    public bool fxOnDestroy = true;

    ProjectileManager projectileManager;

    private void Awake()
    {
        // 자식 오브젝트에서 SpriteRenderer 컴포넌트를 가져옴
        spriteRanderer = GetComponentInChildren<SpriteRenderer>();

        // Rigidbody2D 컴포넌트를 가져와 물리 처리를 담당
        _rigidbody = GetComponent<Rigidbody2D>();

        // 첫 번째 자식 오브젝트를 피벗으로 설정
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        // 오브젝트가 준비되지 않았다면 실행하지 않음
        if (!isReady) return;

        // 현재 지속 시간을 증가시킴
        currentDuration += Time.deltaTime;

        // 투사체의 최대 지속 시간이 초과되면 파괴
        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // 투사체를 지정된 방향으로 이동시킴
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    /// <summary>
    /// 투사체 초기화 함수
    /// </summary>
    /// <param name="direction">방향</param>
    /// <param name="weaponHandler">무기</param>
    /// <param name="projectileManager">발사체 매니저</param>
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;

        // 무기 핸들러 정보 저장
        rangeWeaponHandler = weaponHandler;

        // 투사체 이동 방향 설정
        this.direction = direction;

        // 지속 시간 초기화
        currentDuration = 0;

        // 투사체 크기 설정
        transform.localScale = Vector3.one * weaponHandler.BulletSize;

        // 투사체 색상 설정
        spriteRanderer.color = weaponHandler.ProjectileColor;

        // 투사체가 이동하는 방향으로 회전
        transform.right = this.direction;

        // 투사체의 방향에 따라 피벗 회전 조정
        if (direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        // 투사체 준비 완료
        isReady = true;
    }

    /// <summary>
    /// 투사체를 제거하는 함수
    /// </summary>
    /// <param name="position"></param>
    /// <param name="createFx"></param>
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);
        }

        // 현재 오브젝트를 삭제
        Destroy(this.gameObject);
    }
}
