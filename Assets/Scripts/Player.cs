using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For using UI components like Slider
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Movment")]
    public float moveSpeed = 20f, moveLimiter = 0.7f;
    private Rigidbody rigid;

    [Header("Mechanics")]
    public float healthPoints = 100f; // Current health
    public float maxHealthPoints = 100f; // Maximum health
    public float money = 0f;
    public float turnSensitivity = 10f;
    public float bulletSpeed = 20f;
    public List<GameObject> inventory;

    [Header("Weapons")]
    public bool hasPistol = false, hasRifel = false, hasShotty = false;

    [Header("Instantiatable Prefabs")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Camera cam;

    [Header("UI")]
    public TextMeshProUGUI youDiedText; // "YOU DIED" text
    public Slider healthBar; // Health bar UI element

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cam = Camera.main;

        if (youDiedText != null)
        {
            youDiedText.gameObject.SetActive(false); // Hide "YOU DIED" text at the start
        }

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealthPoints; // Set the health bar's maximum value
            healthBar.value = healthPoints; // Initialize the health bar's value
        }
    }

    void Update()
    {
        PlayerMove();
        ShootWeapon();
        RotatePlayer();
    }

    void FixedUpdate()
    {
        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        if (gameObject.transform.position.y < ground.transform.position.y)
        {
            Die();
        }
    }

    void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        rigid.velocity = new Vector3(horizontal * moveSpeed, rigid.velocity.y, vertical * moveSpeed);
    }

    void RotatePlayer()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 100f, LayerMask.GetMask("Ground")))
        {
            Vector3 playerToMouse = floorHit.point - this.transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rigid.MoveRotation(newRotation);
        }
    }

    void ShootWeapon()
    {
        if (hasRifel && Input.GetAxisRaw("Fire1") > 0)
        {
            FireBullet(bulletSpawnPoint.forward);
        }

        if (hasPistol && Input.GetMouseButtonDown(0))
        {
            FireBullet(bulletSpawnPoint.forward);
        }

        if (hasShotty && Input.GetMouseButtonDown(0))
        {
            FireShotgunSpread();
        }
    }

    void FireBullet(Vector3 direction)
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
        // Destroy bullet after 3 seconds
        Destroy(bullet.gameObject, 3f);
    }

    void FireShotgunSpread()
    {
        int numberOfBullets = 3;
        float spreadAngle = 30f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = Mathf.Lerp(-spreadAngle / 2, spreadAngle / 2, i / (float)(numberOfBullets - 1));
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 spreadDirection = rotation * bulletSpawnPoint.forward;

            FireBullet(spreadDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("zombie"))
        {
            TakeDamage(10); // Take 10 damage when hit by a zombie

            Vector3 knockbackDirection = (transform.position - other.transform.position).normalized;
            rigid.AddForce(knockbackDirection * 5f, ForceMode.Impulse);

            Debug.Log("Player hit by zombie! Current Health: " + healthPoints);
        }
        else
        {
            switch (other.tag)
            {
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

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;

        // Update the health bar
        if (healthBar != null)
        {
            healthBar.value = healthPoints;
        }

        if (healthPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died. Showing 'YOU DIED' screen...");

        if (youDiedText != null)
        {
            youDiedText.gameObject.SetActive(true); // Show "YOU DIED" text
        }

        StartCoroutine(DieAndCleanup());
    }

    IEnumerator DieAndCleanup()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before transitioning

        SceneManager.LoadScene("main_menu"); // Replace "main_menu" with the actual scene name
    }
}