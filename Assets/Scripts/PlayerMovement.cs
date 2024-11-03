using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f, moveLimiter = 0.7f;
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // input goes here
        PlayerMove();
    }

    void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // z axis
        float vertical = Input.GetAxisRaw("Vertical"); // x axis
        //float jump = Input.GetAxisRaw("Jump");

        if(horizontal != 0 || vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        rigid.velocity = new Vector3(horizontal * moveSpeed, 0, vertical * moveSpeed);
    }
}
