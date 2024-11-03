using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movment")]
    public float moveSpeed = 20f, moveLimiter = 0.7f;
    private Rigidbody rigid;
    [Header("Mechanics")]
    public float healthPoints = 100f, money;
    public List<string> inventory;
    // Start is called before the first frame update
    // comment for my new branch
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
    private void FixedUpdate()
    {
        if (healthPoints <= 0)
            Destroy(this);
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

    private void OnTriggerEnter(Collider other)
    { 
        switch (other.tag)
        {
            case "zombie":
                healthPoints -= 1;
                break;
            case "pistol":
                Destroy(other);
                //inventory[0] = "pistol";
                break;
            // money
            case "$50":
                //Destroy(other);
                money += 50;
                break;
            case "$100":
                //Destroy(other);
                money += 100;
                break;
        }
        /*if (!other.tag.Equals("zombie"))
        {
            Destroy(other);
        }*/
    }
}
