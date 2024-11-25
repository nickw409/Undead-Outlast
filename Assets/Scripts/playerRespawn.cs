using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    public GameObject player;
    public Transform respawnPoint;
    public void Respawn()
    {
        if(player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
