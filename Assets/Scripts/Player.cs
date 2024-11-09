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
    public float turnSensitivity = 10f;
    public List<GameObject> inventory;
    private bool hasPistol = false;
    [Header("Instantiatable Prefabs")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    //private GameObject bullet;
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
        //rotatePlayer();
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

    void rotatePlayer()
    {
        float mouseY = Input.GetAxis("Mouse Y") * turnSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * turnSensitivity;
        float angle = Mathf.Atan2(mouseX, mouseY) * Mathf.Rad2Deg;
        if(angle!=0)
            transform.rotation = Quaternion.Euler(0, angle, 0);

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
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * 5, ForceMode.Impulse);
                //Vector3 bulletVelocity = bullet.GetComponent<Rigidbody>().velocity;
                //bulletVelocity = this.transform.forward * 300;
                //bullet.transform.position = this.transform.position;
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
