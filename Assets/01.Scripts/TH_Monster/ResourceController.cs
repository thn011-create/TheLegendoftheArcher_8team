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
    public float enemyCurrentHealth { get; private set; } //�� ���� ü��


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
            Debug.Log($"[Start] Evasionrate: {statHandler.Evasionrate}"); // �߰� �����
        }
        else if (enemyStats != null)
        {
            enemyCurrentHealth = enemyStats.MaxHealth;
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] PlayerStats�� EnemyStats �� �� �����ϴ�! �ùٸ� ������Ʈ���� Ȯ���ϼ���.");
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

        System.Random random = new System.Random(); // ���� ��ü ����

        if (isPlayer)
        {
            // �÷��̾� ü�� ���� ����
            Debug.Log($"Current Evasion Rate: {statHandler.Evasionrate}");

            if (random.NextDouble() <= statHandler.Evasionrate) // ȸ��
            {
                Debug.Log(random.NextDouble());
                animationHandler.Evasion();
            }
            else // ������
            {
                animationHandler.Damage();
                CurrentHealth += change; // ���̳ʽ��̹Ƿ� ü�� ����
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
            // �� ü�� ���� ����
            if (enemyStats == null)
            {
                Debug.LogWarning("�� ������ �������� ����!");
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
        enemyCurrentHealth = enemyStats.MaxHealth; // ���� ü���� �ʱ�ȭ
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
