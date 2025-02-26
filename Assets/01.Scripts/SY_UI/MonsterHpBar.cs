using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // ���� ü�� ��������
    private ResourceController resourceController;

    [SerializeField] private GameObject hpBarPrefab; // HP �� �������� �Ҵ�
    [SerializeField] private Transform canvas; // UI ĵ���� (�ν����Ϳ��� �Ҵ�)

    private GameObject hpBarInstance;

    // hp�� ��ġ
    private RectTransform hpBarRectTransform;
    private Camera mainCamera;


    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        resourceController = GetComponentInParent<ResourceController>();

        mainCamera = Camera.main;

        if (resourceController != null)
        {
            resourceController.AddHealthChangeEvent(UpdateMonsterHpBar); // ü�� ���� �̺�Ʈ ����
            UpdateMonsterHpBar(resourceController.CurrentHealth, resourceController.MaxHealth); // �ʱ� ü�� �ݿ�
        }

        CreateHpBar();
    }
    private void CreateHpBar()
    {
        if (hpBarPrefab == null || canvas == null)
        {
            Debug.LogError("MonsterHpBar: HP �� ������ �Ǵ� Canvas�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // HP �� �ν��Ͻ� ����
        hpBarInstance = Instantiate(hpBarPrefab, canvas);
        slider = hpBarInstance.GetComponentInChildren<Slider>();
        hpBarRectTransform = hpBarInstance.GetComponent<RectTransform>();


        if (slider == null)
        {
            Debug.LogError(" MonsterHpBar: ������ HP �ٿ��� Slider�� ã�� �� �����ϴ�.");
        }
    }


    private void Update()
    {
        if (hpBarInstance != null && mainCamera != null)
        {
            // ������ World ��ǥ�� Screen ��ǥ�� ��ȯ
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
            hpBarRectTransform.position = screenPosition;
        }
    }

    private void UpdateMonsterHpBar(float currentHealth, float maxHealth)
    {
        if (slider == null)
        {
            return;
        }

        slider.value = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            if (resourceController != null)
            {
                resourceController.RemoveHealthChangeEvent(UpdateMonsterHpBar);
            }
            if (hpBarInstance != null)
            {
                Destroy(hpBarInstance);
            }

        }
    }

    //void OnDestroy()
    //{
        
    //    if (resourceController != null)
    //    {
    //        resourceController.RemoveHealthChangeEvent(UpdateMonsterHpBar);
    //    }

    //    if (hpBarInstance != null)
    //    {
    //        Destroy(hpBarInstance);
    //    }
    //}


}
