using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image imgbar;
    private ResourceController resourceController;
    private float lastHealth = -1f; // �ʱⰪ -1�� �����ؼ� ó������ ������ ������Ʈ

    void Start()
    {
        resourceController = GetComponent<ResourceController>();

        if (resourceController == null)
        {
            Debug.LogError("[PlayerHealth] ResourceController�� �����ϴ�! HP �ٸ� ������Ʈ�� �� �����ϴ�.");
            return;
        }

        if (imgbar == null)
        {
            Debug.LogError("[PlayerHealth] imgbar�� �������� �ʾҽ��ϴ�!");
            return;
        }

        UpdateHealthBar();
    }

    void Update()
    {
        if (resourceController == null || imgbar == null) return;

        // ü���� ����Ǿ��� ���� ������Ʈ
        if (lastHealth != resourceController.CurrentHealth)
        {
            UpdateHealthBar();
            lastHealth = resourceController.CurrentHealth;
        }
    }

    public void ForceUpdateHealthBar()
    {
        if (resourceController == null || imgbar == null) return;

        Debug.Log("[PlayerHealth] ���� HP �� ������Ʈ ȣ���!");
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float newScaleX = resourceController.CurrentHealth / resourceController.MaxHealth;
        Vector3 newScale = imgbar.rectTransform.localScale;
        newScale.x = newScaleX; // X�� ũ�⸸ ����
        imgbar.rectTransform.localScale = newScale;
    }
}
