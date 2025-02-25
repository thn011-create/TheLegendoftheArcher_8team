using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    GameManager gameManager;
    // 오브젝트 위치
    RectTransform rectTransform;

    //public Vector3 offset = new Vector3(0, 1f, 0);

    public float height = 0.1f;

    void Awake()
    {
        

        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
            Debug.LogError("RectTransform NULL");
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 스크린 좌표로 플레이어의 위치를 변환해서 따라간다

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector3(player.position.x, player.transform.position.y));
        rectTransform.position = screenPoint;


    }
}
