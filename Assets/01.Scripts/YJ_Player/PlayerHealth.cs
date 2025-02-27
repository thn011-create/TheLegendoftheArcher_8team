using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image imgbar;
    private ResourceController resourceController;
    private float lastHealth = -1f; // 초기값 -1로 설정해서 처음에는 무조건 업데이트

    void Start()
    {
        resourceController = GetComponent<ResourceController>();

        if (resourceController == null)
        {
            Debug.LogError("[PlayerHealth] ResourceController가 없습니다! HP 바를 업데이트할 수 없습니다.");
            return;
        }

        if (imgbar == null)
        {
            Debug.LogError("[PlayerHealth] imgbar가 설정되지 않았습니다!");
            return;
        }

        UpdateHealthBar();
    }

    void Update()
    {
        if (resourceController == null || imgbar == null) return;

        // 체력이 변경되었을 때만 업데이트
        if (lastHealth != resourceController.CurrentHealth)
        {
            UpdateHealthBar();
            lastHealth = resourceController.CurrentHealth;
        }
    }

    public void ForceUpdateHealthBar()
    {
        if (resourceController == null || imgbar == null) return;

        Debug.Log("[PlayerHealth] 강제 HP 바 업데이트 호출됨!");
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float newScaleX = resourceController.CurrentHealth / resourceController.MaxHealth;
        Vector3 newScale = imgbar.rectTransform.localScale;
        newScale.x = newScaleX; // X축 크기만 조정
        imgbar.rectTransform.localScale = newScale;
    }
}
