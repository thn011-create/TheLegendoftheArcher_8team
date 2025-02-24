using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    [SerializeField] private ParticleSystem impactParticleSyetem;

    private void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// 총알을 발사하는 함수
    /// </summary>
    /// <param name="rangeWeaponHandler">무기</param>
    /// <param name="startPosition">시작 위치</param>
    /// <param name="direction">방향</param>
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        // 무기 핸들러의 탄환 인덱스를 사용하여 발사할 탄환 프리팹을 가져옴
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];

        // 탄환 오브젝트를 생성 (위치: startPosition, 회전: 없음)
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        // 생성된 탄환의 ProjectileController 컴포넌트를 가져옴
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();

        // 탄환 초기화 (이동 방향 및 무기 핸들러 정보 설정)
        projectileController.Init(direction, rangeWeaponHandler, this);
    }

    /// <summary>
    /// 파티클 발생 함수
    /// </summary>
    /// <param name="position">위치</param>
    /// <param name="weaponHandler">무기</param>
    public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        // 충돌 위치로 파티클 시스템 이동
        impactParticleSyetem.transform.position = position;

        // 파티클 시스템의 방출 모듈 가져오기
        ParticleSystem.EmissionModule em = impactParticleSyetem.emission;

        // 무기의 탄환 크기에 따라 파티클 방출량 설정
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));

        // 파티클 시스템의 메인 모듈 가져오기
        ParticleSystem.MainModule mainModule = impactParticleSyetem.main;

        // 탄환 크기에 따라 파티클의 속도 조정
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;

        // 파티클 시스템 실행
        impactParticleSyetem.Play();
    }
}
