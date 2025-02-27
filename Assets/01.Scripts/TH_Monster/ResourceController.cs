using System;
using UnityEngine;
using Unity.Mathematics;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private BaseController baseController;
    private PlayerStats statHandler;
    private EnemyStats enemyStats;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.MaxHealth; // 항상 최신 상태 반영
    public float enemyCurrentHealth { get; private set; } // 적 현재 체력

    public AudioClip damageClip;
    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        statHandler = GetComponent<PlayerStats>();
        enemyStats = GetComponent<EnemyStats>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    private void Start()
    {
        if (statHandler != null)
        {
            CurrentHealth = statHandler.CurrentHealth;
        }
        else if (enemyStats != null)
        {
            enemyCurrentHealth = enemyStats.MaxHealth;
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] PlayerStats와 EnemyStats 둘 다 없습니다! 올바른 오브젝트인지 확인하세요.");
        }
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change, bool isPlayer)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;

        System.Random random = new System.Random();

        if (isPlayer)
        {
            if (random.NextDouble() <= statHandler.Evasionrate)
            {
                animationHandler.Evasion();
                return false;
            }

            animationHandler.Damage();
            CurrentHealth += change;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            // **PlayerHealth UI 업데이트 호출**
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ForceUpdateHealthBar();
            }

            if (CurrentHealth <= 0f)
            {
                Death();
            }
        }
        else
        {
            if (enemyStats == null)
            {
                Debug.LogWarning("[ResourceController] 적 정보 없음!");
                return false;
            }

            enemyCurrentHealth += change;
            enemyCurrentHealth = Mathf.Clamp(enemyCurrentHealth, 0, enemyStats.MaxHealth);

            if (enemyCurrentHealth <= 0f)
            {
                Death();
            }
        }

        return true;
    }

    public void SetEnemyStats(EnemyStats stats)
    {
        enemyStats = stats;
        enemyCurrentHealth = enemyStats.MaxHealth;
    }

    private void Death()
    {
        baseController.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}
