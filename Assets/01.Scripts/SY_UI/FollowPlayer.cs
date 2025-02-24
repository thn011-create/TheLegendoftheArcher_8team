using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // 오브젝트 위치
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 스크린 좌표로 플레이어의 위치를 변환해서 따라간다
        rectTransform.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position); 
            
    }
}
