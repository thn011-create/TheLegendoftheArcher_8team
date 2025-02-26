using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // ���� ������ ���� Rigidbody2D ������Ʈ

    [SerializeField] private SpriteRenderer chracterRendere; // ĳ������ ��������Ʈ ������
    [SerializeField] private Transform weaponPivot; // ������ ȸ�� �߽��� �Ǵ� Transform

    protected Vector2 movementDirection = Vector2.zero; // �̵� ����
    public Vector2 MoveMentDirection { get { return movementDirection; } } // �̵� ���� ��ȯ (Getter)

    protected Vector2 lookDirection = Vector2.zero; // �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } } // �ٶ󺸴� ���� ��ȯ (Getter)

    public Vector2 knockback = Vector2.zero; // �˹� ����
    public float knockbackDuration = 0.0f; // �˹� ���� �ð�

    protected AnimationHandler animationHandler;
    protected PlayerStats statHandler;
    protected EnemyStats enemyStats;

    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler _weaponHandler;

    protected bool isAttacking;
    public float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();

        if (WeaponPrefab != null)
        {
            _weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else
        {
            _weaponHandler = GetComponentInChildren<WeaponHandler>();
        }
    }


    protected virtual void Start()
    {
        // Start()�� �ڽ� Ŭ�������� �������̵� �����ϵ��� �����
        
    }

    protected virtual void Update()
    {
        HandleAction(); // ����� �Է��� ó���ϴ� �Լ� (�ڽ� Ŭ�������� ����)
        Rotate(lookDirection); // ĳ���Ͱ� �ٶ󺸴� ������ ȸ��
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        // ���̽�ƽ�� ������ ���� �̵� ó��
        if (movementDirection != Vector2.zero)
        {
            MoveMent(movementDirection);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero; // ���̽�ƽ �Է� ���� �� ����
        }

        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {
        // ����� �Է��� ó���ϴ� �Լ� (�ڽ� Ŭ�������� ����)
    }

    // �̵� ó�� �Լ�
    protected virtual void MoveMent(Vector2 direction)
    {
        float moveSpeed = 1f; // �⺻ �ӵ� (Ȥ�ö� �� �� null�̸� �⺻�� ���)

        if (statHandler != null)
        {
            moveSpeed = statHandler.MoveSpeed; // �÷��̾��� ��� �ӵ� ����
        }
        else if (enemyStats != null)
        {
            moveSpeed = enemyStats.MoveSpeed; // ���� ��� �ӵ� ����
        }

        direction = direction * moveSpeed; // �̵� �ӵ� �ݿ�

        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f; // �˹� ���� �� �̵� �ӵ� ����
            direction += knockback;
        }

        _rigidbody.velocity = direction; // Rigidbody2D�� ����
        animationHandler?.Move(direction);
    }


    // ĳ���� ȸ�� ó�� �Լ�
    private void Rotate(Vector2 direction)
    {
        // ���� ���͸� ������ ��ȯ (���� -> ��)
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ĳ���Ͱ� ������ �ٶ󺸴��� �Ǵ� (90�� �̻��̸� ����)
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        // ��������Ʈ �¿� ���� ó��
        chracterRendere.flipX = isLeft;

        // ������ ȸ�� ����
        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        _weaponHandler?.Rotate(isLeft);
    }

    // �˹� ���� �Լ�
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration; // �˹� ���� �ð� ����

        // ��� ��ġ���� �ڽ��� ��ġ�� ���� �˹� ������ ����� �� ����ȭ
        knockback = -(other.position - transform.position).normalized * power;
    }

    public virtual void HandleAttackDelay()
    {

        if (_weaponHandler == null)
            return;
        if (timeSinceLastAttack <= (1f / _weaponHandler.Delay))
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (isAttacking && timeSinceLastAttack > (1f/_weaponHandler.Delay))
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            _weaponHandler?.Attack();
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
        //animationHandler.Die();
        Destroy(gameObject, 2f);
    }

}
