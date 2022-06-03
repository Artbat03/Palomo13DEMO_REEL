using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Controller : MonoBehaviour
{
    // Variables
    public Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
