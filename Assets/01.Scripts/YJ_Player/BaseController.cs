using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 물리 연산을 위한 Rigidbody2D 컴포넌트

    [SerializeField] private SpriteRenderer chracterRendere; // 캐릭터의 스프라이트 렌더러
    [SerializeField] private Transform weaponPivot; // 무기의 회전 중심이 되는 Transform

    protected Vector2 movementDirection = Vector2.zero; // 이동 방향
    public Vector2 MoveMentDirection { get { return movementDirection; } } // 이동 방향 반환 (Getter)

    protected Vector2 lookDirection = Vector2.zero; // 바라보는 방향
    public Vector2 LookDirection { get { return lookDirection; } } // 바라보는 방향 반환 (Getter)

    private Vector2 knockback = Vector2.zero; // 넉백 벡터
    private float knockbackDuration = 0.0f; // 넉백 지속 시간

    protected AnimationHandler animationHandler;
    protected PlayerStats statHandler;

    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<PlayerStats>();
        if (WeaponPrefab != null)
        {
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>();
        }
    }

    protected virtual void Start()
    {
        // Start()는 자식 클래스에서 오버라이드 가능하도록 비워둠
    }

    protected virtual void Update()
    {
        HandleAction(); // 사용자 입력을 처리하는 함수 (자식 클래스에서 구현)
        Rotate(lookDirection); // 캐릭터가 바라보는 방향을 회전
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate() // 물리 연산이 필요할 때 일정한 간격으로 호출
    {
        
        MoveMent(movementDirection); // 이동 처리

        // 넉백이 지속 중이면 시간을 줄임
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {
        // 사용자 입력을 처리하는 함수 (자식 클래스에서 구현)
    }

    // 이동 처리 함수
    private void MoveMent(Vector2 direction)
    {

        direction = direction * statHandler.MoveSpeed; // 기본 이동 속도 적용
        Debug.Log($"Applying Velocity: {direction}");
        // 넉백 지속 중이면 이동 속도를 줄이고 넉백 벡터를 추가
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // 이동 속도를 20%로 줄임
            direction += knockback; // 넉백 반영
        }

        _rigidbody.velocity = direction; // Rigidbody2D에 적용
        animationHandler.Move(direction);
    }

    // 캐릭터 회전 처리 함수
    private void Rotate(Vector2 direction)
    {
        // 방향 벡터를 각도로 변환 (라디안 -> 도)
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 캐릭터가 왼쪽을 바라보는지 판단 (90도 이상이면 좌측)
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // 스프라이트 좌우 반전 처리
        chracterRendere.flipX = isLeft;

        // 무기의 회전 적용
        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        weaponHandler?.Rotate(isLeft);
    }

    // 넉백 적용 함수
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration; // 넉백 지속 시간 설정

        // 상대 위치에서 자신의 위치를 빼서 넉백 방향을 계산한 후 정규화
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {

        if (weaponHandler == null)
            return;
        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Debug.Log("버튼누름");
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour componet in transform.GetComponentsInChildren<Behaviour>())
        {
            componet.enabled = false;
        }

        Destroy(gameObject, 2f);
    }

}
