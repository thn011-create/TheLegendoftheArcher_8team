using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager; // 적 매니저 참조
    private Transform target; // 타겟(플레이어 등) 참조

    [SerializeField] private float followRange = 15f; // 적이 플레이어를 추적하는 범위

    public bool iAttack = false;
    public Animator _animator;

    // 적 초기화 (적 매니저와 타겟 설정)

    public GameObject experiencePrefab; // 경험치 프리팹
    public int experienceAmount = 10; // 몬스터가 주는 경험치 양

    public GameObject damagePopupPrefab; // 데미지 텍스트 프리팹 
    public Transform damagePopupSpawnPoint; // 데미지 텍스트 생성 위치
    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
        if (iAttack)
            _animator = GetComponentInParent<Animator>();

    }
    private void Start()
    {
        if (damagePopupPrefab != null)
        {
            damagePopupPrefab.SetActive(false); //처음에는 비활성화
        }
    }
    // 타겟과의 거리 계산
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    // 적의 행동 처리
    protected override void HandleAction()
    {
        base.HandleAction();

        if (_weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget(); // 타겟과의 거리 측정
        Vector2 direction = DirectionToTarget(); // 타겟 방향 계산

        isAttacking = false;
        if (distance <= followRange) // 타겟이 추적 범위 내에 있는 경우
        {
            lookDirection = direction;
            // 벽 감지 Raycast 추가
            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, 1f, 1 << LayerMask.NameToLayer("Level"));
            if (wallHit.collider != null)
            {
                // 벽을 감지하면 경로 변경
                movementDirection = GetAlternativeDirection(direction);
            }
            else
            {
                movementDirection = direction;
            }

            if (distance <= _weaponHandler.AttackRange) // 타겟이 공격 범위 내에 있는 경우
            {
                int layerMaskTarget = _weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, direction, _weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                // 공격 대상이 감지되면 공격 수행
                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;                    
                }

                movementDirection = Vector2.zero; // 공격 시 이동 중지
                return;
            }

            movementDirection = direction; // 타겟을 향해 이동
        }
    }

    // 타겟 방향을 계산하여 반환
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    public override void Death()
    {
        SpawnExperience(); // 경험치 프리펩 생성
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }
    private Vector2 GetAlternativeDirection(Vector2 originalDirection)
    {
        // 벽을 기준으로 좌우로 회피할 수 있는지 검사
        Vector2 leftDirection = new Vector2(-originalDirection.y, originalDirection.x); // 좌측 90도 회전
        Vector2 rightDirection = new Vector2(originalDirection.y, -originalDirection.x); // 우측 90도 회전
        bool canMoveLeft = !Physics2D.Raycast(transform.position, leftDirection, 1f, 1 << LayerMask.NameToLayer("Level"));
        bool canMoveRight = !Physics2D.Raycast(transform.position, rightDirection, 1f, 1 << LayerMask.NameToLayer("Level"));
        if (canMoveLeft) return leftDirection;
        if (canMoveRight) return rightDirection;
        return Vector2.zero; // 이동할 수 없는 경우 정지
    }

    private void SpawnExperience()
    {
        if (experiencePrefab)
        {
            int expCount = UnityEngine.Random.Range(2, 5); // 2~4개 랜덤 생성

            for (int i = 0; i < expCount; i++)
            {
                Vector2 spawnOffset = UnityEngine.Random.insideUnitCircle * 0.5f; // 약간 퍼지는 효과
                Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;

                GameObject expItem = Instantiate(experiencePrefab, spawnPosition, Quaternion.identity);
                expItem.GetComponent<ExperienceItem>().experienceAmount = experienceAmount;
            }
        }
    }

    public void ShowDamage(float damage)
    {
        Debug.Log($"[ShowDamage] 표시할 데미지: {damage}");

        if (damagePopupPrefab != null && damagePopupSpawnPoint != null)
        {
            GameObject damageTextObj = Instantiate(damagePopupPrefab, damagePopupSpawnPoint.position, Quaternion.identity);
            damageTextObj.SetActive(true); // 🔥 활성화

            TextMeshProUGUI damageText = damageTextObj.GetComponentInChildren<TextMeshProUGUI>();

            if (damageText != null)
            {
                damageText.text = (-damage).ToString("F0"); // 🔥 음수 데미지를 양수로 변환
                Debug.Log($"[ShowDamage] 최종 표시될 텍스트: {damageText.text}");
            }
            else
            {
                Debug.LogError("[ShowDamage] damageText가 할당되지 않음!");
            }

            // 🔥 위로 올라가는 모션 추가
            StartCoroutine(HideDamageText(damageTextObj, 0.5f));
        }
        else
        {
            Debug.LogError("[ShowDamage] damagePopupPrefab 또는 damagePopupSpawnPoint가 없음!");
        }
    }

    // 🔥 데미지 텍스트를 위로 이동시키면서 일정 시간 후 비활성화
    private IEnumerator HideDamageText(GameObject damageTextObj, float delay)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = damageTextObj.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 1f, 0); // 🔥 위로 1 단위 이동

        while (elapsedTime < delay)
        {
            damageTextObj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / delay);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        damageTextObj.SetActive(false); // 일정 시간 후 비활성화
        Destroy(damageTextObj); // 🔥 오브젝트 삭제 (비활성화 대신)
    }

}
