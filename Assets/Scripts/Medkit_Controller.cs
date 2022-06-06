using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Medkit_Controller : MonoBehaviour
{
    // Variables
    public HealthSystem _healthSystem;


    public void Awake()
    {
        _healthSystem = GameObject.FindGameObjectWithTag("PlayerFather").GetComponent<HealthSystem>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _healthSystem.isInfected = false;
            _healthSystem.actualHealth = 100;
            GameManager.Instance.healthBarSlider.value = _healthSystem.actualHealth;
            Destroy(gameObject);
        }
    }
}
