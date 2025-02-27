using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] GameObject projectilePrefab;
           

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
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction, int key, int bouncing)
    {
        // 무기 핸들러의 탄환 인덱스를 사용하여 발사할 탄환 프리팹을 가져옴
        GameObject origin = projectilePrefab;

        // 탄환 오브젝트를 생성 (위치: startPosition, 회전: 없음)
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        // 생성된 탄환의 ProjectileController 컴포넌트를 가져옴
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();

        // 탄환 초기화 (이동 방향 및 무기 핸들러 정보 설정)
        projectileController.Init(direction, rangeWeaponHandler, this, key, bouncing, obj);

        //// bouncing이 남아있다면 재귀 호출
        //if (bouncing > 0)
        //{
        //    // 반사 방향을 결정하는 로직 (임시 예제)
        //    Vector2 newDirection = Vector2.Reflect(direction, Vector2.right); // 예제: 오른쪽 벽에 반사되는 효과

        //    // 재귀 호출 (bouncing 값을 1 감소시킴)
        //    ShootBullet(rangeWeaponHandler, obj.transform.position, newDirection, key, bouncing - 1);
        //}

    }


}

