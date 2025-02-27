using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private PlayerStats statHandler;
    private EnemyStats enemyStats;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.CurrentHealth;
    public float enemyCurrentHealth { get; private set; } //적 현재 체력


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
            Debug.Log($"[Start] Evasionrate: {statHandler.Evasionrate}"); // 추가 디버깅
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

        System.Random random = new System.Random(); // 랜덤 객체 생성

        if (isPlayer)
        {
            // 플레이어 체력 변경 로직
            Debug.Log($"Current Evasion Rate: {statHandler.Evasionrate}");

            if (random.NextDouble() <= statHandler.Evasionrate) // 회피
            {
                Debug.Log(random.NextDouble());
                animationHandler.Evasion();
            }
            else // 데미지
            {
                animationHandler.Damage();
                CurrentHealth += change; // 마이너스이므로 체력 감소
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

            if (CurrentHealth <= 0f)
            {
                Death();
            }
        }
        else
        {
            // 적 체력 변경 로직
            if (enemyStats == null)
            {
                Debug.LogWarning("적 정보가 설정되지 않음!");
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
        enemyCurrentHealth = enemyStats.MaxHealth; // 적의 체력을 초기화
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
