using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using static DesignEnums;

public class WeaponHandler : MonoBehaviour
{
    [Header("�̹�����")]
    [SerializeField] protected List<Sprite> images;

    [Header("Key��")]
    [SerializeField] int key;

    [Header("Weapon Info - �ڵ� �Է�")]
    [SerializeField, ReadOnly(false)] int imageIndex;
    [SerializeField, ReadOnly(false)] string itemName;
    [SerializeField, ReadOnly(false)] Grade grade;
    [SerializeField, ReadOnly(false)] bool equip;
    [SerializeField, ReadOnly(false)] float damage;
    [SerializeField, ReadOnly(false)] string description;

    [SerializeField, ReadOnly(false)] float delay;
    [SerializeField, ReadOnly(false)] float speed;
    [SerializeField, ReadOnly(false)] float attackRange;
    [SerializeField, ReadOnly(false)] int bouncing = 0;

    public int Key
    {
        get => key;
        set
        {
            key = value;
            LoadData(Key);
        }
    }

    public int ImageIdx { get => imageIndex; set => imageIndex = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public Grade Grade { get => grade; set => grade = value; }
    public bool Equipe { get => equip; set => equip = value; }
    public float Damage { get => damage; set => damage = value; }
    public string Description { get => description; set => description = value; }
    public float Delay { get => delay; set => delay = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public int Bouncing { get => bouncing; set => bouncing = value; }
    public Sprite _Sprite { get; set; }

    public LayerMask target;

    [Header("Knockback Info - �ڵ� �Է�")]
    [SerializeField, ReadOnly(false)] bool isOnKnockback = false;
    [SerializeField, ReadOnly(false)] float knockbackPower = 0.1f;
    [SerializeField, ReadOnly(false)] float knockbackTime = 0.5f;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    [Header("Flip")]
    [SerializeField] protected Transform flip;

    [Header("AudioClip")]
    public AudioClip attackSoundClip;

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");


    protected Animator animator;
    protected SpriteRenderer weaponRenderer;
    protected DataManager dataManager;
    public BaseController Controller { get; private set; }



    private PlayerStats playerStats;
    private float baseDamage; // ������ �⺻ ������ ����

    public float playerDamage { get; private set; } // ���� ���� ������

    protected virtual void Awake()
    {
        dataManager = DataManager.Instance;
        dataManager.Initialize();
    }

    protected virtual void Start()
    {
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        animator.speed = 1f / delay;


        LoadData(key);
        playerStats = GetComponentInParent<PlayerStats>(); // �θ𿡼� �÷��̾� ã��
        baseDamage = damage; // �⺻ ���� �������� `damage` ������ ����
        UpdateDamage();
    }
    public void UpdateDamage()
    {
        if (playerStats != null)
        {
            Damage = baseDamage + playerStats.AttackDamage; // ���� �⺻ ������ + �÷��̾� ���ݷ�
        }
    }
    protected virtual void LoadData(int key)
    {
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        dataManager = DataManager.Instance;
        dataManager.Initialize();

        var data = dataManager.WeaponInfoLoader.GetByKey(key);
        Debug.Assert(!(null == data), "Ű ���� Ȯ���ϼ���.");

        imageIndex = data.SpriteIndex;
        itemName = data.Name;
        grade = data.Grade;
        equip = data.Equip;
        damage = data.Damage;
        description = data.Description;
        delay = data.Delay;
        speed = data.Speed;
        attackRange = data.AttackRange;
        isOnKnockback = data.isOnKnockback;
        knockbackPower = data.KnockbackPower;
        knockbackTime = data.KnockbackTime;
        bouncing = data.Bouncing;

        string imageName = "fantasy_weapons_pack1_noglow_";
        weaponRenderer.sprite = FindImage(imageName, imageIndex);
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
            //Debug.Log(img.name);
            if ($"{name}{idx.ToString()}" == img.name)
            {
                return img;
            }
        }

        return null;
    }

    public virtual void Attack()
    {
        AttackAnimation();
        if (attackSoundClip != null)
            SoundManager.PlayClip(attackSoundClip);
    }

    public virtual void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isFlip)
    {
        weaponRenderer.flipY = isFlip;

        float scaleX = isFlip ? -1f : 1f;
        flip.localScale = new Vector3(scaleX, 1f, 1f);
    }

    public void GetImage(int key)
    {

    }

}