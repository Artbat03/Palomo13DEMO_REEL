using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaNeveras : MonoBehaviour
{
    public Player_Controller _playerController;
    
    void Awake()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerController.estoyEnZonaNevera = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerController.estoyEnZonaNevera = false;
        }
    }
}
