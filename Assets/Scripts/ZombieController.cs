using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private GameObject player;
    public NavMeshAgent agent;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Check if the player still exists
        if (player != null)
        {
            Vector3 playerPOS = player.transform.position;
            agent.SetDestination(playerPOS);
        }
        else
        {
            // If the player no longer exists, stop the zombie
            agent.SetDestination(transform.position);
        }
    }
}