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
    public float bulletSpeed = 20f;
    public List<GameObject> inventory;
    private bool hasPistol = false, hasRifel = false, hasShotty = false;
    [Header("Instantiatable Prefabs")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Camera cam;
    //private GameObject bullet;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // input goes here
        PlayerMove();
        ShootWeapon();
        rotatePlayer();
    }
    private void FixedUpdate()
    {
        if (healthPoints <= 0)
            Destroy(this.gameObject);
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

    // from https://www.reddit.com/r/Unity3D/comments/44mqkj/making_a_top_down_shooter_how_do_i_make_my/?rdt=47221
    void rotatePlayer()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, 100f, LayerMask.GetMask("Ground")))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - this.transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rigid.MoveRotation(newRotation);
        }
    }

    void ShootWeapon()
    {

        if (hasRifel)
        {
            if (Input.GetAxisRaw("Fire1") > 0) // continuous fire
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);   
            }
        }

        if (hasPistol)
        {
            if (Input.GetMouseButtonDown(0)) // single fire
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);
            }
        }
        // this resource seemed to help me in the development of this condition
        // https://www.reddit.com/r/Unity2D/comments/76dyvl/how_do_you_do_a_shotgun_spread_in_a_topdown/?rdt=54590
        if (hasShotty) 
        {
            var x = bulletSpawnPoint.position.x - transform.position.x;
            var z = bulletSpawnPoint.position.z - transform.position.z;
            float rotateAngleLeftLeft = 15 + (Mathf.Atan2(z, x) * Mathf.Rad2Deg);
            float rotateAngleRightRight = -15 + (Mathf.Atan2(z, x) * Mathf.Rad2Deg);
            float rotateAngleLeft = 5 + (Mathf.Atan2(z, x) * Mathf.Rad2Deg);
            float rotateAngleRight = -5 + (Mathf.Atan2(z, x) * Mathf.Rad2Deg);
            if (Input.GetMouseButtonDown(0)) // single fire
            {
                var leftBullet = Instantiate(bulletPrefab,
                                                 bulletSpawnPoint.position,
                                                          Quaternion.identity);
                var rightBullet = Instantiate(bulletPrefab,
                                                 bulletSpawnPoint.position,
                                                          Quaternion.identity);
                var leftLeftBullet = Instantiate(bulletPrefab,
                                                bulletSpawnPoint.position,
                                                          Quaternion.identity);
                var rightRightBullet = Instantiate(bulletPrefab,
                                                bulletSpawnPoint.position,
                                                          Quaternion.identity);
                var leftMovementDirection = new Vector3(
                                                       Mathf.Cos(rotateAngleLeft * Mathf.Deg2Rad),
                                                       0,
                                                       Mathf.Sin(rotateAngleLeft * Mathf.Deg2Rad)
                                                       ).normalized;

                var rightMovementDirection = new Vector3(
                                                        Mathf.Cos(rotateAngleRight * Mathf.Deg2Rad),
                                                        0,
                                                        Mathf.Sin(rotateAngleRight * Mathf.Deg2Rad)
                                                        ).normalized;

                var leftLeftMovementDirection = new Vector3(
                                                        Mathf.Cos(rotateAngleLeftLeft * Mathf.Deg2Rad), 
                                                        0, 
                                                        Mathf.Sin(rotateAngleLeftLeft * Mathf.Deg2Rad)
                                                        ).normalized;

                var rightRightMovementDirection = new Vector3(
                                                         Mathf.Cos(rotateAngleRightRight * Mathf.Deg2Rad), 
                                                         0, 
                                                         Mathf.Sin(rotateAngleRightRight * Mathf.Deg2Rad)
                                                         ).normalized;
                leftLeftBullet.GetComponent<Rigidbody>().velocity 
                                     = leftLeftMovementDirection * bulletSpeed;
                rightRightBullet.GetComponent<Rigidbody>().velocity 
                                     = rightRightMovementDirection * bulletSpeed;
                leftBullet.GetComponent<Rigidbody>().velocity
                                     = leftMovementDirection * bulletSpeed;
                rightBullet.GetComponent<Rigidbody>().velocity
                                    = rightMovementDirection * bulletSpeed;
                // debugging code
                Destroy(rightRightBullet.gameObject, 3f);
                Destroy(leftLeftBullet.gameObject, 3f);
                Destroy(rightBullet.gameObject, 3f);
                Destroy(leftBullet.gameObject, 3f);
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
                hasRifel = false;
                hasShotty = false;
                break;
            case "rifel":
                hasRifel = true;
                hasPistol = false;
                hasShotty = false;
                break;
            case "shotgun":
                hasShotty = true;
                hasRifel = false;
                hasPistol = false;
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
