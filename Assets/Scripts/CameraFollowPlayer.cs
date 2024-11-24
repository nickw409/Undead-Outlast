using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject Player;

    public float offset = 2f;
    public float height = 7f;

    void FixedUpdate()
    {
        // Check if the Player still exists
        if (Player != null)
        {
            transform.position = Player.transform.position + new Vector3(0, height, 0);
        }
        else
        {
            Debug.LogWarning("Player object is destroyed. Camera is no longer following.");
        }
    }
}