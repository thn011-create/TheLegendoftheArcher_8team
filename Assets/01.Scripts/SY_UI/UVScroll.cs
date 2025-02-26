using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{

    public float speed = 50f; // 구름 이동 속도
    public float resetX = 10f; // 화면 밖으로 나가는 X 좌표
    public float startX = 0f; // 구름이 다시 시작하는 위치
    public float cloudWidth = 5f; // 구름 이미지의 너비

    void Update()
    {
        // 왼쪽으로 이동
        transform.position += Vector3.left * speed * Time.deltaTime;

        // 화면 밖으로 나가면 다시 오른쪽으로 이동
        if (transform.position.x < resetX)
        {
            transform.position = new Vector3(startX + cloudWidth, transform.position.y, transform.position.z);
        }
    }

}
