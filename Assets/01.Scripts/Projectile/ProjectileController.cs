using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    [SerializeField] List<Sprite> images;

    private RangeWeaponHandler rangeWeaponHandler;

    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRanderer;

    public bool fxOnDestroy = true;

    int bouncing;
    int key;
    float rotZ = 0;
    float currentDuration = 0;
    ProjectileManager projectileManager;
    Vector3 reflect_normal;

    GameObject prefab;

    private void Awake()
    {
        currentDuration = 0;
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

        currentDuration += Time.deltaTime;
        //투사체의 최대 지속 시간이 초과되면 파괴
        if (currentDuration > 10f)//.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // 투사체를 지정된 방향으로 이동시킴
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;

        // 투사체 회전
        if (rangeWeaponHandler.Key / 1000 != 5 && rangeWeaponHandler.Key / 1000 != 9)
        {
            rotZ += Time.deltaTime * 1000;
            transform.localEulerAngles = new Vector3(0f, 0f, rotZ);
            return;
        }

        
    }

    /// <summary>
    /// 투사체 초기화 함수
    /// </summary>
    /// <param name="direction">방향</param>
    /// <param name="weaponHandler">무기</param>
    /// <param name="projectileManager">발사체 매니저</param>
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager, int key, int bouncing, GameObject prefab)
    {
        this.projectileManager = projectileManager;

        // 무기 핸들러 정보 저장
        this.rangeWeaponHandler = weaponHandler;

        // 투사체 이동 방향 설정
        this.direction = direction;
        this.bouncing = bouncing;
        this.key = key;
        this.prefab = prefab;

        // 투사체 크기 설정
        transform.localScale = Vector3.one * weaponHandler.BulletSize;

        // 투사체 색상 설정
        spriteRanderer.color = weaponHandler.ProjectileColor;

        // 투사체가 이동하는 방향으로 회전
        transform.right = this.direction;

        // 투사체의 방향에 따라 피벗 회전 조정
        pivot.localEulerAngles += new Vector3(0, 180, 0);

        // Sprite
        string imageName = "fantasy_bullet_";
        spriteRanderer.sprite = FindImage(imageName, rangeWeaponHandler.ImageIdx);

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
        //if (createFx)
        //{
        //    projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);
        //}

        // 현재 오브젝트를 삭제
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 무기 이미지 찾는 함수
    /// </summary>
    /// <param name="name">파일 이름</param>
    /// <param name="idx">이미지 인덱스</param>
    /// <returns></returns>
    protected Sprite FindImage(string name, int idx)
    {
        foreach (Sprite img in images)
        {
            if ($"{name}{idx.ToString()}" == img.name)
            {
                return img;
            }
        }

        return null;
    }

    protected Sprite FindImage(string name)
    {
        foreach (Sprite img in images)
        {
            if (name == img.name)
            {
                Debug.Log(img.name);
                return img;
            }
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //RaycastHit hit;
        //Vector3 dir = (collision.transform.position - transform.position).normalized;

        //// Raycast를 사용하여 충돌 표면의 법선 벡터 찾기
        //if (Physics.Raycast(transform.position, dir, out hit))
        //{
        //    reflect_normal = hit.normal;
        //}

        // 충돌 표면의 법선 가져오기 (2D 전용)
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        if (collision.TryGetComponent(out Rigidbody2D rb) && rb.GetContacts(contacts) > 0)
        {
            reflect_normal = contacts[0].normal; // 첫 번째 충돌 표면의 법선
        }
        else
        {
            reflect_normal = (transform.position - collision.transform.position).normalized; // 대체 법선 (비정상적인 경우)
        }

        Vector3 relect_dir = Vector3.Reflect(this.direction, reflect_normal).normalized;
        Vector3 startPoint = transform.position + relect_dir * .5f;

        // 레벨 충돌 레이어에 닿았는지 확인
        if (levelCollisionLayer.value ==
            (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            if (bouncing == 0)
            {   // 충돌 위치에서 투사체 파괴
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);
            }
            else
            {
                // 충돌 위치에서 투사체 파괴
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);

                projectileManager.ShootBullet(rangeWeaponHandler, startPoint, relect_dir, this.key, bouncing - 1);
            }
        }
        // 공격 대상에 맞았는지 확인
        else if (rangeWeaponHandler.target.value ==
                 (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                // **대상이 플레이어인지 적인지 판별**
                bool isPlayer = collision.GetComponent<PlayerStats>() != null;

                // **체력 변경 (플레이어 또는 적)**
                resourceController.ChangeHealth(-rangeWeaponHandler.Damage, isPlayer);

                // 넉백 처리
                if (rangeWeaponHandler.IsOnKnockback)
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            // 충돌 위치에서 투사체 파괴
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

}
