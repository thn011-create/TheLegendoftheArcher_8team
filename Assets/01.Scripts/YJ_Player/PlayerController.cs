using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    
    public Joystick joy;
    private Camera camera; // ī�޶� ���� ����

    protected override void Start()
    {
        base.Start(); // �θ� Ŭ������ Start() ����
        camera = Camera.main; // ���� ī�޶� ��������
        if (camera == null)
        {
            Debug.LogError(" ���� ī�޶� ã�� �� �����ϴ�!", gameObject);
        }
    }

    protected override void HandleAction()
    {
       
        float horizontal = joy.Horizontal; 
        float vertial = joy.Vertical; 
           
        
        if(horizontal != 0f || vertial != 0f)
        {
            movementDirection = new Vector2(horizontal, vertial); // ���̽�ƽ �Է°��� ������� �̵� ���� ����
            isAttacking = false;
        }
        else
        {
            movementDirection = Vector2.zero;
            isAttacking = true;
        }
        // ���콺 ��ġ ��������
        //Vector2 mousePosition = Input.mousePosition;
        //Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ

        //// ĳ������ ��ġ�� �������� ���콺 ���� ���� ���
        //lookDirection = (worldPos - (Vector2)transform.position);

        //// ���콺�� ĳ������ �Ÿ��� �ʹ� ������ �ٶ󺸴� ������ ����
        //if (lookDirection.magnitude < .9f)
        //{
        //    lookDirection = Vector2.zero; // �ּ� �Ÿ� ������ ��� ���� �ʱ�ȭ
        //}
        //else
        //{
        //    lookDirection = lookDirection.normalized; // ���� ���͸� ����ȭ (ũ�⸦ 1�� ����)
        //}


    }
}