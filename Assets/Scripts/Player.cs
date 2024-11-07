using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movment")]
    public float moveSpeed = 20f, moveLimiter = 0.7f;
    private Rigidbody rigid;
    [Header("Mechanics")]
    public float healthPoints = 100f, money = 0f;
    public List<GameObject> inventory;
    private bool hasPistol = false;
    [Header("Instantiatable Prefabs")]
    public GameObject bulletPrefab;
    private GameObject bullet;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // input goes here
        PlayerMove();
        ShootWeapon();
    }
    private void FixedUpdate()
    {
        if (healthPoints <= 0)
            Destroy(this);
    }
    void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // x axis
        float vertical = Input.GetAxisRaw("Vertical"); // z axis
        //float jump = Input.GetAxisRaw("Jump");

        if(horizontal != 0 || vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        rigid.velocity = new Vector3(horizontal * moveSpeed, 0, vertical * moveSpeed);
    }

    void ShootWeapon()
    {
        float shoot = Input.GetAxisRaw("Fire1");

        if (shoot > 0)
        {
            if (hasPistol)
            {
                // instantiate bullet prefab from player position
                // (will make it gun position if we get there)
                bullet = Instantiate(bulletPrefab) as GameObject;
                bullet.transform.position = this.transform.position;
            }
        }
    }

    // collision code
    private void OnTriggerEnter(Collider other)
    { 
        switch (other.tag)
        {
            case "zombie":
                healthPoints -= 1;
                break;
            case "pistol":
                hasPistol = true;
                break;
            // money
            case "$10":
                money += 10;
                break;
            case "$50":
                money += 50;
                break;
            case "$100":
                money += 100;
                break;
        }
        if (!other.tag.Equals("zombie"))
        {
            Destroy(other.gameObject);
        }
    }
}
