using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeInOut : MonoBehaviour
{
    
    public Text textComponent; // UI �ؽ�Ʈ

    public float blinkSpeed = 1f; // �����̴� �ӵ� (���İ� ��ȭ �ӵ�)


    public float maxAlpha = 0.8f; // �ִ� ���İ�
    public float minAlpha = 0f; // �ּ� ���İ�

    //
    private float targetAlpha;
    private Color originalColor;

    void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<Text>();

        originalColor = textComponent.color; // �ʱ� �ؽ�Ʈ �� ����
        targetAlpha = minAlpha; // ó������ �ؽ�Ʈ�� ������ �����ϰ� ����
    }

    void Update()
    {
        // ���İ��� �ε巴�� ����
        float alpha = Mathf.MoveTowards(textComponent.color.a, targetAlpha, blinkSpeed * Time.deltaTime);
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // ��ǥ ���İ��� ����Ǹ� ������ �ٲߴϴ�
        if (alpha == targetAlpha)
        {
            targetAlpha = targetAlpha == minAlpha ? maxAlpha : minAlpha;
        }
    }

}
