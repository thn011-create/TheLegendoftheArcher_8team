using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static DesignEnums;

public class WeaponHandler : MonoBehaviour
{
    [Header("이미지들")]
    [SerializeField] List<Sprite> images;

    [Header("Key값")]
    [SerializeField] int key;

    [Header("Weapon Info")]

    [SerializeField] int imageIndex;
    [SerializeField] string itemName;
    [SerializeField] Grade grade;
    [SerializeField] float damage;
    [SerializeField] string description;

    [SerializeField] float delay;
    [SerializeField] float speed;
    [SerializeField] float attackRange;
    
    public int Key { get => key; set => key = value; }
    public int ImageIdx { get => imageIndex; set => imageIndex = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public Grade Grade { get => grade; set => grade = value; }
    public float Damage { get => damage; set => damage = value; }
    public string Description { get => description; set => description = value; }
    public float Delay { get => delay; set => delay = value; }
    public float Speed { get => speed; set => speed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    [SerializeField] private float knockbackPower = 0.1f;
    [SerializeField] private float knockbackTime = 0.5f;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    [Header("Flip")]
    [SerializeField] private Transform flip;

    [Header("AudioClip")]
    public AudioClip attackSoundClip;

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");


    Animator animator;
    SpriteRenderer weaponRenderer;
    DataManager dataManager;

    protected virtual void Awake()
    {
        dataManager.Initialize();
    }

    protected virtual void Start()
    {
        //Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        animator.speed = 1f / delay;
        

        LoadData(1000);

    }

    protected void LoadData(int key)
    {

        var data = dataManager.WeaponInfoLoader.GetByKey(key);
        //imageIndex = data.index;
        itemName = data.Name;
        grade = data.Grade;
        description = data.Description;
        //delay = 
        //weaponSize = 
        speed = data.Speed;
        //attackRange = 
        //isOnKnockback = data.;
        //knockbackPower = data;
        //knockbackTime = data;
    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public virtual void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isFlip)
    {
        //weaponRenderer.flipY = isFlip;

        float scaleX = isFlip ? -1f : 1f;
        flip.localScale = new Vector3(scaleX, 1f, 1f);
    }

}
