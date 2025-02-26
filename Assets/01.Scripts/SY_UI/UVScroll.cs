using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{

    public float speed = 50f; // ���� �̵� �ӵ�
    public float resetX = 10f; // ȭ�� ������ ������ X ��ǥ
    public float startX = 0f; // ������ �ٽ� �����ϴ� ��ġ
    public float cloudWidth = 5f; // ���� �̹����� �ʺ�

    void Update()
    {
        // �������� �̵�
        transform.position += Vector3.left * speed * Time.deltaTime;

        // ȭ�� ������ ������ �ٽ� ���������� �̵�
        if (transform.position.x < resetX)
        {
            transform.position = new Vector3(startX + cloudWidth, transform.position.y, transform.position.z);
        }
    }

}
