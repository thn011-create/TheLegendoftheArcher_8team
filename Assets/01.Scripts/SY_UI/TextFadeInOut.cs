using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeInOut : MonoBehaviour
{
    
    public Text textComponent; // UI 텍스트

    public float blinkSpeed = 1f; // 깜빡이는 속도 (알파값 변화 속도)


    public float maxAlpha = 0.8f; // 최대 알파값
    public float minAlpha = 0f; // 최소 알파값

    //
    private float targetAlpha;
    private Color originalColor;

    void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<Text>();

        originalColor = textComponent.color; // 초기 텍스트 색 저장
        targetAlpha = minAlpha; // 처음에는 텍스트를 완전히 투명하게 설정
    }

    void Update()
    {
        // 알파값을 부드럽게 변경
        float alpha = Mathf.MoveTowards(textComponent.color.a, targetAlpha, blinkSpeed * Time.deltaTime);
        textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // 목표 알파값이 변경되면 방향을 바꿉니다
        if (alpha == targetAlpha)
        {
            targetAlpha = targetAlpha == minAlpha ? maxAlpha : minAlpha;
        }
    }

}
