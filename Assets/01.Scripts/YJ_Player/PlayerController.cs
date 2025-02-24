using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    
    public Joystick joy;
    private Camera camera; // 카메라 변수 선언

    protected override void Start()
    {
        base.Start(); // 부모 클래스의 Start() 실행
        camera = Camera.main; // 메인 카메라 가져오기
        if (camera == null)
        {
            Debug.LogError(" 메인 카메라를 찾을 수 없습니다!", gameObject);
        }
    }

    protected override void HandleAction()
    {
       
        float horizontal = joy.Horizontal; 
        float vertial = joy.Vertical; 
        movementDirection = new Vector2(horizontal, vertial); // 조이스틱 입력값을 기반으로 이동 방향 설정        
        // 마우스 위치 가져오기
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition); // 마우스 위치를 월드 좌표로 변환

        // 캐릭터의 위치를 기준으로 마우스 방향 벡터 계산
        lookDirection = (worldPos - (Vector2)transform.position);

        // 마우스와 캐릭터의 거리가 너무 가까우면 바라보는 방향을 유지
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero; // 최소 거리 이하일 경우 방향 초기화
        }
        else
        {
            lookDirection = lookDirection.normalized; // 방향 벡터를 정규화 (크기를 1로 유지)
        }


    }
}