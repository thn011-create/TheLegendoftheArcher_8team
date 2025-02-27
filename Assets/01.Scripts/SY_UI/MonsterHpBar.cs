using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // 몬스터 체력 가져오기
    private ResourceController resourceController;

    [SerializeField] private GameObject hpBarPrefab; // HP 바 프리팹을 할당
    [SerializeField] private Transform canvas; // UI 캔버스 (인스펙터에서 할당)

    private GameObject hpBarInstance;

    // hp바 위치
    private RectTransform hpBarRectTransform;
    private Camera mainCamera;


    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        resourceController = GetComponentInParent<ResourceController>();

        mainCamera = Camera.main;

        if (resourceController != null)
        {
            resourceController.AddHealthChangeEvent(UpdateMonsterHpBar); // 체력 변경 이벤트 연결
            UpdateMonsterHpBar(resourceController.CurrentHealth, resourceController.MaxHealth); // 초기 체력 반영
        }

        CreateHpBar();
    }
    private void CreateHpBar()
    {
        if (hpBarPrefab == null || canvas == null)
        {
            Debug.LogError("MonsterHpBar: HP 바 프리팹 또는 Canvas가 설정되지 않았습니다.");
            return;
        }

        // HP 바 인스턴스 생성
        hpBarInstance = Instantiate(hpBarPrefab, canvas);
        slider = hpBarInstance.GetComponentInChildren<Slider>();
        hpBarRectTransform = hpBarInstance.GetComponent<RectTransform>();


        if (slider == null)
        {
            Debug.LogError(" MonsterHpBar: 생성된 HP 바에서 Slider를 찾을 수 없습니다.");
        }
    }


    private void Update()
    {
        if (hpBarInstance != null && mainCamera != null)
        {
            // 몬스터의 World 좌표를 Screen 좌표로 변환
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
