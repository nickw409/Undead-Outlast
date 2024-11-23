using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{   
    private GameObject player;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPOS = player.transform.position;
        agent.SetDestination(playerPOS);
    }
}
