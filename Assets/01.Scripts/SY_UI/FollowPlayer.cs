using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // ������Ʈ ��ġ
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ��ũ�� ��ǥ�� �÷��̾��� ��ġ�� ��ȯ�ؼ� ���󰣴�
        rectTransform.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position); 
            
    }
}
