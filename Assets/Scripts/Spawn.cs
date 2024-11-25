using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject zombiePrefab;
    public Transform spawnPoint;


    [Header("Inscribed")]
    public int zombieCount = 4;
    public float spawnSeconds = 15;
    public float spawnDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

        spawnPoint = GameObject.FindGameObjectWithTag("zombieSpawner").transform;
        StartCoroutine(SpawnIntervals());
        
    }
    public IEnumerator SpawnIntervals()
    {
        while (true)
        {
            for (int i = 0; i < zombieCount; i++)
            {
                Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(spawnDelay);

            }
            yield return new WaitForSeconds(spawnSeconds);

        }
    }



    // Update is called once per frame
    void Update()
    {
       
         
    }
}
