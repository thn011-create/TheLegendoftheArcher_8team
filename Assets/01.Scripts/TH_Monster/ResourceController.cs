using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private PlayerStats PlayerStats;
    private EnemyStats EnemyStats;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => PlayerStats != null ? PlayerStats.CurrentHealth :
                          EnemyStats != null ? EnemyStats.CurrentHealth : 0;

    public AudioClip damageClip;

    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        PlayerStats = GetComponent<PlayerStats>();
        EnemyStats = GetComponent<EnemyStats>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    private void Start()
    {
        if (PlayerStats != null)  // 플레이어일 경우
        {
            CurrentHealth = PlayerStats.CurrentHealth;
        }
        else if (EnemyStats != null)  // 적일 경우
        {
            CurrentHealth = EnemyStats.CurrentHealth;
        }
        else
        {
            Debug.LogError($"{gameObject.name}에 PlayerStats 또는 EnemyStats가 없습니다!");
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

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();

            //if (damageClip)
            //    SoundManager.PlayClip(damageClip);
        }

        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
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
