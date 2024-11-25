using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1; // Damage the bullet deals

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("zombie"))
        {
            ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the bullet after hitting
        }
    }
}