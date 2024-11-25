using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject zombiePrefab; // Zombie prefab to spawn
    public float spawnInterval = 5f; // Time between spawns
    public int maxZombies = 10; // Maximum number of zombies this spawner can have active

    private List<GameObject> spawnedZombies = new List<GameObject>(); // Track spawned zombies

    void Start()
    {
        // Start spawning zombies
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            // Check if the max number of zombies has been reached
            spawnedZombies.RemoveAll(zombie => zombie == null); // Clean up destroyed zombies
            if (spawnedZombies.Count < maxZombies)
            {
                SpawnZombie();
            }

            yield return new WaitForSeconds(spawnInterval); // Wait before spawning next zombie
        }
    }

    void SpawnZombie()
    {
        GameObject newZombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        spawnedZombies.Add(newZombie); // Add the new zombie to the list
    }
}