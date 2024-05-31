using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;
    
    private Transform player;
    
    void Start()
    {
        player = GameObject.Find("PlayerSkin").GetComponent<Transform>();

        targetPosition = new Vector3(player.position.x, player.position.y, -10);
    }

    void Update()
    {
        targetPosition.x = player.position.x;
        targetPosition.y = player.position.y;

        transform.position = targetPosition;
    }
}
