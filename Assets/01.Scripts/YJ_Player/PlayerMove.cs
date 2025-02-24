using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public Joystick joy;
    Animator animatorMove;
    private PlayerStats playerStats;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorMove = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        float h = joy.Horizontal;
        float v = joy.Vertical;

        Vector2 moveDir = new Vector2(h, v).normalized;

        if (moveDir != Vector2.zero)
        {
            animatorMove.SetBool("IsMove", true);
            //float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            //transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            animatorMove.SetBool("IsMove", false);
            //����
        }
        rb.velocity = moveDir * playerStats.MoveSpeed;

    }
}
