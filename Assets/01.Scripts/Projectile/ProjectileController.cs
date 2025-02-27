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
        // �ڽ� ������Ʈ���� SpriteRenderer ������Ʈ�� ������
        spriteRanderer = GetComponentInChildren<SpriteRenderer>();

        // Rigidbody2D ������Ʈ�� ������ ���� ó���� ���
        _rigidbody = GetComponent<Rigidbody2D>();

        // ù ��° �ڽ� ������Ʈ�� �ǹ����� ����
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        // ������Ʈ�� �غ���� �ʾҴٸ� �������� ����
        if (!isReady) return;

        currentDuration += Time.deltaTime;
        //����ü�� �ִ� ���� �ð��� �ʰ��Ǹ� �ı�
        if (currentDuration > 10f)//.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // ����ü�� ������ �������� �̵���Ŵ
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;

        // ����ü ȸ��
        if (rangeWeaponHandler.Key / 1000 != 5 && rangeWeaponHandler.Key / 1000 != 9)
        {
            rotZ += Time.deltaTime * 1000;
            transform.localEulerAngles = new Vector3(0f, 0f, rotZ);
            return;
        }


    }

    /// <summary>
    /// ����ü �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="direction">����</param>
    /// <param name="weaponHandler">����</param>
    /// <param name="projectileManager">�߻�ü �Ŵ���</param>
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager, int key, int bouncing, GameObject prefab)
    {
        this.projectileManager = projectileManager;

        // ���� �ڵ鷯 ���� ����
        this.rangeWeaponHandler = weaponHandler;

        // ����ü �̵� ���� ����
        this.direction = direction;
        this.bouncing = bouncing;
        this.key = key;
        this.prefab = prefab;

        // ����ü ũ�� ����
        transform.localScale = Vector3.one * weaponHandler.BulletSize;

        // ����ü ���� ����
        spriteRanderer.color = weaponHandler.ProjectileColor;

        // ����ü�� �̵��ϴ� �������� ȸ��
        transform.right = this.direction;

        // ����ü�� ���⿡ ���� �ǹ� ȸ�� ����
        pivot.localEulerAngles += new Vector3(0, 180, 0);

        // Sprite
        string imageName = "fantasy_bullet_";
        spriteRanderer.sprite = FindImage(imageName, rangeWeaponHandler.ImageIdx);

        // ����ü �غ� �Ϸ�
        isReady = true;
    }

    /// <summary>
    /// ����ü�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="position"></param>
    /// <param name="createFx"></param>
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        //if (createFx)
        //{
        //    projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);
        //}

        // ���� ������Ʈ�� ����
        Destroy(this.gameObject);
    }

    /// <summary>
    /// ���� �̹��� ã�� �Լ�
    /// </summary>
    /// <param name="name">���� �̸�</param>
    /// <param name="idx">�̹��� �ε���</param>
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

        //// Raycast�� ����Ͽ� �浹 ǥ���� ���� ���� ã��
        //if (Physics.Raycast(transform.position, dir, out hit))
        //{
        //    reflect_normal = hit.normal;
        //}

        // �浹 ǥ���� ���� �������� (2D ����)
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        if (collision.TryGetComponent(out Rigidbody2D rb) && rb.GetContacts(contacts) > 0)
        {
            reflect_normal = contacts[0].normal; // ù ��° �浹 ǥ���� ����
        }
        else
        {
            reflect_normal = (transform.position - collision.transform.position).normalized; // ��ü ���� (���������� ���)
        }

        Vector3 relect_dir = Vector3.Reflect(this.direction, reflect_normal).normalized;
        Vector3 startPoint = transform.position + relect_dir * .5f;

        // ���� �浹 ���̾ ��Ҵ��� Ȯ��
        if (levelCollisionLayer.value ==
            (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            // �浹 ��ġ���� ����ü �ı�
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);
            if (bouncing != 0)
            {
                projectileManager.ShootBullet(rangeWeaponHandler, startPoint, relect_dir, this.key, bouncing - 1);
            }
        }
        // ���� ��� �¾Ҵ��� Ȯ��
        else if (rangeWeaponHandler.target.value ==
                 (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                // **����� �÷��̾����� ������ �Ǻ�**
                bool isPlayer = collision.GetComponent<PlayerStats>() != null;

                // **ü�� ���� (�÷��̾� �Ǵ� ��)**
                resourceController.ChangeHealth(-rangeWeaponHandler.Damage, isPlayer);

                // �˹� ó��
                if (rangeWeaponHandler.IsOnKnockback)
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            // �浹 ��ġ���� ����ü �ı�
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

}